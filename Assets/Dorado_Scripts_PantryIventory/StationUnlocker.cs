using UnityEngine;

public class StationUnlocker : MonoBehaviour
{
    [SerializeField] private UpgradeSO upgrade;
    [SerializeField] private SpriteRenderer sprite; 

    void Start()
    {
        Refresh();

        if (upgrade != null)
            upgrade.OnUpgradeUnlocked += Refresh;
    }

    void OnDestroy()
    {
        if (upgrade != null)
            upgrade.OnUpgradeUnlocked -= Refresh;
    }

    public void Refresh()
    {
        if (upgrade == null) return;

        sprite.enabled = upgrade.IsUnlocked;
    }
}