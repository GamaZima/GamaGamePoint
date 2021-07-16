using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator anim;
    //EnemyController enemyActive;
    //Transform deahtTransform;


    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        //enemyActive = GetComponent<EnemyController>();
    }

    public override void TookDamage()
    {
        base.TookDamage();
        anim.SetTrigger("tookDamage");
    }

    public override void Die()
    {
        base.Die();

        anim.SetTrigger("die");
        GetComponent<EnemyController>().enabled = false;
        GetComponent<Enemy>().enabled = false;

        // Drop loot
        // Destroy(gameObject);
    }

    
}
