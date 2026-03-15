using UnityEngine;

public class PantryInteract : MonoBehaviour, IInteractable
{
    [SerializeField]private PantryInventory pantry;
    public void Interact()
    {
        UIPantryInventory.main.OpenPanel(pantry);
    }
}
