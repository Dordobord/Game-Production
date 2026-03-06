using UnityEngine;
using TMPro;

public class BuyBCManager : MonoBehaviour
{
    [SerializeField]
    private float money;
    
    public int totalBcStock;

    // Bottle Caps Stock
    private int handfulBcStock;
    private int pouchBcStock;
    private int bucketBcStock;
    private int pileBcStock;

    public TextMeshProUGUI bcStockText;

    void Start()
    {
        bcStockText.text = "BC: " + totalBcStock;
    }

    public void BuyHandfulOfBottleCaps()
    {
        if(money >= 50)
        {
            Debug.Log("Bought a Handful of Bottle Caps");

            money-=50;
            
            handfulBcStock = 55;
            totalBcStock += handfulBcStock;
            bcStockText.text = "BC: " + totalBcStock;
        }

        else
        {
            Debug.Log("Not enough money to buy a Handful of Bottle Caps");
        }
    }

    public void BuyAPouchOfBottleCaps()
    {
        if(money >= 80)
        {
            Debug.Log("Bought a Pouch of Bottle Caps");

            money-=80;
            
            pouchBcStock = 90;
            totalBcStock += pouchBcStock;
            bcStockText.text = "BC: " + totalBcStock;
        }

        else
        {
            Debug.Log("Not enough money to buy a Pouch of Bottle Caps");
        }
    }

    public void BuyBucketOfBottleCaps()
    {
        if(money >= 120)
        {
            Debug.Log("Bought a Bucket of Bottle Caps");

            money-=120;
            
            bucketBcStock = 135;
            totalBcStock += bucketBcStock;
            bcStockText.text = "BC: " + totalBcStock;
        }

        else
        {
            Debug.Log("Not enough money to buy a Bucket of Bottle Caps");
        }
    }

    public void BuyPileOfBottleCaps()
    {
        if(money >= 150)
        {
            Debug.Log("Bought a Pile of Bottle Caps");

            money-=150;
            
            pileBcStock = 170;
            totalBcStock += pileBcStock;
            bcStockText.text = "BC: " + totalBcStock;
        }

        else
        {
            Debug.Log("Not enough money to buy a Pile of Bottle Caps");
        }
    }
}
