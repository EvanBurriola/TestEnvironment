using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //[Header "Stats"]
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHealth = 100f;

    public GameObject healthBarUI;
    public Slider slider;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update() {
        slider.value = CalculateHealth();

        if (health < maxHealth) {
            healthBarUI.SetActive(true);
        }

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
    }

    float CalculateHealth() {
        return health / maxHealth;
    }
}
