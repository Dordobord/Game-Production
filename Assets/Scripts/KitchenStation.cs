using UnityEngine;
using System.Collections;
using System;

public class KitchenStation : MonoBehaviour, IInteractable
{
    [Header("Station Settings")]
    [SerializeField] private ItemType inputItem;
    [SerializeField] private ItemType outputItem;
    [SerializeField] private float processingTime = 3.5f;
    [SerializeField] private bool sendToPlateRack = false;
    [SerializeField] private PlateRack plateRack;
    [SerializeField] private UIDurationBar durationBar;

    private bool isProcessing = false;
    private PlayerStats playerStats;
    private ItemType? currentItem = null;
    private PlayerInventory playerInventory;

    public event Action OnStartCooking;
    public event Action OnFinishCooking;
    public event Action OnClear;

    [System.Obsolete]
    void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        // If currently cooking, do nothing
        if (isProcessing)
        {
            Debug.Log("Still processing...");
            return;
        }

        //Station is empty
        if (currentItem == null)
        {
            if (playerInventory.HasItem(inputItem))
            {
                playerInventory.RemoveItem(inputItem);
                currentItem = inputItem;

                OnStartCooking?.Invoke();
                StartCoroutine(ProcessItem());
            }
            else
            {
                Debug.Log("You need a " + inputItem + " to use this station.");
            }
        }
        //Station has finished item ready
        else if (currentItem == outputItem)
        {
            if (sendToPlateRack && plateRack != null)
            {
                plateRack.AddPlate();
                Debug.Log("Sent " + outputItem + " to plate rack.");
                currentItem = null;
            }
            else
            {
                bool added = playerInventory.AddItem(outputItem);

                if (added)
                {
                    Debug.Log("Collected " + outputItem);
                    currentItem = null;
                    OnClear?.Invoke();
            }
                else
                {
                    Debug.Log("Inventory full! Cannot collect " + outputItem);
                }
            }
        }

    }

    private IEnumerator ProcessItem()
    {
        isProcessing = true;

        float speed;
        speed = PlayerStats.main.Efficiency;
        float finalTime = processingTime / speed;

        durationBar.EnableBar(finalTime);

        float timer = finalTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }
        // Debug.Log("Processing " + inputItem + "...");

        // yield return new WaitForSeconds(processingTime);

        currentItem = outputItem;
        isProcessing = false;
        OnFinishCooking?.Invoke();
        Debug.Log("Finished " + outputItem + "!");
    }

    
}
