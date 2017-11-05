using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour
{
    public int enemyHealth;
    public int maxEnemyHealth;
    private Slider enemyHealthSlider;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

    }

    void ApplyDamage(int damage)

    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        enemyHealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update() { }

    void ChangeHealth(int newenemyHealth)

    {
        enemyHealth = newenemyHealth;

        enemyHealthSlider.value = (float)enemyHealth / maxEnemyHealth * 100;
    }

}