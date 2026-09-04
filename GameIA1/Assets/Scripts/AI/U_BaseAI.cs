using UnityEngine;

public abstract class U_BaseAI : MonoBehaviour, IDescription
{
    private UnityEngine.AI.NavMeshAgent agent;

    [Header("AI Configuration")]
    [SerializeField][Min(1)] protected int updatesPerSecond;

    protected float frequency;
    protected float counter;

    public Transform Transform => transform;
    public float Velocity => agent.velocity.magnitude;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        frequency = 1.0f / updatesPerSecond;
        counter = 0.0f;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter > frequency)
        {
            UpdateAI();
            counter -= frequency;
        }
    }

    protected abstract void UpdateAI(); 

    protected void Seek(IDescription target)
    {
        agent.SetDestination(target.Transform.position);
    }

    protected void Flee(IDescription target)
    {
        Vector3 direction = transform.position - target.Transform.position;
        Vector3 destination = transform.position + direction.normalized * 10f;

        agent.SetDestination(destination);
    }



    protected void Evade(IDescription target)
    {
        Vector3 direction = target.Transform.position - transform.position;

        float distance = direction.magnitude;
        float speed = agent.speed + target.Velocity;

        float prediction = distance / speed;

        Vector3 futurePosition =
            target.Transform.position +
            target.Transform.forward * prediction;

        Vector3 fleeDirection =
            transform.position - futurePosition;

        Vector3 destination =
            transform.position + fleeDirection.normalized * 10f;

        agent.SetDestination(destination);
    }

    protected void Wander()
    {
        Vector3 wanderTarget = Vector3.zero;
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 5;

        wanderTarget += new Vector3(Random.insideUnitCircle.x * wanderJitter, 0, Random.insideUnitCircle.y * wanderJitter);

        wanderTarget.Normalize();

        wanderTarget *= wanderRadius;

        Vector3 target = wanderTarget + Vector3.forward * wanderDistance;
        Vector3 targetWorld = transform.TransformPoint(target);

        agent.SetDestination(targetWorld);
    }
}
