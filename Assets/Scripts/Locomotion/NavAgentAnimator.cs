using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class NavAgentAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector2 velocity;
    private float distanceFromWall;
    private Locomotion locomotion;
    private Animator animator;

    [SerializeField]
    private float agentDesiredSpeed = 5.5f;
    [SerializeField]
    private float collisionDistanceCheck = 2f;
    [SerializeField]
    private LayerMask collisionCheck;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
    }

    private void Update()
    {
        if (!AgentDone())
        {
            RaycastHit info;
            var wallNear = Physics.Raycast(transform.position, agent.desiredVelocity.normalized, out info, collisionDistanceCheck, collisionCheck);
            var speedModifier = 1f;

            if (wallNear)
            {
                var oldDistance = distanceFromWall;
                var newDistanceFromWall = Vector3.Distance(info.point, transform.position); ;
                distanceFromWall = oldDistance * 0.9f + newDistanceFromWall * 0.1f;
                speedModifier = distanceFromWall / collisionDistanceCheck;
            }

            agent.speed = agentDesiredSpeed * speedModifier;
            float speed = agent.desiredVelocity.magnitude;
            Vector3 velocity = Quaternion.Inverse(animator.rootRotation) * agent.desiredVelocity;

            float angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
            locomotion.Do(speed, angle);
        }
        else
        {
            locomotion.Do(0, 0);
        }
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;// smoothDelta / Time.deltaTime;
        transform.rotation = animator.rootRotation;
    }

    protected bool AgentDone()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh)
            return true;

        return !agent.pathPending &&
                agent.remainingDistance <= agent.stoppingDistance &&
                agent.isStopped;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (agent)
        {
            Gizmos.color = new Color(1, 1, 0);
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity.normalized * collisionDistanceCheck);
        }
    }
#endif
}
