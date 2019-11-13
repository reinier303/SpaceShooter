using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/StatUpgrade", order = 999)]
public class StatUpgrade : ScriptableUpgrade
{
    public string Stat;
    public float UpgradeAmount;

    public override void Upgrade(PlayerStats playerStats)
    {
        if(Cost <= playerStats.TotalUnits)
        {
            playerStats.TotalUnits -= Cost;
            playerStats.Stats[Stat] += UpgradeAmount;
        }
    }
}