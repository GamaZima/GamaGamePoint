using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumables : Items
{
    //public CharacterStats characterStats;
    public int heal;

    public override void Use()
    {
        base.Use();
        Heal();
        RemoveFromInventory();             
    }

    public virtual void Heal()
    {
        CharacterStats stats = FindObjectOfType<CharacterStats>();

        if (stats != null)
        {
            stats.AddHealth(heal);
        }
    }
}