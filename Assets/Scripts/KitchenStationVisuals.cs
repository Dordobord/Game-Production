using UnityEngine;

public class KitchenStationVisual : MonoBehaviour
{
    [SerializeField] private KitchenStation station;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        station.OnStartCooking += StartCooking;
        station.OnFinishCooking += StopCooking;
        station.OnClear += StopCooking;
    }

    private void OnDestroy()
    {
        station.OnStartCooking += StartCooking;
        station.OnFinishCooking += StopCooking;
        station.OnClear += StopCooking;
    }

    private void StartCooking()
    {
        anim.SetBool("IsCooking", true);
    }

    private void StopCooking()
    {
        anim.SetBool("IsCooking", false);
    }
}