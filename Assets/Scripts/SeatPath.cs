using UnityEngine;

public class SeatPath : MonoBehaviour
{
    [SerializeField]private Transform[] waypoints;
    public Transform[] Waypoints => waypoints;
}
