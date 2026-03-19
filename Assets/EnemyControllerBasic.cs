using System;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EnemyControllerBasic : MonoBehaviour
{
    Animator anim;
    public GameObject target;
    public AudioSource impact;
    NavMeshAgent agent;
    int hit = 0;
    public float walkingspeed;
    public float runningspeed;
    public bool Chaseonly;

    enum STATE { IDLE, WANDER, ATTACK, CHASE, DEAD }
    STATE state = STATE.IDLE;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    void TurnoffTriggers()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Die", false);
    }

    float DistancetoPlayer()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }

    bool CanSeePlayer()
    {
        return DistancetoPlayer() < 10;
    }

    bool ForgetPlayer()
    {
        return DistancetoPlayer() > 20;
    }

    bool SetDestinationSafe(Vector3 destination)
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(destination);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            hit = 1;
            impact.Play();
            Debug.Log("Chaser hit");
        }
    }

    void Update()
    {
        if (this.transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
            return;
        }

        switch (state)
        {
            case STATE.IDLE:
                if (Chaseonly)
                {
                    state = STATE.CHASE;
                    break;
                }
                if (hit > 0)
                {
                    state = STATE.DEAD;
                }
                if (CanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 5000) < 5)
                {
                    state = STATE.WANDER;
                }
                break;

            case STATE.WANDER:
                if (!agent.hasPath)
                {
                    agent.speed = walkingspeed;
                    anim.SetBool("Walk", true);
                    float newX = this.transform.position.x + Random.Range(-15, 15);
                    float newZ = this.transform.position.z + Random.Range(-15, 15);
                    float newY = 0f;
                    Vector3 dest = new Vector3(newX, newY, newZ);
                    if (!SetDestinationSafe(dest))
                        break;
                    agent.stoppingDistance = 2f;
                }
                if (hit > 0)
                {
                    state = STATE.DEAD;
                }
                if (CanSeePlayer())
                {
                    Debug.Log("Can see player");
                    state = STATE.CHASE;
                }
                else if (Random.Range(0, 5000) < 5)
                {
                    state = STATE.IDLE;
                    TurnoffTriggers();
                    agent.ResetPath();
                }
                break;

            case STATE.CHASE:
                if (!SetDestinationSafe(target.transform.position))
                    break;
                agent.stoppingDistance = 2;
                TurnoffTriggers();
                agent.speed = runningspeed;
                anim.SetBool("Run", true);
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }
                if (hit > 0)
                {
                    state = STATE.DEAD;
                }
                if (ForgetPlayer() && !Chaseonly)
                {
                    state = STATE.WANDER;
                    agent.ResetPath();
                }
                break;

            case STATE.ATTACK:
                if (hit > 0)
                {
                    state = STATE.DEAD;
                }
                TurnoffTriggers();
                anim.SetBool("Attack", true);
                this.transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));
                //Debug.Log("Attack");
                if (DistancetoPlayer() > agent.stoppingDistance + 2)
                {
                    state = STATE.CHASE;
                }
                break;

            case STATE.DEAD:
                TurnoffTriggers();
                anim.SetBool("Die", true);
                Debug.Log("Case DEAD");
                Destroy(this.gameObject, 5f);
                break;
        }
    }
}