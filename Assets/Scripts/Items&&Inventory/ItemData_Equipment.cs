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

    [Header("Core stats")]
    public int strength;
    public int dexterity;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critDamage;

    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;
    public void AddModifiers()
    {

    }

    public void RemoveModifiers()
    {

    }
}
