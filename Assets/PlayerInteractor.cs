using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInteractor : MonoBehaviour
{
    [Header("Interactor")]
    [SerializeField]private float interactRange = .5f;
    [SerializeField] private LayerMask interactMask;

    private void Update()
    {
        DetectObject();
        if (Input.GetKey(KeyCode.E))
        {

        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, interactRange, interactMask);

        Debug.DrawRay(transform.position, Vector2.up * interactRange,Color.green);

        if (hit.collider != null)
        {
            Debug.Log("Raycast is working");
        }
        else
        {
            Debug.Log("raycast not hitting");
        }
    }

    private void DetectObject()
    {
        
    }
}