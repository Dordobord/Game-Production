using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager main;

    [SerializeField] private TextMeshProUGUI tutorialText;

    private TutorialStep currentStep;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        StartTutorial();
    }

    private void StartTutorial()
    {
        currentStep = TutorialStep.GetOrder;
        UpdateTutorialText();
    }

    public void OnOrderTaken()
    {
        if (currentStep != TutorialStep.GetOrder) return;

        currentStep = TutorialStep.PrepareFood;
        UpdateTutorialText();
    }

    public void OnFoodPrepared()
    {
        if (currentStep != TutorialStep.PrepareFood) return;

        currentStep = TutorialStep.AssembleFood;
        UpdateTutorialText();
    }

    public void OnFoodAssembled()
    {
        if (currentStep != TutorialStep.AssembleFood) return;

        currentStep = TutorialStep.ServeFood;
        UpdateTutorialText();
    }

    public void OnCustomerServed()
    {
        if (currentStep != TutorialStep.ServeFood) return;

        currentStep = TutorialStep.Complete;
        UpdateTutorialText();
    }


    private void UpdateTutorialText()
    {
        if (tutorialText == null) return;

        switch (currentStep)
        {
            case TutorialStep.GetOrder:
                tutorialText.text = "Step 1 : Talk to the customer to take their order.";
                break;

            case TutorialStep.PrepareFood:
                tutorialText.text = "Step 2 : Prepare the required ingredients.";
                break;

            case TutorialStep.AssembleFood:
                tutorialText.text = "Step 3 : Assemble the food at the assembly station.";
                break;

            case TutorialStep.ServeFood:
                tutorialText.text = "Step 4 : Serve the food to the customer.";
                break;

            case TutorialStep.Complete:
                tutorialText.text = "Tutorial complete! You are ready.";
                break;
        }
    }
}