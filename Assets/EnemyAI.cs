using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform followTransform;

    private NavMeshAgent navMeshAgent;
    private float followDistance;
    private float maxChaseRadius = 20f;
    private float minChaseRadius = 2f;
    private float movementSpeed = 7f;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        followTransform = FindObjectOfType<PlayerMovement>().transform;
        
    }

    void Update()
    {
        followDistance = Vector3.Distance(followTransform.position, transform.position);


        if ( followDistance > minChaseRadius && followDistance < maxChaseRadius )
        {
            Vector3 offset = (followTransform.position - transform.position).normalized * Time.deltaTime * movementSpeed;

            navMeshAgent.Move(offset);
        }
    }

    private void OnDrawGizmos()
    {


        Handles.color = new Vector4(255, 0, 0, 0.2f);
        Handles.DrawSolidDisc(transform.position + Vector3.down, Vector3.up, maxChaseRadius);

        Handles.color = new Vector4(0, 255, 0, 0.2f);
        Handles.DrawSolidDisc(transform.position + Vector3.down * 0.5f, Vector3.up, minChaseRadius);

    }
}
