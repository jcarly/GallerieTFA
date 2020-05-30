﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ABACAAnimationBehaviour : ScriptedAnimationBehaviour
{
    public KeyCode switchAKey = KeyCode.Space;
    public KeyCode switchBKey = KeyCode.Return;

    private int _animationID;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyDown(switchAKey) || Input.GetButtonUp("Cross"))
        {
            _animatorController.SetInteger("AnimationID", 0);
            _animatorController.SetTrigger("TriggerNextAnimation");
        }
        else if (Input.GetKeyDown(switchBKey) || Input.GetButtonUp("Square"))
        {
            _animatorController.SetInteger("AnimationID", 1);
            _animatorController.SetTrigger("TriggerNextAnimation");
        }
    }
}
