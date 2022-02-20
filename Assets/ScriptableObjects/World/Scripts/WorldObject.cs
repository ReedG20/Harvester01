using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// World object class
[CreateAssetMenu(fileName = "New World", menuName = "World System/World")]
public class WorldObject : ScriptableObject
{
    // by inputing a Vector2, you have access to that tile/object
    Dictionary<Vector2, ObjectObject> worldGround = new Dictionary<Vector2, ObjectObject>();
    Dictionary<Vector2, ObjectObject> worldObject = new Dictionary<Vector2, ObjectObject>();

    // Add

    public void AddGround(Vector2 vector2, ObjectObject objectObject)
    {
        worldGround.Add(vector2, objectObject);
    }

    public void AddObject(Vector2 vector2, ObjectObject objectObject)
    {
        worldObject.Add(vector2, objectObject);
    }

    // Change

    public void ChangeGround(Vector2 vector2, ObjectObject objectObject)
    {
        worldGround[vector2] = objectObject;
    }

    public void ChangeObject(Vector2 vector2, ObjectObject objectObject)
    {
        worldObject[vector2] = objectObject;
    }

    // Get

    public ObjectObject GetGround(int x, int y)
    {
        return worldGround[new Vector2(x, y)];
    }

    public ObjectObject GetObject(int x, int y)
    {
        return worldObject[new Vector2(x, y)];
    }

    // Get Dictionary Length

    public int GetGroundDictionaryLength()
    {
        return worldGround.Count;
    }

    public int GetObjectDictionaryLength()
    {
        return worldObject.Count;
    }

    // Value at Key

    public bool ValueAtKeyGround(int x, int y)
    {
        return worldGround.TryGetValue(new Vector2(x, y), out _);
    }

    public bool ValueAtKeyObject(int x, int y)
    {
        return worldObject.TryGetValue(new Vector2(x, y), out _);
    }
}