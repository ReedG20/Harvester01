using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Object")]
public class ObjectObject : ScriptableObject
{
    public string name; // e.g., "Snowy Tree"
    // Prefabs
    public GameObject[] states;

    public ObjectType type; // e.g., ObjectType("Tree")

    public int CompareTo(object a)
    {
        ItemObject other = a as ItemObject;
        return other.name.CompareTo(this.name);
    }
}