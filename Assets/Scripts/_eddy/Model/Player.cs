using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int Class { get; set; }
    public int Figure { get; set; }
    public DataPoint DataPoint { get; set; }

    public void Initialize
        (int id, string playerName, int @class, int figure, DataPoint dataPoint)
    {
        Id = id;
        PlayerName = playerName;
        Class = @class;
        Figure = figure;
        DataPoint = dataPoint;
    }

    public float velocity = 5f;
    public int health = 5;
    public int shield = 7;
    public int mana = 202;
}

public class DataPoint
{
    public int CoreHP { get; set; }
    public int CoreMP { get; set; }
    public int CoreATK { get; set; }
    public int CoreDEF { get; set; }
    public int CoreCRIT { get; set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }

    public DataPoint(
        int coreHP, int coreMP, int coreATK, int coreDEF, int coreCRIT,
        int currentHP, int currentMP)
    {
        CoreHP = coreHP;
        CoreMP = coreMP;
        CoreATK = coreATK;
        CoreDEF = coreDEF;
        CoreCRIT = coreCRIT;
        CurrentHP = currentHP;
        CurrentMP = currentMP;
    }
}