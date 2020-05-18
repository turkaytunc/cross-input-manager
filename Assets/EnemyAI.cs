using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform followTransform;
    private NavMeshAgent navMeshAgent;

    private float chaseDistance;

    [Header("Enemy Movement Controls")]
    [SerializeField] private float maxChaseRadius = 20f;
    [SerializeField] private float minChaseRadius = 2f;
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float turnSpeed = 15f;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        followTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        chaseDistance = Vector3.Distance(followTransform.position, transform.position);


        if ( chaseDistance > minChaseRadius && chaseDistance < maxChaseRadius )
        {
            CalculateLookRotation();

            Vector3 offset = (followTransform.position - transform.position).normalized * Time.deltaTime * movementSpeed;
            navMeshAgent.Move(offset);
        }
    }

    private void CalculateLookRotation()
    {
        Vector3 distanceBetweenPlayerAndThisGameObject = (followTransform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(distanceBetweenPlayerAndThisGameObject, Vector3.up);
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmos()
    {
        Handles.color = new Vector4(230, 0, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position + Vector3.down, Vector3.up, maxChaseRadius);

        Handles.color = new Vector4(0, 240, 0, 0.1f);
        Handles.DrawSolidDisc(transform.position + Vector3.down * 0.5f, Vector3.up, minChaseRadius);
    }
}
