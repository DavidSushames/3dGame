using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (DistancetoPlayer() < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool ForgetPlayer()
    {
        if (DistancetoPlayer() > 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Sword")
        {
            

        }
            hit = 1;

            impact.Play();
            Debug.Log("Chaser hit");

        
    }

    // Update is called once per frame
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
                // float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                float newY = 0f;
                Vector3 dest = new Vector3(newX, newY, newZ);
                agent.SetDestination(dest);
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

            agent.SetDestination(target.transform.position);
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
            this.transform.LookAt(target.transform.position);
            Debug.Log("Attack");
            if (DistancetoPlayer() > agent.stoppingDistance + 2)
            {
                state = STATE.CHASE;

            }

            break;

        case STATE.DEAD:

            TurnoffTriggers();
            anim.SetBool("Die", true);



            // hit = 1;
            // Destroy(this.gameObject, 5f);
            Debug.Log("Case DEAD");

            Destroy(this.gameObject, 5f);
            // this.GetComponent<Sink>().StartSink();
            break;


    }

}
}
