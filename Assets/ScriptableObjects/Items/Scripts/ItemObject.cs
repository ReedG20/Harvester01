using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject, IComparable
{
    public string itemName;
    public Sprite itemIcon;
    // Craft
    [Space]
    [Header("Craft")]
    public bool isCraftable;
    public int craftOutput = 1;
    public ItemObject[] ingredients;
    public int[] ingredientCount;
    // Place
    [Space]
    [Header("Place")]
    public bool isPlaceable;
    public ObjectObject objectObject;
    //*
    // Efficiency
    [Space]
    [Header("Make Efficient")]
    public bool isEfficient;
    public ObjectObject[] efficiencyTargetObjects;
    public float toolEfficiencyMultiplier;
    // Damage
    [Space]
    [Header("Damage")]
    public bool doesDamage;
    public float damage;
    // Health
    [Space]
    [Header("Heal")]
    public bool restoresHealth;
    public float health;
    // Harvest
    [Space]
    [Header("Harvest")]
    public bool canHarvest;
    public string harvestTargetObjects;
    public string harvestedItem;
    // Reduce Damage
    [Space]
    [Header("Reduce Damage")]
    public bool reducesDamage;
    public float damagedReduced;
    // Wear
    [Space]
    [Header("Wear")]
    public bool isWearable;
    public bool isHat;
    [Space]
    [Header("Cook")]
    public bool isCookable;
    public float secondsUntilCooked;
    //*/

    public int CompareTo(object a)
    {
        ItemObject other = a as ItemObject;
        return other.itemName.CompareTo(this.itemName);
    }
}