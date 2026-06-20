using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 50;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    private Transform player;
    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            MoveToPlayer();
        }
        else
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        transform.LookAt(player);
    }

    void AttackPlayer()
    {
        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
            pc.TakeDamage(damage);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        health -= dmg;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject, 1f);
    }
}