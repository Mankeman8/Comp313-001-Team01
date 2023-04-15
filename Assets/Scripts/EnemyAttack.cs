using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;
    public AudioClip attackSound;

    public float scaleTime = 1f;
    public float maxScale = 2f;

    private GameObject _player;
    private bool _canAttack = true;
    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= attackRange && _canAttack)
        {
            Debug.Log("Attack Start");
            StartCoroutine(AttackCooldown());
            StartCoroutine(ScaleOverTime(scaleTime));
            Attack();
        }
    }

    private void Attack()
    {
        // Play attack animation or perform attack action here
        Debug.Log("Enemy attacked player for " + attackDamage + " damage!");
        //// Damage player
        //PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        //if (playerHealth != null)
        //{
        //    playerHealth.TakeDamage(attackDamage);
        //}
        PlayAttackSound();

    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    private IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(maxScale, maxScale, maxScale);

        float currentTime = 0.0f;

        while (currentTime <= time / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, (currentTime / (time / 2)));
            currentTime += Time.deltaTime;
            yield return null;
        }

        while (currentTime <= time)
        {
            transform.localScale = Vector3.Lerp(destinationScale, originalScale, ((currentTime - (time / 2)) / (time / 2)));
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    void PlayAttackSound()
    {
        _audioSource.PlayOneShot(attackSound);
    }
}
