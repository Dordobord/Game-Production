using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    public void OpenCookbook(List<ItemMenuData> recipes)
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

    private void ShowRecipes(List<ItemMenuData> recipes)
    {
        recipeText.text = "";

        if (recipes == null || recipes.Count == 0)
        {
            recipeText.text = "No recipes available.";
            return;
        }

        for (int i = 0; i < recipes.Count; i++)
        {
            recipeText.text += FormatRecipe(recipes[i]) + "\n";
        }
    }

    private string FormatRecipe(ItemMenuData recipe) //gets item in the recipe list and converts it into string format hehe.
    {
        if (recipe == null || recipe.ingredients == null || recipe.ingredients.Count == 0) return "";

        string ingredients = "";

        for (int i = 0; i < recipe.ingredients.Count; i++) //loop through to check for items to be added 
        {
            ingredients += recipe.ingredients[i].ToString();

            if (i < recipe.ingredients.Count - 1) ingredients += " + ";
        }

        return ingredients + " = " + recipe.dishItem;
    }
}