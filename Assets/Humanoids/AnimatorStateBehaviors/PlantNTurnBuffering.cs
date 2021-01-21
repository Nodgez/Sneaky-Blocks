using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantNTurnBuffering : StateMachineBehaviour
{
    public int fixedStepBufferCount = 2;
    private float inputBufferTimer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        inputBufferTimer = 0;
        var stateName = stateInfo.IsName("PlantNTurnLeft") ? "PlantNTurnLeft" : "PlantNTurnRight";
        Debug.Log(stateName + " Fixed Time to pass: " + (Time.fixedDeltaTime * fixedStepBufferCount).ToString());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        inputBufferTimer += Time.deltaTime;
        var quitBuffering = inputBufferTimer > Time.fixedDeltaTime * fixedStepBufferCount;
        var dampeningValue = quitBuffering ? 10000 : 0;

        if(!quitBuffering)
            Debug.Log("Time Buffer: " + inputBufferTimer.ToString());
        animator.SetFloat(Locomotion.m_DirectionId, animator.GetFloat(Locomotion.m_DirectionId), dampeningValue, Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
