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
}
