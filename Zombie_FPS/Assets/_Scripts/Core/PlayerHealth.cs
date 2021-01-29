using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public static float CurrentHealth {get; set;}
    public float MaxHealth { get; set; }

    //private GameMaster gm;

    public Slider healthbar;

    private Vector3 respawnPoint;

    public FirstPersonController player;

    void Start() 
    {
        MaxHealth = 100f;

        CurrentHealth = MaxHealth;

        healthbar.value = CalculateHealth();

        respawnPoint = player.transform.position;
    }

    void Update() 
    {

    }

    public void TakeDamage(float damageValue)
    {
        //Debug.Log("Taking damage");
        CurrentHealth -= damageValue;
        healthbar.value = CalculateHealth();

        if (CurrentHealth <= 0)
            Die();
           
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    void Die() 
    {
        CurrentHealth = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.transform.position = respawnPoint;
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
         respawnPoint = newPosition;
    }
}