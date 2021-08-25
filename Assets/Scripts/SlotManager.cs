using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotManager : MonoBehaviour
{
    /*
    SlotManager is used to change visual
    elements of each inventory/hotbar
    slot from other classes
    */

    public ItemObject item;
    public int amount;

    public Image icon;  
    public TextMeshProUGUI text;
    public Image image;
    public InventoryUI iUI;

    public int index;

    Color slotColor;

    void Start()
    {
        slotColor = image.color;
        iUI = gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<Transform>().gameObject.GetComponentInParent<InventoryUI>();
    }

    public void SetSlot(ItemObject newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;

        icon.sprite = item.itemIcon;
        icon.enabled = true;

        text.text = amount.ToString("n0");
        text.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        amount = 0;

        icon.sprite = null;
        icon.enabled = false;

        text.text = "0";
        text.enabled = false;

        item = null;
    }

    public void HighlightSlot()
    {
        Color temp = image.color;
        temp.a = 0f;
        image.color = temp;
    }

    public void UnhighlightSlot()
    {
        image.color = slotColor;
    }

    public void SelectSlot()
    {
        iUI.ClickSlot(index);
    }
}
