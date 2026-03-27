using UnityEngine;
using UnityEngine.UI;

public class UIPantryInventory : MonoBehaviour
{
    public static UIPantryInventory main;

    [SerializeField]private Image[] playerSlots;
    [SerializeField]private Image[] pantrySlots;
    [SerializeField]private int columns = 6;
    [SerializeField]private Color defaultCol = Color.white;
    [SerializeField]private Color selectedCol = Color.yellow;
    [SerializeField]private ItemIconDataBase iconDb;

    public bool IsOpen { get; private set; }
    private PantryInventory currentPantry;
    private PlayerInventory playerInventory;
    private PlayerMovement playerMovement;
    private int selectedSlot = 0;
    private bool selectingPlayer = true;
    private int row = 0;
    private int col = 0;

    void Awake()
    {
        main = this;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentPantry == null) return;

        if (Input.GetKeyDown(KeyCode.A)) MoveLeft();
        if (Input.GetKeyDown(KeyCode.D)) MoveRight();
        if (Input.GetKeyDown(KeyCode.W)) MoveUp();
        if (Input.GetKeyDown(KeyCode.S)) MoveDown();
        if (Input.GetKeyDown(KeyCode.E)) TransferItem();
        if (Input.GetKeyDown(KeyCode.Escape)) ClosePanel();
    }

    public void OpenPanel(PantryInventory pantry)
    {
        currentPantry = pantry;
        playerInventory = PlayerInventory.main;

        row = 0;
        col = 0;
        selectedSlot = 0;
        selectingPlayer = true;

        playerInventory.OnInventoryChanged += UpdateUI;
        currentPantry.OnPantryChanged += UpdateUI;

        playerMovement.AllowMovement(false);

        IsOpen = true;
        gameObject.SetActive(true);

        UpdateUI();
    }

    private void ClosePanel()
    {
        if (playerInventory != null)
            playerInventory.OnInventoryChanged -= UpdateUI;

        if (currentPantry != null)
            currentPantry.OnPantryChanged -= UpdateUI;
        
        playerMovement.AllowMovement(true);

        IsOpen = false;
        gameObject.SetActive(false);
        currentPantry = null;
    }

    private void TransferItem()
    {
        if (selectingPlayer)
        {
            if (selectedSlot >= playerInventory.GetTotalItemCount()) 
                return;
            
            ItemType item = playerInventory.GetItem(selectedSlot);
            
            if (currentPantry.AddItem(item))
            {
                playerInventory.RemoveItem(item);
            }
        }
        else
        {
            if (selectedSlot >= currentPantry.GetItemCount()) 
                return;
            
            ItemType item = currentPantry.GetItem(selectedSlot);

            if (playerInventory.AddItem(item))
            {
                currentPantry.RemoveItem(item);
            }
        }

        UpdateUI();
    }

    private void MoveUp()
    {
        row--;

        if (row < 0)
        {
            selectingPlayer = !selectingPlayer;
            row = GetMaxRows() - 1;
        }

        UpdateUI();
    }

    private void MoveDown()
    {
        row++;

        if (row >= GetMaxRows())
        {
            selectingPlayer = !selectingPlayer;
            row = 0;
        }

        UpdateUI();
    }
    private void MoveRight()
    {
        col = Mathf.Min(columns - 1, col + 1);
        UpdateUI();
    }

    private void MoveLeft()
    {
        col = Mathf.Max(0, col - 1);
        UpdateUI();
    }

    private int GetMaxRows()
    {
        int count;

        if (selectingPlayer)
        {
            count = playerSlots.Length;
        }
        else
        {
            count = pantrySlots.Length;
        }

        float rows = (float)count / columns; //divide

        int result = Mathf.CeilToInt(rows); //rounded up

        return result;
    }

    void UpdateUI()
    {
        if (playerInventory == null || currentPantry == null) return;

        selectedSlot = row * columns + col;

        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (i < playerInventory.GetTotalItemCount())
            {
                playerSlots[i].sprite = iconDb.GetIcon(playerInventory.GetItem(i));
            }
            else
            {
                playerSlots[i].sprite = null;
            }

            if (selectingPlayer && i == selectedSlot)
                playerSlots[i].color = selectedCol;
            else
                playerSlots[i].color = defaultCol;
        }
        for (int i = 0; i < pantrySlots.Length; i++)
        {
            if (i < currentPantry.GetItemCount())
            {
                pantrySlots[i].sprite = iconDb.GetIcon(currentPantry.GetItem(i));
                pantrySlots[i].enabled = true;
            }
            else
            {
                pantrySlots[i].enabled = false;
            }

            if (!selectingPlayer && i == selectedSlot)
                pantrySlots[i].color = selectedCol;
            else
                pantrySlots[i].color = defaultCol;
        }
    }
}
