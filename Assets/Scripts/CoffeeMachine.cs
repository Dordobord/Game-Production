using UnityEngine;
using System.Collections;
using System;

public class CoffeeMachine : MonoBehaviour, IInteractable
{
    [SerializeField]private float brewTime = 5f;
    [SerializeField]private UIDurationBar durationBar;

    private bool isBrewing = false;
    private bool coffeeReady = false;
    private PlayerInventory playerInventory;

    public event Action OnStartBrewing;
    public event Action OnFinishBrewing;
    public event Action OnClear;

    [System.Obsolete]
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
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
                OnClear?.Invoke();
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
        OnStartBrewing?.Invoke();

        float speed;
        speed = PlayerStats.main.Efficiency;
        float finalTime = brewTime / speed;

        durationBar.EnableBar(finalTime);

        float timer = finalTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }
        //Debug.Log("Brewing coffee...");
        //yield return new WaitForSeconds(brewTime);

        isBrewing = false;
        coffeeReady = true;
        OnFinishBrewing?.Invoke();
        Debug.Log("Coffee is ready!");
    }
}
