using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public int selectedInventorySlot;

    public int selectedHotbarSlot;

    // Make private
    public SlotManager[] inventorySlots;

    // Make private
    public SlotManager[] hotbarSlots;

    public InventoryObject inventory;

    int inventorySlotsAmount = 72;

    //int hotbarSlotsAmount = 6;

    public GameObject inventorySlotsParent;

    public GameObject hotbarSlotsParent;

    // Show inventory
    public GameObject inventoryObject;
    public GameObject craftingObject;

    public Crafting crafting;
    public DisplayCraftingPanel displayCraftingPanel;

    public bool inventoryIsOpen;

    public PlayerMovement moveScript;

    void Start()
    {
        inventorySlots = inventorySlotsParent.GetComponentsInChildren<SlotManager>();
        selectedInventorySlot = 0;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].index = i;
        }

        selectedHotbarSlot = 0;
        hotbarSlots = hotbarSlotsParent.GetComponentsInChildren<SlotManager>();

        // Show Inventory
        inventoryObject.SetActive(false);
        craftingObject.SetActive(false);

        moveScript.enabled = true;
    }

    void Update()
    {
        if (inventoryIsOpen)
        {
            // Highlight slot
            GetSelectedInventorySlot();
            inventorySlots[selectedInventorySlot].HighlightSlot();

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (i != selectedInventorySlot)
                {
                    inventorySlots[i].UnhighlightSlot();
                }
            }

            // Item to hotbar
            if (GetSelectedHotbarSlot())
            {
                ItemToHotbar();
            }
        }
        else
        {
            // Highlight slot
            GetSelectedHotbarSlot();
            hotbarSlots[selectedHotbarSlot].HighlightSlot();
            for (int i = 0; i < hotbarSlots.Length; i++)
            {
                if (i != selectedHotbarSlot)
                {
                    hotbarSlots[i].UnhighlightSlot();
                }
            }
        }

        // Show inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedInventorySlot = 0;

            crafting.UpdateCrafts();
            displayCraftingPanel.UpdateCraftingUI();

            inventoryObject.SetActive(!inventoryObject.activeSelf);
            craftingObject.SetActive(!craftingObject.activeSelf);

            moveScript.enabled = !inventoryObject.activeSelf;

            if (inventoryObject.activeSelf)
            {
                inventoryIsOpen = true;
            }
            else
            {
                inventoryIsOpen = false;
            }

            if (inventoryIsOpen)
            {
                for (int i = 0; i < hotbarSlots.Length; i++)
                {
                    hotbarSlots[i].UnhighlightSlot();
                }
            }
        }
    }

    public void UpdateUI()
    {
        // Inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < 72 && inventory.GetInventoryAmountAt(i) != 0)
            {
                inventorySlots[i].SetSlot(inventory.GetInventoryItemAt(i), inventory.GetInventoryAmountAt(i));
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }

        // Hotbar
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < 6 && inventory.GetHotbarAmountAt(i) != 0)
            {
                hotbarSlots[i].SetSlot(inventory.GetHotbarItemAt(i), inventory.GetHotbarAmountAt(i));
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }

        //inventory.PrintContents();
    }

    public void GetSelectedInventorySlot()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedInventorySlot++;
            if (selectedInventorySlot >= inventorySlotsAmount)
            {
                selectedInventorySlot = 0;
            }
        }
    }

    bool GetSelectedHotbarSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedHotbarSlot = 0;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedHotbarSlot = 1;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedHotbarSlot = 2;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedHotbarSlot = 3;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedHotbarSlot = 4;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedHotbarSlot = 5;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClickSlot(int i)
    {
        selectedInventorySlot = i;
    }

    public void ItemToHotbar()
    {
        //Hotbar item is succesfully going to inventory, but inventory item is increasing when going to hotbar

        //Store inventory item as newItem
        ItemObject newItem = inventory.GetInventoryItemAt(selectedInventorySlot);

        if (newItem == null)
            return;

        //Store inventory amount as newAmount
        int newAmount = inventory.GetInventoryAmountAt(selectedInventorySlot);
        //Debug.Log("newAmount = " + newAmount);

        //Undefined InventorySlot
        //InventorySlot tempItem;
        ItemObject tempItem;
        int tempAmount;

        // If there is an item in the desired hotbar slot
        if (hotbarSlots[selectedHotbarSlot].item != null)
        {
            //tempItem = hotbar item
            //tempItem = new InventorySlot(hotbarSlots[selectedHotbarSlot].item, hotbarSlots[selectedHotbarSlot].amount);
            tempItem = hotbarSlots[selectedHotbarSlot].item;
            tempAmount = hotbarSlots[selectedHotbarSlot].amount;
            //Debug.Log(tempItem);

            //Add inventory item to hotbar
            inventory.SetHotbarItemAt(selectedHotbarSlot, newItem, newAmount);

            //Add temp hotbar item to inventory
            inventory.SetInventoryItemAt(selectedInventorySlot, tempItem, tempAmount);
        }
        else
        {
            //Add inventory item to hotbar
            inventory.SetHotbarItemAt(selectedHotbarSlot, newItem, newAmount);

            //Remove selected inventory item
            inventory.AddItemToInventoryAt(selectedInventorySlot, newItem, -newAmount);
        }
        UpdateUI();
    }

    //Functions return selected item and amount

    public ItemObject GetSelectedItem()
    {
        return inventory.GetHotbarItemAt(selectedHotbarSlot);
    }
    
    public int GetSelectedAmount()
    {
        return inventory.GetHotbarAmountAt(selectedHotbarSlot);
    }
}