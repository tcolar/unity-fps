using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStats, staminaStats;

    public void DispalyHealthStats(float healthVal)
    {
        healthVal /= 100f;
        healthStats.fillAmount = healthVal;
    }

    public void DisplayStaminaStats(float stamina)
    {
        stamina /= 100f;
        staminaStats.fillAmount = stamina;
    }
}
