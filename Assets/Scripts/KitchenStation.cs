using UnityEngine;
using System.Collections;

public class KitchenStation : MonoBehaviour, IInteractable
{
    [Header("Station Settings")]
    [SerializeField] private ItemType inputItem;
    [SerializeField] private ItemType outputItem;
    [SerializeField] private float processingTime = 3.5f;
    [SerializeField] private bool sendToPlateRack = false;
    [SerializeField] private PlateRack plateRack;


    private bool isProcessing = false;
    private ItemType? currentItem = null;

    private PlayerInventory playerInventory;

    [System.Obsolete]
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
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
        Debug.Log("Processing " + inputItem + "...");

        yield return new WaitForSeconds(processingTime);

        currentItem = outputItem;
        isProcessing = false;

        Debug.Log("Finished " + outputItem + "!");
    }
}
