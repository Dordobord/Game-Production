using UnityEngine;
using System.Collections;
using System;

public class BeverageStation : MonoBehaviour, IInteractable
{
    [SerializeField] private float processTime = 5f;
    [SerializeField] private UIDurationBar durationBar;
    [SerializeField] private UpgradeSO speedUpgrade;
    [SerializeField] private ItemType dispensedItem; 

    private bool isPreparing = false;
    private bool itemReady = false;
    private PlayerInventory playerInventory;

    public event Action OnStartPreparing;
    public event Action OnFinishPreparing;
    public event Action OnClear;

    void Start()
    {
        playerInventory = PlayerInventory.main;
    }

    public void Interact()
    {
        if (playerInventory == null)
            return;

        if (isPreparing)
        {
            Debug.Log("Item is dispensing...");
            return;
        }

        if (itemReady)
        {
            bool added = playerInventory.AddItem(dispensedItem);

            if (added)
            {
                Debug.Log("Collected Item!");
                itemReady = false;
                OnClear?.Invoke();
            }
            return;
        }

        StartCoroutine(PrepareDrink());
    }

    private IEnumerator PrepareDrink()
    {
        isPreparing = true;
        OnStartPreparing?.Invoke();

        float speed = PlayerStats.main.Efficiency;

        float reduction = 0f;
        if (speedUpgrade != null && speedUpgrade.IsUnlocked)
        {
            reduction = speedUpgrade.GetValue();
        }

        float finalTime = Mathf.Max(0.1f, (processTime - reduction) / speed);

        durationBar.EnableBar(finalTime);

        float timer = finalTime;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }

        isPreparing = false;
        itemReady = true;

        OnFinishPreparing?.Invoke();
    }
}