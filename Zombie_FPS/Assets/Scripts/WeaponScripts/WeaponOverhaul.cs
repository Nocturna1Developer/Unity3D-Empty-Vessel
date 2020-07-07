using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class WeaponOverhaul : MonoBehaviour
{
    public Text ammoText;

    private Animator anim;

    private FirstPersonController FPSController;
    
    [Header("Weapon Audio")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    //public AudioClip emptyChamberSound;

    [Header("Weapon Properties")]
    public int bulletPerMag = 32;
    public static int currentbullets;
    public static int bulletsLeft = 96;
    public int fieldofView = 75;
    public ParticleSystem muzzleFlash;
    public Transform shootPoint;         
    public GameObject hitParticles;
    public GameObject bloodParticles;

    [Header("Weapon Setings")]
    public float range = 100f;
    public float fireRate = 0.1f;
    public float damage = 5f;
    public float headshotDamage = 5f;
    public float adsSpeed = 1f;
    public float TargetFOV = 50f;
    public float NormalFOV = 70f;
    public float Speed = 5f;
    public float spreadFactor = 0.1f;

    private Vector3 initPos;
    private Vector3 orginalPosition;
    public Vector3 aimPosition;

    // Counter for the delay
    float fireTimer;

    private bool isReloading;
    private bool isLeaningLeft = false;
    private bool isLeaningRight = false;

    void Start()
    {
       currentbullets = bulletPerMag;
       orginalPosition = transform.localPosition;

       anim = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Shoot
        if (Input.GetButton("Fire1"))
        {
            if(currentbullets > 0)
                Fire();
            else if(bulletsLeft > 0)
                DoReload(); 
        }

        // Add into time counter
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; 
        }

        // Reload
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(currentbullets < bulletPerMag && bulletsLeft > 0)  
                DoReload(); 
        }
        AimDownSights();

        //if (bulletsLeft <= 0)
        //{
        //    audioSource.PlayOneShot(emptyChamberSound);
        //}
    }

    void FixedUpdate() 
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Fire")) anim.SetBool("fire", false);

        isReloading = info.IsName("Reload");
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * adsSpeed);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, TargetFOV, Speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, orginalPosition, Time.deltaTime * adsSpeed);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, NormalFOV, Speed * Time.deltaTime);
        }
    }

    private void Fire()
    {
        // Prevents negative bullets

        if (fireTimer < fireRate || currentbullets <= 0 || isReloading) 
            return;

        // Plays animation, reduces bullets, plays audio

        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleFlash.Play();
        PlayShootSound();

        currentbullets--;
        UpdateAmmoText();
        // Resets Timer
        fireTimer = 0.0f; 

        // Bullet Spread - Changes with respect to parent

        Vector3 shootDirection = shootPoint.transform.forward;
        shootDirection = shootDirection + shootPoint.TransformDirection(new Vector3(Random.Range(-spreadFactor,spreadFactor), Random.Range(-spreadFactor,spreadFactor)));

        // Raycasting

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootDirection, out hit, range))
        {
            //Debug.Log(hit.transform.name + "hit");

            // The normal returns the 90 drgree direction with the particle effect ( alligns directions )
            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(hitParticleEffect, .2f);

            if(hit.transform.tag == "Zombie")
            {
                GameObject bloodParticleEffect = Instantiate(bloodParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(bloodParticleEffect, .4f);
                Destroy(hitParticleEffect, .001f);
            }   

            if(hit.transform.gameObject.GetComponent<HealthController>())
            {
                hit.transform.gameObject.GetComponent<HealthController>().ApplyDamage(damage);
            }

            if (hit.transform.gameObject.GetComponent<Headshot>())
            {
                //Debug.Log(hit.transform.name + "hit");
                hit.transform.gameObject.GetComponent<Headshot>().ApplyDamage(headshotDamage);
            }
        }
    }

    public void Reload()
    {
        //Debug.Log("reloading!!!");
        // Ignore this fuction if bullets left is 0
        if( bulletsLeft <= 0) return;

        int bulletsToLoad = bulletPerMag - currentbullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentbullets += bulletsToDeduct;

        UpdateAmmoText();
    }

    void DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (isReloading) return;
        anim.CrossFadeInFixedTime("Reload", 0.5f);
        PlayReloadSound();
    }
    
    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }

    private void PlayReloadSound()
    {
        audioSource.clip = reloadSound;
        audioSource.Play();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = " Ammo: " + currentbullets + " / " + bulletsLeft;
    }
  
}

