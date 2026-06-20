using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    public AudioClip attackSound;   // 🔊 صدای حمله زامبی

    private Transform player;
    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            transform.LookAt(player.position);
        }
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        // 🔊 پخش صدای حمله زامبی
        if (attackSound != null)
            AudioSource.PlayClipAtPoint(attackSound, transform.position);

        GetComponent<Animator>()?.SetTrigger("IsAttacking");

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

        GetComponent<Animator>()?.SetTrigger("IsDead");

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(100);

        Destroy(gameObject, 2f);
    }
}