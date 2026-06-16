using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    public int maxHealth = 100;
    private int currentHealth;
    public GameObject weapon;
    public float attackRange = 2f;
    public int attackDamage = 25;
    public float attackCooldown = 0.8f;

    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (isDead) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * 3f * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        weapon.SetActive(true);

        Collider[] hits = Physics.OverlapSphere(weapon.transform.position, attackRange);
        foreach (Collider col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(attackDamage);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
            
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("You Died!");

        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        Animator anim = GetComponent<Animator>();
        if (anim != null) anim.SetTrigger("Die");
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}