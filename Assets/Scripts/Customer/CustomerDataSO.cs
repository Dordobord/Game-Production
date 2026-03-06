using UnityEngine;

[CreateAssetMenu(menuName = "Game/CustomerData")]
public class CustomerDataSO : ScriptableObject
{
    public string customerName;
    public float moveSpeed = 5f;
    public float orderTimer = 15f;
    public float serveTimer = 15f;
    public float warningTimer = 5f;
    public int reward = 50;
    public int penalty = 20;
    public int expReward = 50;
    public int expPenalty = 20;
    public ItemType[] menuList;
    public Color customerCol = Color.white;
}
