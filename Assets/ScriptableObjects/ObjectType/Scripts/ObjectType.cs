using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectType", menuName = "Inventory System/ObjectType")]
public class ObjectType : ScriptableObject
{
    public string typeName; // e.g., "Tree"
    public ItemObject[] dropItem;
    public int[] dropAmount;
    // Health
    public float objectEfficiencyMultiplier;
}