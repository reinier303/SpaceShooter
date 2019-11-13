using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Header("Current Stats")]
    public Dictionary<string, float> Stats;
    public Dictionary<string, bool> Unlockables;

    public float Units;
    public float TotalUnits;
    public float MaxHealth;
    public float Health;
    public float Damage;
    public float Speed;
    public float FireRate;
    public float ProjectileCount;

    public bool BoostUnlocked;
    public bool MultipleProjectiles;
    public Dictionary<string, bool> ShopItems;

    public void Awake()
    {
        Stats = new Dictionary<string, float>();
        Unlockables = new Dictionary<string, bool>();
        ShopItems = new Dictionary<string, bool>();

        if (Instance == null)
        {
            Instance = this;
        }
        PlayerData data = SaveSystem.LoadPlayer();
        if(data == null)
        {
            NewSave();
            SaveSystem.SavePlayer(this);
            data = SaveSystem.LoadPlayer();
        }
        else
        {
            InitializeDictionaries(data);
        }
        Initialize(data);
    }


    public void NewSave()
    {
        //Stats
        Stats.Add("Damage", 1);
        Stats.Add("FireRate", 1.5f);
        Stats.Add("MaxHealth", 3);
        Stats.Add("ProjectileCount", 1);
        Stats.Add("Speed", 3);

        //Units
        Units = 0;
        TotalUnits = 0;

        //Unlockables
        Unlockables.Add("BoostUnlocked" , false);
        Unlockables.Add("MultipleProjectiles", false);
    }

    public void InitializeDictionaries(PlayerData data)
    {
        //Stats
        Stats.Add("Damage", data.Stats["Damage"]);
        Stats.Add("FireRate", data.Stats["FireRate"]);
        Stats.Add("MaxHealth", data.Stats["MaxHealth"]);
        Stats.Add("ProjectileCount", data.Stats["ProjectileCount"]);
        Stats.Add("Speed", data.Stats["Speed"]);

        //Unlockables
        Unlockables.Add("BoostUnlocked", data.Unlockables["BoostUnlocked"]);
        Unlockables.Add("MultipleProjectiles", data.Unlockables["MultipleProjectiles"]);
    }

    public void Initialize(PlayerData data)
    {
        //Stats
        MaxHealth = data.Stats["MaxHealth"];
        Health = data.Stats["MaxHealth"];
        Damage = data.Stats["Damage"];
        Speed = data.Stats["Speed"];
        FireRate = data.Stats["FireRate"];
        ProjectileCount = (int)data.Stats["ProjectileCount"];

        //Units
        Units = 0;
        TotalUnits = data.TotalUnits;

        //Unlockables
        BoostUnlocked = data.Unlockables["BoostUnlocked"];
        MultipleProjectiles = data.Unlockables["MultipleProjectiles"];

        //Shop
        if (data.ShopItems != null)
        {
            ShopItems = data.ShopItems;
        }
    }

    public void AddToTotal()
    {
        TotalUnits += Units;
    }

    public void RemoveFromTotal(float Amount)
    {
        TotalUnits -= Amount;
    }

}
