using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// World object class
[CreateAssetMenu(fileName = "New World", menuName = "World System/World")]
public class WorldObject : ScriptableObject
{
    // by inputing a Vector2, you have access to that tile/object
    Dictionary<Vector2, ObjectObject> world = new Dictionary<Vector2, ObjectObject>();
    
    public void Add(Vector2 vector2, ObjectObject objectObject)
    {
        world.Add(vector2, objectObject);
    }

    public void Change(Vector2 vector2, ObjectObject objectObject)
    {
        world[vector2] = objectObject;
    }

    public ObjectObject GetObject(int x, int y)
    {
        return world[new Vector2(x, y)];
    }

    public int GetDictionaryLength()
    {
        return world.Count;
    }
}