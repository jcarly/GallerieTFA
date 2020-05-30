using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterizedAnimationBehaviour : ScriptedAnimationBehaviour
{
    public KeyCode switchAKey = KeyCode.Space;
    public KeyCode switchBKey = KeyCode.Return;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyDown(switchAKey) || Input.GetButtonUp("Cross"))
        {
            _animatorController.SetTrigger("TriggerLocomotion");
        }
        else if (Input.GetKeyDown(switchBKey) || Input.GetButtonUp("Square"))
        {
            _animatorController.SetTrigger("TriggerJump");
        }
    }
}
