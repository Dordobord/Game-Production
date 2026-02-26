using UnityEngine;
using System.Collections;

public class CoffeeMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private float brewTime = 5f;

    private bool isBrewing = false;
    private bool coffeeReady = false;
    private Color origCol;
    private SpriteRenderer sr;

    private PlayerInventory playerInventory;

    [System.Obsolete]
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        sr = GetComponent<SpriteRenderer>();

        origCol = sr.color;
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        if (isBrewing)
        {
            Debug.Log("Coffee is brewing...");
            return;
        }

        if (coffeeReady)
        {
            bool added = playerInventory.AddItem(ItemType.CoffeeCup);

            if (added)
            {
                Debug.Log("Collected Coffee!");
                coffeeReady = false;
            }
            else
            {
                Debug.Log("Inventory full! Cannot take coffee.");
            }

            return;
        }

        StartCoroutine(BrewCoffee());
    }

    private IEnumerator BrewCoffee()
    {
        isBrewing = true;
        sr.color = Color.blue;
        Debug.Log("Brewing coffee...");

        yield return new WaitForSeconds(brewTime);

        isBrewing = false;
        coffeeReady = true;
        sr.color = origCol;
        Debug.Log("Coffee is ready!");
    }
}
