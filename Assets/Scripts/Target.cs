using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 50;
    private int healthRegen = 5;
    private float timer = 10f;
    private float keepTimer;

    public HealthBar healthBar;

    private void Start()
    {
        if (this.gameObject.tag == "Enemy")
        {
            health = health * (int)FindObjectOfType<GameManager>().GetDifficultyModifier();
            healthBar.SetMaxHealth(health);
        }
        keepTimer = timer;
        healthBar.SetMaxHealth(health);
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag == "Player")
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                health += healthRegen;
                timer = keepTimer;
            }
            if (health > 500)
            {
                health = 500;
                healthBar.SetHealth(health);
            }
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if(this.gameObject.tag== "Enemy")
        {
            FindObjectOfType<GenerateEnemies>().enemyCount--;
            FindObjectOfType<GameManager>().EnemiesKilled(1);
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "Player")
        {
            FindObjectOfType<Gun>().isDead = true;
            FindObjectOfType<GameManager>().playerDead = true;
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}