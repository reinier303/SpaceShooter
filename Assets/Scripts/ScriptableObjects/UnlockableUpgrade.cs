using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/UnlockUpgrade", order = 999)]
public class UnlockableUpgrade : ScriptableUpgrade
{
    public string Unlock;

    public override void Upgrade(PlayerStats playerStats)
    {
        if(Cost <= playerStats.TotalUnits)
        {
            playerStats.TotalUnits -= Cost;
            playerStats.Unlockables[Unlock] = true;
        }
    }
}