using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private Transform player;
    private float lastAttackTime;
    private bool isDead = false;

    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        GetComponent<Animator>()?.SetTrigger("IsAttacking");
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null) pc.TakeDamage(damage);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead)
        {
            Vector3 posdead = transform.position;
            posdead.y -= 0.001f;
            transform.position = posdead;
            return;
        }
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<Animator>()?.SetTrigger("IsDead");
        Destroy(gameObject, 2f);
    }
}