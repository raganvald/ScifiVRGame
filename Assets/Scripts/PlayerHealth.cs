using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sigtrap.VrTunnellingPro;

public class PlayerHealth : MonoBehaviour
{
    public float regenTime;
    public float playerMaxHealth;
    public float playerHealth;
    public float playerHealthRegenRate;
    public AudioSource audioSource;
    public AudioClip audioGrunt;
    public AudioClip audioDie;
    public AudioClip audioBreath;

    public GameObject tunnelSource;

    TunnellingBase tb;



    public void Awake()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
        StartCoroutine(RegenHealth());
        tb = tunnelSource.GetComponent<TunnellingBase>();
        tb.effectFeather = .2f;
        tb.effectCoverage = 0;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdateTunnel();
        audioSource.clip = audioGrunt;
        audioSource.Play();
        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        audioSource.clip = audioDie;
        audioSource.Play();
        Debug.Log("Player Died");
    }

    private void UpdateTunnel()
    {
        if (playerMaxHealth == 0)
        {
            Debug.Log("Player Max Health cannot be 0");
            return;
        }

        //Set the coverage of tunnel based on player health
        float coverageOffset = 1 - (playerHealth / playerMaxHealth);
        if (coverageOffset <= 0)
        {
            tb.enabled = false;
        } else
        {
            tb.enabled = true;
        }
        tb.effectCoverage = coverageOffset;
    }

    IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenTime);
            if (playerHealth < playerMaxHealth)
            {
                playerHealth += playerHealthRegenRate;
                UpdateTunnel();
            }
        }

    }
}

