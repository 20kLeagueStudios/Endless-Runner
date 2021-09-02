using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranchioStepDeathSTM : StateMachineBehaviour
{

    EnemyMove granchioStep;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        granchioStep = animator.GetComponent<EnemyMove>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            granchioStep.Disable();
        }
    }
}
