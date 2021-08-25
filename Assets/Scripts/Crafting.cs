using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<ItemObject> allItems;
    public List<ItemObject> craftableItems;

    public List<string> test;

    public InventoryObject inventory;

    public InventoryUI inventoryUI;

    public DisplayCraftingPanel displayCrafting;

    // Checking possible crafts
    public void UpdateCrafts()
    {
        craftableItems.Clear();

        // Check each item
        for (int i = 0; i < allItems.Count; i++)
        {
            // If the item is craftable...
            if (allItems[i].isCraftable)
            {
                // Variable that will help check if we have each ingredient
                int ingredientsInInventory = 0;

                // Check each ingredient
                for (int j = 0; j < allItems[i].ingredients.Length; j++)
                {
                    // Make sure that there are items in the inventory
                    if (inventory.GetNumItems() > 0)
                    {
                        // Check inventory for ingredient
                        for (int k = 0; k < 72; k++)
                        {
                            if (inventory.GetInventoryItemAt(k) != null)
                            {
                                // If the current ingredient is the same as the current item in the inventory
                                if (inventory.GetInventoryItemAt(k) == allItems[i].ingredients[j])
                                {
                                    // We found an ingredient in the inventory that can be used for the allItems[i] craft!
                                    // Now check if we have the correct quantity
                                    if (inventory.GetInventoryAmountAt(k) >= allItems[i].ingredientCount[j])
                                    {
                                        // We now know that we have the correct quantity of the item
                                        ingredientsInInventory++;
                                    }
                                }
                            }
                        }
                    }
                }

                // If we have the necessary ingredients
                if (ingredientsInInventory >= allItems[i].ingredients.Length)
                {
                    craftableItems.Add(allItems[i]);
                }
            }
        }

        for (int t = 0; t < craftableItems.Count; t++)
        {
            test.Add(craftableItems[t].itemName);
        }

        test.Sort();
    }

    public void Craft(ItemObject item)
    {
        for (int i = 0; i < item.ingredients.Length; i++)
        {
            inventory.AddItemToInventory(item.ingredients[i], -item.ingredientCount[i]);
        }

        inventory.AddItemToInventory(item, item.craftOutput);
        inventoryUI.UpdateUI();
        UpdateCrafts();
        displayCrafting.UpdateCraftingUI();
    }
}
