using UnityEngine;
using System.Collections;
using System;

public class CoffeeMachine : MonoBehaviour, IInteractable
{
    [SerializeField]private float brewTime = 5f;
    [SerializeField]private UIDurationBar durationBar;
    [SerializeField]private RestaurantUpgradeSO brewSpeedUpgrade;

    private bool isBrewing = false;
    private bool coffeeReady = false;
    private PlayerInventory playerInventory;

    public event Action OnStartBrewing;
    public event Action OnFinishBrewing;
    public event Action OnClear;

    void Start()
    {
        playerInventory = PlayerInventory.main;
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
            return;
        }

        StartCoroutine(BrewCoffee());
    }

    private IEnumerator BrewCoffee()
    {
        isBrewing = true;
        OnStartBrewing?.Invoke();

        float speed = PlayerStats.main.Efficiency;

        float reduction = 0f;
        if (brewSpeedUpgrade != null)
        {
            reduction = brewSpeedUpgrade.GetValue();
        }

        float finalTime = Mathf.Max(0.1f, (brewTime - reduction) / speed);

        durationBar.EnableBar(finalTime);

        float timer = finalTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }

        isBrewing = false;
        coffeeReady = true;

        OnFinishBrewing?.Invoke();
    }
}
