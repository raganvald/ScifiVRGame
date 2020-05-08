using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    [SerializeField]
    Transform target;
    NavMeshAgent agent;

    public bool active = false;                 //Is this timer active?
    public float cooldown = 1f;              //How often this cooldown may be used
    public float timer = 0;                 //Time left on timer, can be used at 0
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameMgr.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (active)
            timer -= Time.deltaTime;    //Subtract the time since last frame from the timer.
        if (timer < 0)
            timer = 0;                  //If timer is less than 0, reset it to 0 as we don't want it to be negative


        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                //attack the target
                AttackTarget();
                FaceTarget();
            } else
            {
                agent.isStopped = false;
            }
                    
        }
    }

    void AttackTarget()
    {
        if (timer > 0 && GameMgr.Instance.gameOver == false)
            return;
    
        active = false;
        //Run Action Logic
        animator.Play("attack");

        target.transform.root.GetComponent<PlayerHealth>().TakeDamage(3);

        timer = cooldown;
        active = true;
    }

    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
