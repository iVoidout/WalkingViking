using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Attack")]
    public GameObject weapon;
    public float attackRange = 2f;
    public int attackDamage = 7;
    public float attackCooldown = 0.8f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip axeSound;

    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        Move();

        if (Input.GetMouseButtonDown(0) &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        if (audioSource != null && axeSound != null)
            audioSource.PlayOneShot(axeSound);

        weapon.SetActive(true);

        Collider[] hits = Physics.OverlapSphere(
            weapon.transform.position,
            attackRange
        );

        foreach (Collider col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player Died!");
    }
}