using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : StateMachineBehaviour
{
    public string triggerName = "TriggerNextAnimation";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ScriptedAnimationBehaviour scriptedAnimationBehaviour = animator.gameObject.GetComponent<ScriptedAnimationBehaviour>();
        if(scriptedAnimationBehaviour != null)
        {
            scriptedAnimationBehaviour.ResetTrigger(triggerName);
        }
    }
}
