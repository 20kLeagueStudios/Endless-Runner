using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSTM : StateMachineBehaviour
{

    /// <summary>
    /// Questo stm si occupa di rendere a true lo sliding del PlayerMovement quando l'animazione starà per finire
    /// in questo modo verrà gestito tutto correttamente all'interno dello script PlayerMovement
    /// </summary>


    //Riferimento allo script playerMovement da cui prenderemo la booleana sliding per settarla a false
    PlayerMovement playerMovement;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Prendo il riferimento playerMovement dal parent
        playerMovement = animator.GetComponentInParent<PlayerMovement>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Se l'animazione è finita
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9 && !animator.IsInTransition(0))
        {
            //Chiamo il metodo ResetSliding del playerMovement
            playerMovement.ResetSliding();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //playerMovement.sliding = false;
        //playerMovement.ResetSliding();
        //playerMovement.ResetSliding();
    }

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
