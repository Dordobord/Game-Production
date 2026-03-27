using UnityEngine;

public class BeverageStationVisual : MonoBehaviour
{
    [SerializeField] private BeverageStation station;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        station.OnStartPreparing += StartPreparing;
        station.OnFinishPreparing += StopPreparing;
        station.OnClear += StopPreparing;
    }

    private void OnDestroy()
    {
        station.OnStartPreparing -= StartPreparing;
        station.OnFinishPreparing -= StopPreparing;
        station.OnClear -= StopPreparing;
    }

    private void StartPreparing()
    {
        anim.SetBool("IsBrewing", true);
    }

    private void StopPreparing()
    {
        anim.SetBool("IsBrewing", false);
    }
}