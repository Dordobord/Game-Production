using UnityEngine;
using TMPro;

public class UICookbook : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI recipeText;

    private bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OpenCookbook(Recipe[] recipes)
    {
        if (panel == null || recipeText == null)
            return;

        isOpen = true;
        panel.SetActive(true);

        ShowRecipes(recipes);
    }

    public void CloseCookbook()
    {
        if (panel == null)
            return;

        isOpen = false;
        panel.SetActive(false);
    }

    private void ShowRecipes(Recipe[] recipes)
    {
        recipeText.text = "";

        if (recipes == null || recipes.Length == 0)
        {
            recipeText.text = "No recipes available.";
            return;
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            recipeText.text += FormatRecipe(recipes[i]) + "\n";
        }
    }

    private string FormatRecipe(Recipe recipe) //gets item in the recipe list and converts it into string format hehe.
    {
        if (recipe == null || recipe.requiredItems == null || recipe.requiredItems.Length == 0) return "";

        string ingredients = "";

        for (int i = 0; i < recipe.requiredItems.Length; i++) //loop through to check for items to be added 
        {
            ingredients += recipe.requiredItems[i].ToString();

            if (i < recipe.requiredItems.Length - 1) ingredients += " + ";
        }

        return ingredients + " = " + recipe.resultItem;
    }
}