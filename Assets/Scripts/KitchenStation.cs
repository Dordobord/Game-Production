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
    private PlayerStats playerStats;
    private Color origCol;
    private ItemType? currentItem = null;
    private SpriteRenderer sr;
    private PlayerInventory playerInventory;

    [System.Obsolete]
    void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerStats = FindObjectOfType<PlayerStats>();
        sr = GetComponent<SpriteRenderer>();

        origCol = sr.color;
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
        sr.color = Color.blue;

        float speed;
        speed = PlayerStats.main.Efficiency;

        float finalTime = processingTime / speed;
        Debug.Log("Processing " + inputItem + "...");

        yield return new WaitForSeconds(processingTime);

        currentItem = outputItem;
        isProcessing = false;
        sr.color = origCol;
        Debug.Log("Finished " + outputItem + "!");
    }
}
