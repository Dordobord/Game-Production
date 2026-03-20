using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet main { get; private set; }

    [SerializeField] private float totalMoney;
    [SerializeField] private float dayIncome;

    private void Awake() => main = this;

    public float CalculateIncome() => dayIncome - totalMoney;
    public float FetchDayIncome() => dayIncome;
    public float FetchSavings() => totalMoney;
    public void ResetDayIncome() => dayIncome = totalMoney;

    public void AddIncome (float amount)
    {
        dayIncome += amount;

        UIGameHUD.main.UpdateIncome(dayIncome);
    }

    public bool SpendMoney (float amount)
    {
        // If money is not enough
        if(totalMoney < amount)
        {
            return false;
        }
        // Deduct total money
        
        totalMoney -= amount;
        UIGameHUD.main.UpdateIncome(totalMoney);
        return true;
    }

    public bool PayBills(float utilities, float stock)
    {
        float totalBill = utilities + stock;

        // Cannot afford bill, auto fail
        if(dayIncome < totalBill) return false;

        // Pay bills
        dayIncome -= totalBill;
        return true;
    }

    public void ConfirmWalletChanges()
    {
        totalMoney = dayIncome;
        
        UIGameHUD.main.UpdateIncome(dayIncome);
    }
}
