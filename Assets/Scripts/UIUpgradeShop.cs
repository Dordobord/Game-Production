using TMPro;
using UnityEngine;

public class UIUpgradeShop : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI phoneTxt;

    private bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OpenVault()
    {
        if (panel == null || phoneTxt == null)
            return;

        isOpen = true;
        panel.SetActive(true);
    }

    public void CloseVault()
    {
        if (panel == null)
            return;

        isOpen = false;
        panel.SetActive(false);
    }
}
