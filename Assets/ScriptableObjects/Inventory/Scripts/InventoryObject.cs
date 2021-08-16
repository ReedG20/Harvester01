using UnityEngine;

//Inventory object class
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField]
    InventorySlot[] inventory = new InventorySlot[72];

    [SerializeField]
    InventorySlot[] hotbar = new InventorySlot[6];

    // Add item
    public void AddItemToInventory(ItemObject _item, int _amount)
    {
        // If its already there, just add to the amount
        for (int i = 0; i < inventory.Length; i++)
        {
            if (GetInventoryItemAt(i) == _item)
            {
                inventory[i].AddAmount(_amount);

                if (inventory[i].GetAmount() <= 0)
                    inventory[i].ClearInventorySlot();

                return;
            }
        }

        // If it isn't there, add a new item
        for (int i = 0; i < inventory.Length; i++)
        {
            if (GetInventoryItemAt(i) == null)
            {
                SetInventoryItemAt(i, _item, _amount);
                return;
            }
        }
        Debug.LogWarning("Not enough space to add: " + _item.itemName + "!");
    }

    public void AddItemToHotbar(ItemObject _item, int _amount)
    {
        // If its already there, just add to the amount
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (GetHotbarItemAt(i) == _item)
            {
                hotbar[i].AddAmount(_amount);

                if (hotbar[i].GetAmount() <= 0)
                    hotbar[i].ClearInventorySlot();

                return;
            }
        }

        // If it isn't there, add a new item
        for (int i = 0; i < inventory.Length; i++)
        {
            if (GetInventoryItemAt(i) == null)
            {
                SetInventoryItemAt(i, _item, _amount);
                return;
            }
        }
        Debug.LogWarning("Not enough space to add: " + _item.itemName + "!");
    }

    // Add item at
    public void AddItemToInventoryAt(int i, ItemObject item, int amount)
    {
        if ((amount + inventory[i].GetAmount()) <= 0)
        {
            inventory[i].ClearInventorySlot();
            return;
        }

        inventory[i].SetItem(item);
        inventory[i].SetAmount(amount + GetInventoryAmountAt(i));
    }

    public void AddItemToHotbarAt(int i, ItemObject item, int amount)
    {
        if ((amount + hotbar[i].GetAmount()) <= 0)
        {
            hotbar[i].ClearInventorySlot();
            return;
        }

        hotbar[i].SetItem(item);
        hotbar[i].SetAmount(amount + GetHotbarAmountAt(i));
    }

    // Set item at
    public void SetInventoryItemAt(int i, ItemObject item, int amount)
    {
        inventory[i].SetItem(item);
        inventory[i].SetAmount(amount);
    }

    public void SetHotbarItemAt(int i, ItemObject item, int amount)
    {
        hotbar[i].SetItem(item);
        hotbar[i].SetAmount(amount);
    }

    // Get item quantity
    public int GetItemQuantity(ItemObject item)
    {
        int itemQuantity = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetItem() == item)
                itemQuantity += inventory[i].GetAmount();
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetItem() == item)
                itemQuantity += inventory[i].GetAmount();
        }

        return itemQuantity;
    }

    // Get item at
    public ItemObject GetInventoryItemAt(int i)
    {
        if (inventory.Length <= i)
            return null;
        return inventory[i].GetItem();
    }

    public ItemObject GetHotbarItemAt(int i)
    {
        if (hotbar.Length <= i)
            return null;
        return hotbar[i].GetItem();
    }

    // Get amount at
    public int GetInventoryAmountAt(int i)
    {
        if (inventory.Length <= i)
            return 0;
        return inventory[i].GetAmount();
    }

    public int GetHotbarAmountAt(int i)
    {
        if (hotbar.Length <= i)
            return 0;
        return hotbar[i].GetAmount();
    }

    // get number of items
    public int GetNumItems()
    {
        int number = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetItem() != null)
                number++;
        }

        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].GetItem() != null)
                number++;
        }

        return number;
    }

    // Clear inventory
    public void Clear()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].ClearInventorySlot();
        }

        for (int i = 0; i < hotbar.Length; i++)
        {
            hotbar[i].ClearInventorySlot();
        }
    }

    // Print contents
    public void PrintContents()
    {
        Debug.Log("-- Inventory --");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetItem() != null)
                Debug.Log(" - " + inventory[i].GetItem().itemName + ": " + inventory[i].GetAmount());
        }

        Debug.Log("-- Hotbar --");
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].GetItem() != null)
                Debug.Log(" - " + hotbar[i].GetItem().itemName + ": " + hotbar[i].GetAmount());
        }
    }

    // Test
    public void Test()
    {
        Debug.Log(inventory.Length);
    }
}

//inventory slot class
[System.Serializable]
public class InventorySlot
{
    private ItemObject item;
    private int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void SetAmount(int value)
    {
        amount = value;
    }

    public ItemObject GetItem()
    {
        return item;
    }

    public void SetItem(ItemObject itemObject)
    {
        item = itemObject;
    }

    public void ClearInventorySlot()
    {
        item = null;
        amount = 0;
    }

    public override string ToString()
    {
        return item.itemName + " : " + amount;
    }
}