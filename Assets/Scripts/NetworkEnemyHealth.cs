﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NetworkEnemyHealth : MonoBehaviour
{
    EnemyController enemyController;
    CapsuleCollider capsule;
    Rigidbody rigidbody;
    Animator animator;
    NavMeshAgent agent;
     

    public int maxHp = 10;
    private int hp;
    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }
    void Start()
    {
        enemyController = GetComponent<EnemyController>() ;
        capsule = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //SetKinematic(true);
        hp = maxHp;
    }
    public void Damage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0) Die();
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        PhotonNetwork.Destroy(gameObject);
    }

    void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;
        enemyController.enabled = false;
        //capsule.enabled = false;
        //rigidbody.detectCollisions = false;
        GetComponent<Animator>().enabled = false;
        //SetKinematic(false);


        //Destroy(gameObject, 5);
        StartCoroutine(RemoveEnemy());
    }
}
