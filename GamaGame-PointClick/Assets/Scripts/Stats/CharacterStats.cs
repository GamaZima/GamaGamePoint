using System;
using UnityEngine;

[SerializeField]
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; set; }
    //public int heal;

    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;

    public int MyCurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    // Starts the game with maxHealth value
    private void Awake()
    {
        currentHealth = maxHealth;
    }    

    private void Update()
    {
        // Takes damage on player when T is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }

        //if(Input.GetKeyDown(KeyCode.Y)) { Heal(5); }        
    }

    // Damage the character
    public void TakeDamage (int damage)
    {
        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            // Triggers Animation
            TookDamage();
        }       

        Debug.Log(transform.name + " takes " + damage + " damage");

        // Sends info to GUI health bar slider..?
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);

            if (currentHealth > 100) currentHealth = 100;
            OnHealthChanged(maxHealth, currentHealth);
        }

        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void TookDamage()
    {
        // Takes damage
        // Meant to be overwritten
        // Set Damage animation
    }

    // Heals character
    internal void AddHealth(int heal)
    {
        {
            currentHealth += heal;
        }
        Debug.Log(transform.name + " received " + heal + " healing points");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);

            if (currentHealth > 100) currentHealth = 100;
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // Meant to be overwritten
        Debug.Log(transform.name + " died.");
    }

}
