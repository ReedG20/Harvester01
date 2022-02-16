using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal", menuName = "Animal System/Animal")]
public class AnimalObject : ScriptableObject
{
    //Health
    public float animalHealth;

    //Drops
    public ItemObject[] dropItem;
    public int[] dropAmount;
}