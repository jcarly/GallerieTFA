using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnimator : MonoBehaviour
{
    public Animator animatorControllerToFollow;

    void LateUpdate()
    {
        if (animatorControllerToFollow == null && transform.parent.GetComponentsInChildren<Animator>().Length != 0)
        {
            animatorControllerToFollow = transform.parent.GetComponentsInChildren<Animator>()[0];
        }
        transform.position = animatorControllerToFollow.rootPosition;
        transform.rotation = animatorControllerToFollow.rootRotation;
    }
}
