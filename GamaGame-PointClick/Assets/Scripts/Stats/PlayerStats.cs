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
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        Debug.Log("DamageRoutine started");
        anim.SetLayerWeight(anim.GetLayerIndex("Damage Layer"), 1);
        anim.SetTrigger("tookDamage");

        yield return new WaitForSeconds(0.8f);
        anim.SetLayerWeight(anim.GetLayerIndex("Damage Layer"), 0);

    }

    public override void Die()
    {
        base.Die();
        StartCoroutine (DeathRoutine());
    }

    public IEnumerator DeathRoutine()
    {
        GetComponent<PlayerController>().enabled = false;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(5);

        // Kill the player, restarts scene in PlayerManager class
        PlayerManager.instance.KillPlayer();
        GetComponent<PlayerController>().enabled = true;
    }
}
