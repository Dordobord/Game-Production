using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SeatInstance
{
    public Transform seatPoint;
    public CustomerBehavior customer;
    public bool isOccupied;
}

public class Table : MonoBehaviour
{
    [SerializeField] private List<SeatInstance> tableSeats = new List<SeatInstance>();

    public bool IsTableFull { get; private set; }
    
    public void CheckAvailableSeats()
    {
        int available = 0;
        foreach(SeatInstance seat in tableSeats)
        {
            if(seat != null && seat.seatPoint != null)
            {
                if (!seat.isOccupied)
                {
                    available++;
                }
            }
        }

        if(available < 1) // no available seats
            IsTableFull = true;
        else
            IsTableFull = false;
        
    }

    public SeatInstance GetAvailableSeat()
    {
        foreach(SeatInstance seat in tableSeats)
        {
            if(seat != null && seat.seatPoint != null)
            {
                if (!seat.isOccupied)
                {
                    return seat;
                }
            }
        }

        return null;
    }

    public Transform AssignSeatToCustomer(CustomerBehavior customer)
    {
        SeatInstance seat = GetAvailableSeat();

        if(seat == null || seat.seatPoint == null) return null;

        seat.isOccupied = true;
        seat.customer = customer;
        CheckAvailableSeats();

        Debug.Log($"Customer taken a seat at {gameObject.name}");
        return seat.seatPoint;
    }

    public void ClearSeat(CustomerBehavior customer)
    {
        foreach(SeatInstance seat in tableSeats)
        {
            if(seat != null && seat.seatPoint != null)
            {
                if (seat.isOccupied && seat.customer == customer)
                {
                    seat.isOccupied = false;
                    seat.customer = null;
                }
            }
        }
        
        CheckAvailableSeats();
    }
}
