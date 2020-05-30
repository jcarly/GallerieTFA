using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWeight : ScriptedAnimationBehaviour
{
    public KeyCode enableRootMotionKey = KeyCode.UpArrow;
    public Vector2 limits = new Vector2(3.5f, -3.5f);
    public bool enableFollowCameraAnchorPoint = true;
    public CameraAnchorPoint cameraAnchorPoint;
    public CameraAnchor cameraAnchor;
    
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        _animatorController.applyRootMotion = Input.GetKey(enableRootMotionKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical")) > 0;

        Vector3 localPosition = transform.localPosition;
        if (localPosition.x < limits.y)
        {
            transform.localPosition = new Vector3(limits.x, 0, 0);
        }

        if (enableFollowCameraAnchorPoint)
        {
            cameraAnchor.transform.position = cameraAnchorPoint.transform.position;
            cameraAnchor.transform.rotation = cameraAnchorPoint.transform.rotation;
        }
    }
}
