using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Transform soldier;
    [SerializeField] private SoldierHealth soldierHealth;
    [SerializeField] private float speed;
    NavMeshAgent agent;
    Animator anim;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float turnSpeed;

    bool zombieDeath = false;
    CapsuleCollider col;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (zombieDeath)
        {
            agent.enabled = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, soldier.position);

        if (soldierHealth.death)
        {
            anim.SetBool("Move", false);
            anim.SetBool("Attack", false);
            agent.enabled = false;
        }

        else if (distance > 2f && !zombieDeath) MoveTarget();

        else if(distance < 2f)
        {
            anim.SetBool("Attack", true);
            agent.enabled = false;
        }
    }

    void MoveTarget()
    {
        agent.enabled = true;

        Vector3 target = soldier.position;
        Vector3 dir = soldier.position - gameObject.transform.position;
        dir.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

        agent.SetDestination(target);
        anim.SetBool("Move", true);
        anim.SetBool("Attack", false);
    }

    public void Attack()//AnimationEvent
    {
        soldierHealth.SoldierTakeDamage(5f);
    }

    public void TakeDamge(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            zombieDeath = true;
            currentHealth = 0;
            col.enabled = false;

            anim.SetBool("Attack", false);
            anim.SetBool("Move", false);
            anim.Play("Dead");
            Destroy(gameObject, 3f);
        }
    }
}
