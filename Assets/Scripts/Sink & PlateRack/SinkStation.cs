using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class SinkStation : MonoBehaviour, IInteractable
{
    public static SinkStation main { get; private set; }

    [Header("Sink Settings")]
    [SerializeField] private float washingTime;
    [SerializeField] private Transform sinkLocation;

    [Header("Reference")]
    [SerializeField] private UIDurationBar durationBar;
    [SerializeField] private CleanPlateRack rack;
    [SerializeField] private DirtyPlateRack dirtyRack;

    private bool isWashing = false;
    private bool isOccupied = false;


    public void SetOccupancy(bool newBool) => isOccupied = newBool;
    public Transform GetSinkTransform() => sinkLocation;
    public UIDurationBar GetDurationBar() => durationBar;

    public void Interact()
    {
        if (!isWashing && !isOccupied)
        {
            StartCoroutine(WashPlate());
        }
    }

    void Awake()
    {
        main = this;
    }

    private IEnumerator WashPlate()
    {
        isWashing = true;

        if (dirtyRack.TakePlate())
        {
            PlayerMovement.main.AllowMovement(false);
            PlayerMovement.main.gameObject.transform.position = sinkLocation.position;

            float speed;
            speed = PlayerStats.main.Efficiency;
            float finalTime = washingTime / speed;

            durationBar.EnableBar(finalTime);

            float timer = finalTime;

            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                durationBar.UpdateValue(timer);
                yield return null;
            }

            rack.IncreasePlate();
            PlayerMovement.main.AllowMovement(true);
            Debug.Log("Finsihed washing a plate");
        }
        else
        {
            Debug.Log("No dirty plate on the rack.");
        }

        isWashing = false;
    }
}
