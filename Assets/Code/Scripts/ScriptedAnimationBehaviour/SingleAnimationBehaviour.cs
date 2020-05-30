using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SingleAnimationBehaviour : MonoBehaviour
{
    private Animator _animatorController;

    void Start()
    {
        _animatorController = GetComponent<Animator>();

        float stateLength = _animatorController.GetCurrentAnimatorStateInfo(0).length;
        _animatorController.SetFloat("CycleOffset", Random.Range(0, stateLength));

        enabled = false;
    }
}
