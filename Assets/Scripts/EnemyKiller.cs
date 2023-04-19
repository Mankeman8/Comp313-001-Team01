using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    public int damage = 1;

    private void Start()
    {
        damage += (int)FindObjectOfType<GameManager>().GetDifficultyModifier();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "PlayerCapsule")
        {
            other.gameObject.GetComponent<Target>().TakeDamage(damage);
            FindObjectOfType<GameManager>().DamageTaken(damage);
        }
    }
}
