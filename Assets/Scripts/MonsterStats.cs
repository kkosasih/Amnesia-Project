using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour {
    public int enemyHealth;
    public int maxEnemyHealth;
    public Slider enemyHealthSlider;
    public Slider enemyEchoSlider;
    // Instantiate(Resources.Load("MonsterHealth"), GameObject.Find("Monster").transform);
    // Spawn a health bar attached to a monster.


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

    }

    // Update is called once per frame
    void Update() { }

    public void ChangeEnemyHealth (int newenemyHealth)
    {
        if (newenemyHealth > enemyHealth)
        {
            StartCoroutine(GainHealth(newenemyHealth));
        }
        else
        {
            StartCoroutine(LoseHealth(newenemyHealth));
        }
        enemyHealth = newenemyHealth;
    }

    private IEnumerator LoseHealth (int newHealth)
    {
        enemyHealthSlider.value = (float)newHealth / maxEnemyHealth * 100;
        enemyEchoSlider.gameObject.transform.Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0.5f, 0, 0);
        yield return new WaitForSeconds(1);
        enemyEchoSlider.value = enemyHealthSlider.value;
    }

    private IEnumerator GainHealth (int newHealth)
    {
        enemyEchoSlider.gameObject.transform.Find("Fill Area").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
        enemyEchoSlider.value = (float)newHealth / maxEnemyHealth * 100;
        yield return new WaitForSeconds(1);
        enemyHealthSlider.value = enemyEchoSlider.value;
    }
}