using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    public KeyCode nextAnchorKey = KeyCode.RightArrow;
    public KeyCode previousAnchorKey = KeyCode.LeftArrow;
    public GameObject anchorPointsParent;
    public float interpolationDuration = 0.5f;
    public CameraController cameraController;
    public bool testMode = false;

    private CameraAnchorPoint[] _anchorPoints;
    public int _currentTargetId;
    private ScriptedAnimationBehaviour[] _currentAnimationBehaviours;

    void Awake()
    {
        _anchorPoints = anchorPointsParent.transform.GetComponentsInChildren<CameraAnchorPoint>();

        transform.position = _anchorPoints[0].transform.position;
        transform.rotation = _anchorPoints[0].transform.rotation;

        _currentAnimationBehaviours = GetAnimationBehaviours(_anchorPoints[0]);
    }


    void Update()
    {
        if (Input.GetKeyUp(nextAnchorKey) || Input.GetButtonUp("RightShoulder"))
        {
            _currentTargetId = (_currentTargetId + 1) % _anchorPoints.Length;
            ChangeAnchor();
        }
        else if (Input.GetKeyUp(previousAnchorKey) || Input.GetButtonUp("LeftShoulder"))
        {
            --_currentTargetId;
            _currentTargetId += _currentTargetId < 0 ? _anchorPoints.Length : 0;
            ChangeAnchor();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ChangeAnchor()
    {
        StopAllCoroutines();
        StartCoroutine(LaunchInterpolation());
        cameraController.ResetView();
    }

    IEnumerator LaunchInterpolation()
    {
        EnableAnimationBehaviours(_currentAnimationBehaviours, false);

        int steps = Mathf.RoundToInt(interpolationDuration / Time.unscaledDeltaTime);
        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;
        for (int i = 0; i <= steps; ++i)
        {
            float ratio = (float)i / (float)steps;
            transform.position = Vector3.Lerp(startingPosition, _anchorPoints[_currentTargetId].transform.position, ratio);
            transform.rotation = Quaternion.Slerp(startingRotation, _anchorPoints[_currentTargetId].transform.rotation, ratio);

            yield return null;
        }

        _currentAnimationBehaviours = GetAnimationBehaviours(_anchorPoints[_currentTargetId]);
        EnableAnimationBehaviours(_currentAnimationBehaviours, true);
    }

    private ScriptedAnimationBehaviour[] GetAnimationBehaviours(CameraAnchorPoint anchorPoint)
    {
        Transform parent = anchorPoint.transform.parent;
        if(parent.name == "Cell")
        {
            return anchorPoint.transform.parent.GetComponentsInChildren<ScriptedAnimationBehaviour>();
        }

        return null;
    }

    private void EnableAnimationBehaviour(ScriptedAnimationBehaviour animationBehaviour, bool value)
    {
        if (animationBehaviour != null)
        {
            animationBehaviour.enabled = value;
        }
    }

    private void EnableAnimationBehaviours(ScriptedAnimationBehaviour[] animationBehaviour, bool value)
    {
        if(animationBehaviour != null)
        {
            for (int i = 0; i < animationBehaviour.Length; ++i)
            {
                EnableAnimationBehaviour(animationBehaviour[i], value);
            }
        }
    }
}
