﻿using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();                     // Considers Armor value before damage
        damage = Mathf.Clamp(damage, 0, int.MaxValue);  // Makes sure if Armor > Damage, it won't heal

        currentHealth -= damage;                        // Takes damage
        Debug.Log(transform.name + "takes " + damage + "damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // Meant to be overwritten
        Debug.Log(transform.name + " died.");
    }


}