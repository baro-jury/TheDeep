using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public bool isInitialized = false;

    public float velocity;
    public int health;
    public int shield;
    public int mana;
    public int gold;
    //public List<Item> inventory;
}
