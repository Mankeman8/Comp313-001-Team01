using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float health;
    public float startHealth = 100f;
    public float startSpeed = 10f;
    public int value = 50;
    public GameObject deathEffect;
    [Header("Unity Stuff")]
    public Image healthBar;

    private void Awake()
    {
        switch (startSpeed)
        {
            case 5:
                startHealth = 500;
                break;
            case 10:
                startHealth = 100;
                break;
            case 15:
                startHealth = 2000;
                break;
            case 20:
                startHealth = 150;
                break;
            case 25:
                startHealth = 10000;
                break;
            default:
                startHealth = 100;
                break;
        }
    }
    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    public void Slow(float slowValue)
    {
        speed = startSpeed * (1f - slowValue);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }
}
