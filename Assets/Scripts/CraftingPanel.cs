using UnityEngine;
using TMPro;

public class CraftingPanel : MonoBehaviour
{
    public TextMeshProUGUI craftName;
    public SlotManager slotManager;
    public TextMeshProUGUI craftIngredients;

    public ItemObject craftItem;

    public Crafting crafting;

    public void CraftButton()
    {
        crafting = transform.parent.GetComponent<GetCrafting>().GetCraftingFunction();

        crafting.Craft(craftItem);
    }
}