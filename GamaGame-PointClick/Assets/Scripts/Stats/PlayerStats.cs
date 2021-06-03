using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    // Update is called once per frame
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void TookDamage()
    {
        base.TookDamage();
        anim.SetTrigger("tookDamage");
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine (deathRoutine());
    }

    public IEnumerator deathRoutine()
    {
        GetComponent<PlayerController>().enabled = false;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(5);

        // Kill the player, restarts scene in PlayerManager class
        PlayerManager.instance.KillPlayer();
        GetComponent<PlayerController>().enabled = true;
    }
}
