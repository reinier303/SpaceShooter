using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public static PlayerData Instance;

    public Dictionary<string, float> Stats;
    public Dictionary<string, bool> Unlockables;
    public Dictionary<string, bool> ShopItems;

    public float Units;
    public float TotalUnits;


    void Awake()
    {
        Instance = this;
    }

    public PlayerData(PlayerStats player)
    {
        Debug.Log(player.Stats.Count);
        //Dictionaries
        Stats = player.Stats;
        Unlockables = player.Unlockables;
        ShopItems = player.ShopItems;

        Debug.Log(Stats);

        //Units
        Units = player.Units;
        TotalUnits = player.TotalUnits;
    }
}
