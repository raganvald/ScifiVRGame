using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
        SetKinematic(true);
        hp = maxHp;
    }
    public void Damage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        if (hp <= 0) Die();
    }
    void Die()
    {
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 5);
    }
}
