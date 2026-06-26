using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System.Collections;

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
    public float attackCooldown = 2f;

    [Header("Axe Audio")]
    public AudioSource audioSource;
    public AudioClip axeSound;

    [Header("Death")]
    public AudioSource audioSourceDeath;
    public AudioClip deathSound;
    public AudioSource audioSourceDeathSFX;
    public AudioClip deathSFXSound;
    public GameObject deathMenu;

    [Header("Healing")]
    public AudioSource audioSourceHeal;
    public AudioClip healSound;
    public TMP_Text healedText;

    private float lastAttackTime;
    private bool isDead = false;

    [Header("Misc")]
    public int score;
    private ScoreManager scoreManager;
    private int lastMilestone = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        Move();

        scoreManager = FindObjectOfType<ScoreManager>();
        score = scoreManager.GetScore();
        int currentMilestone = score / 600;

        if (currentMilestone > lastMilestone)
        {
            if (audioSourceHeal != null && healSound != null)
                audioSourceHeal.PlayOneShot(healSound);
            currentHealth += 25;
            lastMilestone = currentMilestone;
            StartCoroutine(ShowRoutine(3f));

        }
            if (Input.GetMouseButtonDown(0) &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    IEnumerator ShowRoutine(float time)
    {
        healedText.alpha = 255f;
        yield return new WaitForSeconds(time);
        healedText.alpha = 0f;
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
        deathMenu.SetActive(true);

        isDead = true;
        if (audioSourceDeath != null && deathSound != null)
            audioSourceDeath.PlayOneShot(deathSound);
        if (audioSourceDeathSFX != null && deathSFXSound != null)
            audioSourceDeathSFX.PlayOneShot(deathSFXSound);
        GetComponent<Animator>()?.SetTrigger("IsDead");
    }
}