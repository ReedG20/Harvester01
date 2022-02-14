using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Object")]
public class ObjectObject : ScriptableObject
{
    // All variants of the object
    public Variant[] variants;
    // So that we know which variant it is after it's been intantiated
    public int variant;
    // Health
    public float objectEfficiencyMultiplier;

    public int CompareTo(object a)
    {
        ItemObject other = a as ItemObject;
        return other.itemName.CompareTo(this.variants[0].name);
    }
}

// Variant class
[System.Serializable]
public class Variant
{
    public string name;
    // Prefabs
    public GameObject[] states;
    public ItemObject[] dropItem;
    public int[] dropAmount;
}