using UnityEngine;

public class CoffeeMachineVisual : MonoBehaviour
{
    [SerializeField] private CoffeeMachine coffee;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        coffee.OnStartBrewing += StartBrewing;
        coffee.OnFinishBrewing += StopBrewing;
        coffee.OnClear += StopBrewing;
    }

    private void OnDestroy()
    {
        coffee.OnStartBrewing -= StartBrewing;
        coffee.OnFinishBrewing -= StopBrewing;
        coffee.OnClear -= StopBrewing;
    }

    private void StartBrewing()
    {
        anim.SetBool("IsBrewing", true);
    }

    private void StopBrewing()
    {
        anim.SetBool("IsBrewing", false);
    }
}