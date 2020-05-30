using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ABAAnimationBehaviour : ScriptedAnimationBehaviour
{
    public KeyCode switchKey = KeyCode.Space;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyUp(switchKey) || Input.GetButtonUp("Cross"))
        {
            _animatorController.SetTrigger("TriggerNextAnimation");
        }
    }
}
