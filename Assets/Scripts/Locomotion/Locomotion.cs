using UnityEngine;
using System.Collections;

public class Locomotion
{
    private Animator m_Animator = null;

    public static int m_SpeedId = 0;
    public static int m_AgularSpeedId = 0;
    public static int m_DirectionId = 0;

    public static float m_SpeedDampTime = 0.4f;
    public static float m_AnguarSpeedDampTime = 0.35f;
    public static float m_DirectionResponseTime = 0.5f;


    public Locomotion(Animator animator)
    {
        m_Animator = animator;

        m_SpeedId = Animator.StringToHash("Speed");
        m_AgularSpeedId = Animator.StringToHash("AngularSpeed");
        m_DirectionId = Animator.StringToHash("Direction");
    }

    public void Reset()
    {
        m_Animator.SetFloat(m_SpeedId, 0);
        m_Animator.SetFloat(m_AgularSpeedId, 0);
        m_Animator.SetFloat(m_DirectionId, 0);
    }

    public void Do(float speed, float direction)
    {
        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(0);

        bool inTransition = m_Animator.IsInTransition(0);
        bool inIdle = state.IsName("Idle");
        bool inTurn = state.IsName("TurnOnSpot") || state.IsName("PlantNTurnLeft") || state.IsName("PlantNTurnRight");
        bool inWalkRun = state.IsName("Movement");

        float speedDampTime = inIdle ? 0 : m_SpeedDampTime;
        float angularSpeedDampTime = inWalkRun || inTransition ? m_AnguarSpeedDampTime : 0;
        float directionDampTime = inTurn || inTransition && (direction > 0) ? 10000 : 0;

        float angularSpeed = direction / m_DirectionResponseTime;

        m_Animator.SetFloat(m_SpeedId, speed, speedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_AgularSpeedId, angularSpeed, angularSpeedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_DirectionId, direction, directionDampTime, Time.deltaTime);
    }

}
