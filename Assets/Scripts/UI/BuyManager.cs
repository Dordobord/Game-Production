using UnityEngine;
using TMPro;

public class BuyManager : MonoBehaviour
{
    public BuyBCManager buyBCManager;

    [Header("OwnedItems")]
    private int jukeBoxOwned = 0;
    private int blenderBoostOwned = 0;
    private int coffeeBoostOwned = 0;
    private int televisionOwned = 0;

    public TextMeshProUGUI bcStockText;

    [Header("OwnedItemsText")]
    public TextMeshProUGUI jukeBoxOwnedText;
    public TextMeshProUGUI blenderBoostOwnedText;
    public TextMeshProUGUI coffeeBoostOwnedText;
    public TextMeshProUGUI televisionOwnedText;

    void Start()
    {
        buyBCManager.totalBcStock = 0;
        bcStockText.text = "BC: " + buyBCManager.totalBcStock;
        jukeBoxOwnedText.text = "Owned: " + jukeBoxOwned;
        blenderBoostOwnedText.text = "Owned: " + blenderBoostOwned;
        coffeeBoostOwnedText.text = "Owned: " + coffeeBoostOwned;
        televisionOwnedText.text = "Owned: " + televisionOwned;
    }

    public void BuyJukeBox()
    {
        if (buyBCManager.totalBcStock >= 20)
        {
            Debug.Log("Bought a Juke Box");

            buyBCManager.totalBcStock -= 20;
            bcStockText.text = "BC: " + buyBCManager.totalBcStock;

            jukeBoxOwned++;
            jukeBoxOwnedText.text = "Owned: " + jukeBoxOwned;
        }

        else
        {
            Debug.Log("Not enough BC to buy a Juke Box");
        }
    }

    public void UseJukeBox()
    {
        if (jukeBoxOwned > 0)
        {
            Debug.Log("Used a Juke Box");

            jukeBoxOwned--;
            jukeBoxOwnedText.text = "Owned: " + jukeBoxOwned;
        }

        else
        {
            Debug.Log("No Juke Boxes to use");
        }
    }

    public void BuyBlenderBoost()
    {
        if (buyBCManager.totalBcStock >= 25)
        {
            Debug.Log("Bought a BlenderBoost");

            buyBCManager.totalBcStock -= 25;
            bcStockText.text = "BC: " + buyBCManager.totalBcStock;

            blenderBoostOwned++;
            blenderBoostOwnedText.text = "Owned: " + blenderBoostOwned;
        }

        else
        {
            Debug.Log("Not enough BC to buy a Blender Boost");
        }
    }

    public void UseBlenderBoost()
    {
        if (blenderBoostOwned > 0)
        {
            Debug.Log("Used a Blender Boost");

            blenderBoostOwned--;
            blenderBoostOwnedText.text = "Owned: " + blenderBoostOwned;
        }

        else
        {
            Debug.Log("No Blender Boost to use");
        }
    }

    public void BuyCoffeeBoost()
    {
        if (buyBCManager.totalBcStock >= 30)
        {
            Debug.Log("Bought a Coffee Boost");

            buyBCManager.totalBcStock -= 30;
            bcStockText.text = "BC: " + buyBCManager.totalBcStock;

            coffeeBoostOwned++;
            coffeeBoostOwnedText.text = "Owned: " + coffeeBoostOwned;
        }

        else
        {
            Debug.Log("Not enough BC to buy a Coffee Boost");
        }
    }

    public void UseCoffeeBoost()
    {
        if (coffeeBoostOwned > 0)
        {
            Debug.Log("Used a Coffee Boost");

            coffeeBoostOwned--;
            coffeeBoostOwnedText.text = "Owned: " + coffeeBoostOwned;
        }

        else
        {
            Debug.Log("No Coffee Boost to use");
        }
    }

    public void BuyTelevision()
    {
        if (buyBCManager.totalBcStock >= 35)
        {
            Debug.Log("Bought a Television");

            buyBCManager.totalBcStock -= 35;
            bcStockText.text = "BC: " + buyBCManager.totalBcStock;

            televisionOwned++;
            televisionOwnedText.text = "Owned: " + televisionOwned;
        }

        else
        {
            Debug.Log("Not enough BC to buy a Television");
        }
    }

    public void UseTelevision()
    {
        if (televisionOwned > 0)
        {
            Debug.Log("Used a Television");

            televisionOwned--;
            televisionOwnedText.text = "Owned: " + televisionOwned;
        }

        else
        {
            Debug.Log("No Television to use");
        }
    }
}
