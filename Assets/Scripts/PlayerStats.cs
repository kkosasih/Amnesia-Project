using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public int health;
    public int maxHealth;
    private Slider healthSlider;
    public int stamina;
    public int maxStamina;
    private Slider staminaSlider;

    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        staminaSlider = GameObject.Find("StaminaSlider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ChangeHealth (int newHealth)
    {
        health = newHealth;
        healthSlider.value = (float)health / maxHealth * 100;
    }
    
    public void ChangeStamina (int newStamina)
    {
        stamina = newStamina;
        staminaSlider.value = (float)stamina / maxStamina * 100;
    }
}

