using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchorPoint : ScriptedAnimationBehaviour
{
    public CameraAnchorPoint cameraAnchorPoint;
    public CameraAnchor cameraAnchor;
	
	void LateUpdate ()
    {
        cameraAnchor.transform.position = cameraAnchorPoint.transform.position;
        cameraAnchor.transform.rotation = cameraAnchorPoint.transform.rotation;
    }
}
