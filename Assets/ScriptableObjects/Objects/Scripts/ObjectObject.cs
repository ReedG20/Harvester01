using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Object")]
public class ObjectObject : ScriptableObject
{
    //Drops
    public ItemObject[] dropItem;
    public int[] dropAmount;

    //Health
    public float objectHealth;
}