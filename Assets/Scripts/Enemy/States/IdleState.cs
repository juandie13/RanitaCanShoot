using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(EnemyController controller) : base(controller)
    {
        // Creamos nuestra transicion de Idle -> Follow
        Transition transitionIdleToFollow = new Transition(
            isValid: () => {
                float distance = Vector3.Distance(
                    controller.target.transform.position,
                    controller.transform.position
                );
                if (distance < controller.DistanceToFollow)
                {
                    return true;
                }else
                {
                    return false;    
                }
                
            },
            getNextState: () => {
                return new FollowState(controller);
            }
        );

        Transitions.Add(transitionIdleToFollow);
    }

    public override void OnStart()
    {
        //Debug.Log("Estado Idle Start");
        //controller.rb.velocity = Vector3.zero;
        controller.agent.isStopped = true;
    }

    public override void OnUpdate()
    {
    }

    public override void OnFinish()
    {
        //Debug.Log("Estado Idle Finish");
    }
}