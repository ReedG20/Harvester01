using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectType", menuName = "Object System/ObjectType")]
public class ObjectTypeObject : ScriptableObject
{
    public string typeName; // e.g., "Tree"
    public ItemObject[] dropItem;
    public int[] dropAmount;
    public bool collider;
    // Health
    public float objectEfficiencyMultiplier;
}