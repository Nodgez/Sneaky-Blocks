using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity;

    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
    }

    private void Update()
    {
        var worldDeltaPosition = agent.nextPosition - transform.position;

        var dx = Vector3.Dot(transform.right, worldDeltaPosition);
        var dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        var deltaPosition = new Vector3(dx, dy);

        var smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        var shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        animator.SetBool("Move", shouldMove);
        animator.SetFloat("VelX", velocity.x);
        animator.SetFloat("VelY", velocity.y);

        if (worldDeltaPosition.magnitude > agent.radius)
            transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;
    }

    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }
}
