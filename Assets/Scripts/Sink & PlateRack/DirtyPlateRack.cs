using Unity.VisualScripting;
using UnityEngine;

public class DirtyPlateRack : MonoBehaviour, IInteractable
{
    public static DirtyPlateRack main { get; private set; }
    [SerializeField] private int dirtyPlatesCount = 0;
    
    [SerializeField] private SpriteRenderer rackSpriteRenderer;
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;

    public int GetCount() => dirtyPlatesCount;

    void Awake()
    {
        main = this;
        UpdateVisuals();
    }

    public void IncreasePlate()
    {
        dirtyPlatesCount++;
        UpdateVisuals();
    }

    public void Interact()
    {
        PlayerInventory player = PlayerInventory.main;

        if (player == null) return;

        if (player.HasItem(ItemType.DirtyPlate)) // player has dirty plate on their inventory
        {
            player.RemoveItem(ItemType.DirtyPlate); // return to rack
            dirtyPlatesCount++;
        }
        else // player does not have any dirty plate
        {
            if (dirtyPlatesCount > 0)
            {
                if (player.AddItem(ItemType.DirtyPlate))
                    dirtyPlatesCount--;
                else
                    Debug.Log("Inventory full.");
            }
            else
            {
                Debug.Log("No plates available.");
            }
        }
        
        UpdateVisuals();
    }

    public bool TakePlate()
    {
        if (dirtyPlatesCount > 0)
        {
            dirtyPlatesCount--;
            UpdateVisuals();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateVisuals()
    {
        if(dirtyPlatesCount < 1) // empty plate rack
        {
            rackSpriteRenderer.sprite = emptySprite;
        }
        else // have at least 1 plate
        {
            rackSpriteRenderer.sprite = fullSprite;
        }
    }
}
