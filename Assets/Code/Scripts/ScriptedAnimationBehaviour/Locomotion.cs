using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to control the locomotion
/// (included blending value in the animator controller)
/// </summary>
public class Locomotion : ScriptedAnimationBehaviour
{
    /// <summary>
    /// Increasing speed value
    /// </summary>
    public float acceleration = 1.0f;
    /// <summary>
    /// Key used to accelerate
    /// </summary>
    public KeyCode accelerationKey = KeyCode.UpArrow;
    /// <summary>
    /// Decreased speed value
    /// </summary>
    public float deceleration = 1.0f;
    /// <summary>
    /// The speed when we walk
    /// </summary>
    public float walkingSpeed = 1.0f;
    /// <summary>
    /// The maximum speed reachable (= the running speed)
    /// </summary>
    public float runningSpeed = 3.0f;
    public bool enableRotation = true;
    /// <summary>
    /// The amount of degrees per second to rotate
    /// </summary>
    public float rotationSpeed = 90.0f;
    /// <summary>
    /// Key to rotate to the right
    /// </summary>
    public KeyCode rotateRightKey = KeyCode.RightArrow;
    /// <summary>
    /// Key to rotate to the left
    /// </summary>
    public KeyCode rotateLeftKey = KeyCode.LeftArrow;
    public bool enableFollowCameraAnchorPoint = true;
    public CameraAnchorPoint cameraAnchorPoint;
    public CameraAnchor cameraAnchor;

    /// <summary>
    /// The actual speed of the object
    /// </summary>
    private float _currentSpeed;

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        // Looks if we press the accelerationKey
        if (Input.GetKey(accelerationKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("DpadVertical")) > 0)
        {
            _currentSpeed += acceleration * Time.deltaTime; // Per second
        }
        else // or not
        {
            _currentSpeed -= deceleration * Time.deltaTime; // Per second
        }

        // Clamp the speed from 0 to runningSpeed
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, runningSpeed);

        // Computes where is located _currentSpeed between 0 and walkingSpeed
        float idleToWalkRatio = Mathf.InverseLerp(0, walkingSpeed, _currentSpeed);
        // Computes where is located _currentSpeed between walkingSpeed and runningSpeed
        float walkToRunRatio = Mathf.InverseLerp(walkingSpeed,
                                            runningSpeed,_currentSpeed);

        // Cumulates the ratios to create the blending value [0->n*ratios]
        float cumulatedRatios = idleToWalkRatio + walkToRunRatio;

        Vector2 leftStickValues = new Vector2(JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickHorizontal")), Mathf.Max(0, JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical"), new Vector2(0.1f, Mathf.Sin(Mathf.PI/4)), 2.0f)));
        cumulatedRatios += leftStickValues.magnitude * 2.0f;

        // Sets the cumulated ratios to the LocomotionBlendingValue parameter
        // in the animator component
        _animatorController.SetFloat("LocomotionBlendingValue", cumulatedRatios);

        if(enableRotation)
        {
            float rotationSigningFactor = JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickHorizontal"));
            if (Input.GetKey(rotateRightKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("DpadHorizontal")) > 0)
            {
                rotationSigningFactor += 1; // Sets the rotation forward
            }
            if (Input.GetKey(rotateLeftKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("DpadHorizontal")) < 0)
            {
                rotationSigningFactor -= 1; // Sets the rotation backward
            }
            // Rotates the object
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * rotationSigningFactor);
        }

        if(enableFollowCameraAnchorPoint)
        {
            cameraAnchor.transform.position = cameraAnchorPoint.transform.position;
            cameraAnchor.transform.rotation = cameraAnchorPoint.transform.rotation;
        }
    }

    private void OnDisable()
    {
        _currentSpeed = 0;
        _animatorController.SetFloat("LocomotionBlendingValue", 0);
    }
}
