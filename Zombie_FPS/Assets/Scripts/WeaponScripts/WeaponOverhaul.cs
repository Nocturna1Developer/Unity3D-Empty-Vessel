using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class WeaponOverhaul : MonoBehaviour
{
    public Text ammoText;

    private Animator anim;

    public AudioSource audioSource;

    private FirstPersonController FPSController;

    public ParticleSystem muzzleFlash;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Transform shootPoint; // Were the bullet leaves the muzzle

    public GameObject hitParticles;
    public GameObject bloodParticles;

    private Vector3 initPos;
    private Vector3 orginalPosition;
    public Vector3 aimPosition;


    public int bulletPerMag = 32;
    public static int currentbullets;
    public static int bulletsLeft = 96;
    public int fieldofView = 75;

    public float range = 100f;
    public float fireRate = 0.1f;
    public float damage = 5f;
    public float adsSpeed = 1f;
    public float TargetFOV = 50f;
    public float NormalFOV = 70f;
    public float Speed = 5f;

    float fireTimer; // Counter for the delay

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

        // Aim
        AimDownSights();
    }

    void FixedUpdate() 
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

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
        // Can't get negative bullets
        if (fireTimer < fireRate || currentbullets <= 0 || isReloading) 
            return;

        anim.CrossFadeInFixedTime("fire", 0.01f);
        muzzleFlash.Play();
        PlayShootSound();

        currentbullets--;
        UpdateAmmoText();
        fireTimer = 0.0f; // Reset Timer

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name + "hit");

            // The normal returns the 90 drgree direction with the particle effect ( alligns directions )
            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(hitParticleEffect, .2f);

            if(hit.transform.tag == "Zombie")
            {
                GameObject bloodParticleEffect = Instantiate(bloodParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }   

            if(hit.transform.gameObject.GetComponent<HealthController>())
            {
                hit.transform.gameObject.GetComponent<HealthController>().ApplyDamage(damage);
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

