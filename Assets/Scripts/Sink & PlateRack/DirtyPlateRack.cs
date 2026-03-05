using UnityEngine;

public class DirtyPlateRack : MonoBehaviour
{
    public static DirtyPlateRack main { get; private set; }
    [SerializeField] private int dirtyPlatesCount = 0;

    public int GetCount() => dirtyPlatesCount;

    void Awake()
    {
        main = this;
    }

    public void IncreasePlate()
    {
        dirtyPlatesCount++;
    }

    public bool TakePlate()
    {
        if (dirtyPlatesCount > 0)
        {
            dirtyPlatesCount--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
