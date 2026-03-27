using UnityEngine;

[CreateAssetMenu(menuName="Restaurant/Premium Item")]
public class PremiumItemSO : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public PremiumItemType itemType;

    [Header("Cost")]
    public int cost;

    [Header("Effect")]
    public int durationDays;
    public int amount;
}
