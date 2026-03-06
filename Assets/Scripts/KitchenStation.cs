using UnityEngine;
using System.Collections;
using System;

public class KitchenStation : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public struct KitchenRecipe
    {
        public ItemType input;
        public ItemType output;
    }

    [Header("Recipes")]
    [SerializeField] private KitchenRecipe[] recipes;

    [Header("Station Settings")]
    [SerializeField] private float processingTime = 3.5f;
    [SerializeField] private bool sendToPlateRack = false;
    [SerializeField] private PlateRack plateRack;
    [SerializeField] private UIDurationBar durationBar;

    private bool isProcessing = false;
    private ItemType? currentInput = null;
    private ItemType currentOutput;

    private PlayerInventory playerInventory;

    public event Action OnStartCooking;
    public event Action OnFinishCooking;
    public event Action OnClear;

    void Awake()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        // If currently cooking, do nothing
        if (isProcessing)
            return;

        // Station is empty
        if (currentInput == null)
        {
            foreach (var recipe in recipes)
            {
                if (playerInventory.HasItem(recipe.input))
                {
                    playerInventory.RemoveItem(recipe.input);

                    currentInput = recipe.input;
                    currentOutput = recipe.output;

                    OnStartCooking?.Invoke();
                    StartCoroutine(ProcessItem());
                    return;
                }
            }
        }
        // Station has finished item ready
        else
        {
            if (sendToPlateRack && plateRack != null)
            {
                plateRack.AddPlate();
                currentInput = null;
            }
            else
            {
                bool added = playerInventory.AddItem(currentOutput);

                if (added)
                {
                    currentInput = null;
                    OnClear?.Invoke();
                }
            }
        }
    }

    private IEnumerator ProcessItem()
    {
        isProcessing = true;

        float speed = PlayerStats.main.Efficiency;
        float reduction = 0;

        if (currentOutput == ItemType.CookedFries || currentOutput == ItemType.CookedChicken)
        {
            reduction = UpgradeManager.main.Fryer.GetValue();
        }

        if (currentOutput == ItemType.CookedBeef)
        {
            reduction = UpgradeManager.main.Grill.GetValue();
        }

        float finalTime = Mathf.Max(0.1f, (processingTime - reduction) / speed);

        durationBar.EnableBar(finalTime);

        float timer = finalTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }

        isProcessing = false;

        OnFinishCooking?.Invoke();
    }
}