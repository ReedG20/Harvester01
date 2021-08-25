using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Object")]
public class ObjectObject : ScriptableObject
{
    public string objectName;
    // Drops
    [Space]
    public ItemObject[] dropItem;
    public int[] dropAmount;
    [Space]
    // Health
    public float objectEfficiencyMultiplier;

    public int CompareTo(object a)
    {
        ItemObject other = a as ItemObject;
        return other.itemName.CompareTo(this.objectName);
    }
}