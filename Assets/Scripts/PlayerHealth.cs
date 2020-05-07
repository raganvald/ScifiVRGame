using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float regenTime;
    public int playerMaxHealth;
    public int playerHealth;
    public int playerHealthRegenRate;


    public void Awake()
    {
        StartCoroutine(RegenHealth());  
    }


    IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenTime);
            if (playerHealth < playerMaxHealth)
            {
                playerHealth += playerHealthRegenRate;
            }
        }

    }
}

