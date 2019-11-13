using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    PlayerStats playerStats;
    Button button;
    public Text SoldText;
    public Text CostText;
    public Text NameText;
    public GameObject Confirmation;
    public bool bought;
    public bool confirming;
    string UpgradeName;
    float Cost;
    public ScriptableUpgrade scriptableUpgrade;

    private void Awake()
    {
        Cost = scriptableUpgrade.Cost;
        UpgradeName = scriptableUpgrade.UpgradeName;
        playerStats = PlayerStats.Instance;
        button = transform.GetChild(0).GetComponent<Button>();
        if(!playerStats.ShopItems.ContainsKey(UpgradeName))
        {
            playerStats.ShopItems.Add(UpgradeName, false);
        }
        if(playerStats.ShopItems[UpgradeName])
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        CostText.text = "Cost: " + Cost;
        CostText.gameObject.SetActive(false);
        NameText.text = UpgradeName;
    }

    public void Upgrade()
    {
        scriptableUpgrade.Upgrade(playerStats);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(bought || confirming)
        {
            CostText.gameObject.SetActive(false);
        }
        else
        {
            CostText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CostText.gameObject.SetActive(false);
    }

    public void DisableConfirmation()
    {
        Confirmation.SetActive(false);
        CostText.gameObject.SetActive(true);
        confirming = false;
    }

    public void DisableButton()
    {
        button.enabled = false;
        SoldText.gameObject.SetActive(true);
    }

    public void Confirm()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            Confirmation.SetActive(true);
            CostText.gameObject.SetActive(false);
            confirming = true;
        }
    }
    /*
    #region Upgrades

    public void UnlockBoost()
    {
        if(Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;                      
            playerStats.BoostUnlocked = true;
            DisableButton();
            bought = true;
        }
    }

    public void DamageLvl1()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.Damage += 1;
            DisableButton();
            bought = true;
        }
    }

    public void FireRateLvl1()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.FireRate += 1;
            DisableButton();
            bought = true;
        }
    }

    public void LivesLvl1()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.MaxHealth += 1;
            DisableButton();
            bought = true;
        }
    }

    public void MutltipleProjectilesLvl1()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.MultipleProjectiles = true;
            playerStats.ProjectileCount++;
            DisableButton();
            bought = true;
        }
    }

    public void SpeedLvl1()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.Speed += 1;
            DisableButton();
            bought = true;
        }
    }

    public void DamageLvl2()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.Damage += 1.5f;
            DisableButton();
            bought = true;
        }
    }

    public void FireRateLvl2()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.FireRate += 1.5f;
            DisableButton();
            bought = true;
        }
    }

    public void LivesLvl2()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.MaxHealth += 1;
            DisableButton();
            bought = true;
        }
    }

    public void MutltipleProjectilesLvl2()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.MultipleProjectiles = true;
            playerStats.ProjectileCount++;
            DisableButton();
            bought = true;
        }
    }

    public void SpeedLvl2()
    {
        if (Cost <= playerStats.TotalUnits)
        {
            playerStats.ShopItems[UpgradeName] = true;
            playerStats.TotalUnits -= Cost;
            playerStats.Speed += 1.5f;
            DisableButton();
            bought = true;
        }
    }

    #endregion
    */
}
