using System.Collections.Generic;
using UnityEngine;

public class DisplayCraftingPanel : MonoBehaviour
{
    public Crafting crafting;

    public GameObject panelPrefab;

    public GameObject parentObject;

    List<GameObject> panelInstances = new List<GameObject>();

    public void UpdateCraftingUI()
    {
        for (int i = 0; i < panelInstances.Count; i++)
        {
            Destroy(panelInstances[i]);
        }
        panelInstances.Clear();

        for (int i = 0; i < crafting.craftableItems.Count; i++)
        {
            GameObject currentInstance = Instantiate(panelPrefab);
            panelInstances.Add(currentInstance);
            currentInstance.transform.SetParent(parentObject.transform, false);

            CraftingPanel craftingPanel = currentInstance.GetComponent<CraftingPanel>();

            craftingPanel.craftName.text = crafting.craftableItems[i].itemName;
            craftingPanel.slotManager.icon.sprite = crafting.craftableItems[i].itemIcon;
            craftingPanel.slotManager.icon.enabled = true;

            craftingPanel.craftIngredients.text = CreateIngredientText(i);

            craftingPanel.craftItem = crafting.craftableItems[i];
        }
    }

    string CreateIngredientText(int itemIndex)
    {
        string ingredientText = "";

        for (int i = 0; i < crafting.craftableItems[itemIndex].ingredients.Length; i++)
        {
            ingredientText = ingredientText + System.Environment.NewLine + crafting.craftableItems[itemIndex].ingredients[i].itemName + " x " + crafting.craftableItems[itemIndex].ingredientCount[i];
        }
        return ingredientText;
    }
}
