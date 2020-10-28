using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth playerHP;
    public float currentHealth;
    public float maxHealth = 100f;

    void Awake()
    {
        playerHP = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(float amount)
    {
        Debug.Log("Damage taken: " + amount + ", current HP: " + currentHealth);
        currentHealth -= amount;

    }
}
