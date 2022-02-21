using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class ObjectObject : ScriptableObject
{
    public string name; // e.g., "Snowy Tree"
    // Prefabs
    public GameObject[] states;

    public ObjectTypeObject objectType; // e.g., ObjectType("Tree")

    public int CompareTo(object a)
    {
        ItemObject other = a as ItemObject;
        return other.name.CompareTo(this.name);
    }
}

//[CreateAssetMenu(fileName = "New Object", menuName = "Object System/Object")]