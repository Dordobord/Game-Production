using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Icon Database")]
public class ItemIconDataBase : ScriptableObject
{
    public ItemIcon[] icons;

    public Sprite GetIcon(ItemType type)
    {
        foreach (var icon in icons)
        {
            if (icon.type == type) return icon.sprite;
        }
        return null;
    }
}
[System.Serializable]
public class ItemIcon
{
    public ItemType type;
    public Sprite sprite;
}