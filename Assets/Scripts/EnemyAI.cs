using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
            Agent.SetDestination(target.position);

    }


    void FindPlayer()
    {
        print("÷≈À‹ Õ¿…ƒ≈Õ¿");

        GameObject player = PlayerController.instance.GetComponent<GameObject>();

        if(player != null)
        {
            target = player.transform;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - Agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
