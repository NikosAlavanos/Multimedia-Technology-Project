using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")] //create default file Name

public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    //public float itemCooldown;
    //public ItemEffect[] itemEffects;

    [Header("Core stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft Requirments")]
    public List<InventoryItem> craftingMaterials;

    public void AddModifiers()
    {
        HeroStats heroStats = HeroManager.instance.hero.GetComponent<HeroStats>();

        heroStats.strength.AddModifier(strength);
        heroStats.agility.AddModifier(agility);
        heroStats.intelligence.AddModifier(intelligence);
        heroStats.vitality.AddModifier(vitality);

        heroStats.damage.AddModifier(damage);
        heroStats.critChance.AddModifier(critChance);
        heroStats.critPower.AddModifier(critPower);

        heroStats.maxHealth.AddModifier(health);
        heroStats.armor.AddModifier(armor);
        heroStats.evasion.AddModifier(evasion);
        heroStats.magicResistance.AddModifier(magicResistance);

        heroStats.fireDamage.AddModifier(fireDamage);
        heroStats.iceDamage.AddModifier(iceDamage);
        heroStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers()
    {
        HeroStats heroStats = HeroManager.instance.hero.GetComponent<HeroStats>();

        heroStats.strength.RemoveModifier(strength);
        heroStats.agility.RemoveModifier(agility);
        heroStats.intelligence.RemoveModifier(intelligence);
        heroStats.vitality.RemoveModifier(vitality);

        heroStats.damage.RemoveModifier(damage);
        heroStats.critChance.RemoveModifier(critChance);
        heroStats.critPower.RemoveModifier(critPower);

        heroStats.maxHealth.RemoveModifier(health);
        heroStats.armor.RemoveModifier(armor);
        heroStats.evasion.RemoveModifier(evasion);
        heroStats.magicResistance.RemoveModifier(magicResistance);

        heroStats.fireDamage.RemoveModifier(fireDamage);
        heroStats.iceDamage.RemoveModifier(iceDamage);
        heroStats.lightingDamage.RemoveModifier(lightingDamage);
    }
}
