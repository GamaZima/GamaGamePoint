using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] wayPoints;
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    private int wayPointIndex;

    // Start is called before the first frame update
    void Start()
    {        
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        wayPointIndex = 0;
        transform.LookAt(wayPoints[wayPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {        
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            FollowPlayer();
        }

        else if (gameObject.tag == "Troll")
        {
            Patrol();
        }

        else return;
    }

    void Patrol()
    {
        float distToPoint = Vector3.Distance(transform.position, wayPoints[wayPointIndex].position);
        agent.SetDestination(wayPoints[wayPointIndex].position);

        if (distToPoint < 5f)
        {
            IncreaseIndex();
        }
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;
        if(wayPointIndex >= wayPoints.Length)
        {
            wayPointIndex = 0;
        }
        transform.LookAt(wayPoints[wayPointIndex].position);
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    // Enemy is attacking
                    combat.Attack(targetStats);
                }

                FaceTarget();
            }
    }

    void FaceTarget()
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
