using System.Collections.Generic;
using UnityEngine;

public class UtilityBillHandler : MonoBehaviour
{
    public static UtilityBillHandler main { get; private set; }
    [Tooltip("Index 0 is Level 1, Index 1 is Level 2, etc.")]
    [SerializeField] private List<float> electricityBill = new List<float>();
    [Tooltip("Index 0 is Level 1, Index 1 is Level 2, etc.")]
    [SerializeField] private List<float> waterBill = new List<float>();

    private void Awake() => main = this;
    
    public float CalculateBill(int level)
    {
        if(level <= 0)
        {
            Debug.Log("Level cannot be zero or negative.");
            return 0;
        }

        int levelIndex = level - 1;
        float totalBill = electricityBill[levelIndex] + waterBill[levelIndex];

        return totalBill;
    }
}
