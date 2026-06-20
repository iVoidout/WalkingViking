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
    public AudioClip zombieSound;

    private Transform player;
    private float lastAttackTime;
    private bool isDead = false;


    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(zombieSound);
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        Vector3 target = player.position;
        target.y = 0f;
        Vector3 pos = transform.position;
        pos.y = 0f;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            transform.position = Vector3.MoveTowards(pos, player.position, speed * Time.deltaTime);
            transform.LookAt(target);
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

    void AttackPlayer()
    {
        
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

        // Add points when enemy dies
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(100); // Add 100 points per enemy
        }

        Destroy(gameObject, 2.5f);
    }
}