using UnityEngine;
using System.Collections;
using System;

public class KitchenStation : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public struct KitchenRecipe
    {
        public ItemType[] inputs;
        public ItemType output;
    }

    [Header("Recipes")]
    [SerializeField] private KitchenRecipe[] recipes;

    [Header("Station Settings")]
    [SerializeField] private float processingTime = 3.5f;
    [SerializeField] private bool sendToPlateRack = false;
    [SerializeField] private PlateRack plateRack;
    [SerializeField] private UIDurationBar durationBar;
    [SerializeField] private UpgradeSO speedUpgrade;
    [SerializeField]private PremiumItemType boostType;

    private bool isProcessing = false;
    private bool hasItem = false;
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
        if (!hasItem)
        {
            foreach (var recipe in recipes)
            {
                bool hasAllIngredients = true;

                foreach (var input in recipe.inputs)
                {
                    int required = 0;
                    foreach (var i in recipe.inputs)
                    {
                        if (i.Equals(input))
                            required++;
                    }

                    if (playerInventory.GetItemCount(input) < required)
                    {
                        hasAllIngredients = false;
                        break;
                    }
                }

                if (hasAllIngredients)
                {
                    foreach (var input in recipe.inputs)
                    {
                        playerInventory.RemoveItem(input);
                    }

                    OnStartCooking?.Invoke();

                    bool hasBoost = !DayManager.main.isPrepPhase && BoostManager.main.IsBoostActive(boostType);
                    if (hasBoost)//instantly give when boosted zzz
                    {
                        Debug.Log("Boost Active");
                        
                        bool added = playerInventory.AddItem(recipe.output);
                        if (added)
                        {
                            OnFinishCooking?.Invoke();
                            OnClear?.Invoke();
                        }
                        else
                        {
                            currentOutput = recipe.output;
                            hasItem = true;
                        }
                        return;
                    }  
                    currentOutput = recipe.output;
                    hasItem = true;

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
                hasItem = false;
            }
            else
            {
                bool added = playerInventory.AddItem(currentOutput);

                if (added)
                {
                    hasItem = false;
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

        if (speedUpgrade != null)
        {
            reduction = speedUpgrade.GetValue();
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