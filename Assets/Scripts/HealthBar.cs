using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;     // Drag HealthBarFill here
    public Text healthText;          // Drag HealthText here
    public PlayerController player;  // Drag Player here

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (healthBarFill != null && player != null)
        {
            healthBarFill.fillAmount = 1.0f; // Start at full health
        }

        UpdateHealthText();
    }

    void Update()
    {
        if (player != null && healthBarFill != null)
        {
            // Update the fill amount based on health
            float healthPercent = (float)player.currentHealth / player.maxHealth;
            healthBarFill.fillAmount = healthPercent;

            UpdateHealthText();
            UpdateHealthBarColor();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null && player != null)
        {
            healthText.text = player.currentHealth + " / " + player.maxHealth;
        }
    }

    void UpdateHealthBarColor()
    {
        if (healthBarFill != null)
        {
            float healthPercent = healthBarFill.fillAmount;

            if (healthPercent > 0.6f)
            {
                healthBarFill.color = Color.green;
            }
            else if (healthPercent > 0.3f)
            {
                healthBarFill.color = Color.yellow;
            }
            else
            {
                healthBarFill.color = Color.red;
            }
        }
    }
}