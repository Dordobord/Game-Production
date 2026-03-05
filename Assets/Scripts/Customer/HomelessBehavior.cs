using System.Collections;
using UnityEngine;

public class HomelessBehavior : MonoBehaviour
{
    [Header("Homeless Settings")]
    [SerializeField] private float washingTime;
    [SerializeField] private float timeBetweenWash;
    [SerializeField] private int platesToWash;
    private int washedCount = 0;
    private bool isWorking = false;

    private IEnumerator WorkCycle()
    {
        // The loop keeps running until the homeless person hits their plate limit
        while (washedCount < platesToWash)
        {
            if (DirtyPlateRack.main.GetCount() > 0)
            {
                if (DirtyPlateRack.main.TakePlate())
                {
                    yield return StartCoroutine(WashPlate());
                    CleanPlateRack.main.IncreasePlate();
                    washedCount++;
                    Debug.Log($"Homeless washed plate {washedCount}/{platesToWash}");
                }
            }
            else
            {
                int currentQuota = CustomerSpawner.main.GetQuotaCount();
                int maxQuota = CustomerSpawner.main.GetMaxQuota();

                // If homeless is the only person left, they will leave
                if (currentQuota >= maxQuota - 1)
                {
                    GetComponent<CustomerBehavior>().ChangeState(CustomerState.Leaving);
                    yield break;
                }
                else
                    Debug.Log("Homeless waiting for more dirty plates...");
            }

            // Wait before checking the rack again
            yield return new WaitForSeconds(timeBetweenWash);
        }

        // Once the limit is reached, leave the restaurant
        isWorking = false;
        SinkStation.main.SetOccupancy(false);
        GetComponent<CustomerBehavior>().ChangeState(CustomerState.Leaving);
    }

    private IEnumerator WashPlate()
    {
        UIDurationBar durationBar = SinkStation.main.GetDurationBar();
        durationBar.EnableBar(washingTime);

        float timer = washingTime;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            durationBar.UpdateValue(timer);
            yield return null;
        }
    }

    public void MoveToSink(Transform customerPosition)
    {
        Transform sinkLocation = SinkStation.main.GetSinkTransform();
        customerPosition.position = sinkLocation.position;

        if (!isWorking)
        {
            isWorking = true;
            SinkStation.main.SetOccupancy(true);
            StartCoroutine(WorkCycle());
        }
    }
}
