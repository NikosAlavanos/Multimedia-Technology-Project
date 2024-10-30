using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")] //create default file Name

public class ItemData : ScriptableObject
{
    public string itemaName;
    public Sprite icon;
}
