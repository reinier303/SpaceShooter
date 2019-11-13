using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableUpgrade : ScriptableObject
{
    public string UpgradeName;
    public int Cost;
    public Sprite Icon;

    public virtual void Upgrade(PlayerStats playerStats)
    {
        //This method is meant to be overidden.
    }
}