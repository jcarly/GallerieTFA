using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaScript : ScriptedAnimationBehaviour
{
    //private Animator _animatorController;
    private int _cellNumber = 0;
    private float _currentSpeed;

    [Header("Custom controls")]
    public KeyCode accelerationKey = KeyCode.UpArrow;
    public KeyCode decelerationKey = KeyCode.DownArrow;
    public KeyCode rotateLeftKey = KeyCode.LeftArrow;
    public KeyCode rotateRightKey = KeyCode.RightArrow;

    [Space(1)]
    public KeyCode switchAKey = KeyCode.Space;
    public KeyCode switchBKey = KeyCode.Return;
    public KeyCode switchCKey = KeyCode.RightControl;

    [Header("Locomotion")]
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;
    public float walkingSpeed = 1.0f;
    public float runningSpeed = 3.0f;
    public bool enableRotation = true;
    public float rotationSpeed = 90.0f;
    public Vector2 limits = new Vector2(3.5f, -3.5f);

    [Header("Camera Anchor Point")]
    public bool enableFollowCameraAnchorPoint = true;    
    public CameraAnchorPoint cameraAnchorPoint;
    public CameraAnchor cameraAnchor;

    void Start()
    {
        //_animatorController = GetComponent<Animator>();

        if(transform.parent != null && transform.parent.transform.parent != null && transform.parent.transform.parent.transform.parent != null)
        {
            int.TryParse(transform.parent.transform.parent.transform.parent.name, out _cellNumber);
            _animatorController.SetInteger("CellNumber", _cellNumber);
        }       

        float stateLength = _animatorController.GetCurrentAnimatorStateInfo(0).length;
        _animatorController.SetFloat("CycleOffset", Random.Range(0, stateLength));

        _animatorController.applyRootMotion = false;

        //enabled = false;

        switch (_cellNumber)
        {
            case 2:
                _animatorController.SetTrigger("VictoryTrigger");
                break;
            case 5:
                _animatorController.SetTrigger("FreeAnimTrigger");
                break;
            case 7:
                _animatorController.SetFloat("LocomotionBlendingValue", 1f);
                _animatorController.SetFloat("Caracterisation", 0f);
                break;
            case 8:
                _animatorController.SetFloat("LocomotionBlendingValue", 1f);
                _animatorController.SetFloat("Caracterisation", 1f);
                break;
            case 9:
                _animatorController.SetFloat("LocomotionBlendingValue", 2f);
                _animatorController.SetFloat("Caracterisation", 0f);
                break;
            case 10:
                _animatorController.SetFloat("LocomotionBlendingValue", 2f);
                _animatorController.SetFloat("Caracterisation", 1f);
                break;
            case 12:
                _animatorController.SetTrigger("HeavyWeaponRunTrigger");
                break;
            case 13:
                transform.Rotate(0, -90, 0);
                transform.Translate(0, 0, -4);
                _animatorController.SetBool("Push", true);
                break;
            case 14:
                _animatorController.SetTrigger("GymTrigger");
                break;                
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(switchAKey) || Input.GetButtonUp("Cross"))
        {
            switch (_cellNumber)
            {
                case 3:
                    _animatorController.SetTrigger("DeathTrigger");
                    break;
                case 4:
                    _animatorController.SetTrigger("TauntTrigger");
                    break;
                case 6:
                    _animatorController.SetTrigger("AttackTrigger");
                    break;
            }
        }
        else if (Input.GetKeyDown(switchBKey) || Input.GetButtonUp("Square"))
        {
            switch (_cellNumber)
            {
                case 4:
                    _animatorController.SetTrigger("EdgyTrigger");
                    break;
                case 6:
                    _animatorController.SetTrigger("HitTrigger");
                    break;
            }
        }
        else if (Input.GetKeyDown(switchCKey) || Input.GetButtonUp("Circle"))
        {
            _animatorController.SetInteger("AnimationID", 2);
            _animatorController.SetTrigger("TriggerNextAnimation");
        }




        if (_cellNumber == 0)
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
                                                runningSpeed, _currentSpeed);

            // Cumulates the ratios to create the blending value [0->n*ratios]
            float cumulatedRatios = idleToWalkRatio + walkToRunRatio;

            Vector2 leftStickValues = new Vector2(JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickHorizontal")), Mathf.Max(0, JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical"), new Vector2(0.1f, Mathf.Sin(Mathf.PI / 4)), 2.0f)));
            cumulatedRatios += leftStickValues.magnitude * 2.0f;

            // Sets the cumulated ratios to the LocomotionBlendingValue parameter
            // in the animator component
            _animatorController.SetFloat("LocomotionBlendingValue", cumulatedRatios);

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

        if (_cellNumber == 11)
        {
            _animatorController.SetTrigger("JumpTrigger");
        }

        if (_cellNumber == 13) { 
            _animatorController.applyRootMotion = Input.GetKey(accelerationKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical")) > 0;

            Vector3 localPosition = transform.localPosition;
            if (localPosition.x < limits.y)
            {
                transform.localPosition = new Vector3(limits.x, 0, 0);
            }
        }

        if (_cellNumber == 13 && enableFollowCameraAnchorPoint && cameraAnchor != null && cameraAnchor._currentTargetId == 45)
        {
            /*cameraAnchor.transform.position = cameraAnchorPoint.transform.position;
            cameraAnchor.transform.rotation = cameraAnchorPoint.transform.rotation;*/
            cameraAnchor.transform.position = transform.position;
            cameraAnchor.transform.rotation = transform.rotation;
        }
    }
}
