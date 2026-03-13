using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet main { get; private set; }

    [SerializeField] private float totalMoney;

    private void Awake() => main = this;

    public void AddIncome (float amount)
    {
        totalMoney += amount;
        
        UIGameHUD.main.UpdateIncome(totalMoney);
    }

    public bool SpendMoney (float amount)
    {
        // If money is not enough
        if(totalMoney > amount)
        {
            return false;
        }
        // Deduct total money
        else
        {
            totalMoney -= amount;
            UIGameHUD.main.UpdateIncome(totalMoney);

            return true;
        }
    }
}
