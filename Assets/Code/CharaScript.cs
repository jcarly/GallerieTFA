using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaScript : ScriptedAnimationBehaviour
{
    //private Animator _animatorController;
    public int cellNumber = 0;
    private float _currentSpeed;
    private bool _shoot = false;
    private AudioSource _audioSource;
    private bool _disappear = false;
    private bool _reappear = false;
    private bool _zoneEffect = false;
    private bool _zoneEffectFade = false;
    public float disappearSpeed = 0.1f;
    public float reappearSpeed = 0.5f;

    [Header("Weapon")]
    public Transform weapon;
    public Transform drawnWeaponBone;
    public Transform undrawnWeaponBone;

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
    //public CameraAnchorPoint cameraAnchorPoint;
    public CameraAnchor cameraAnchor;

    [Header("Test controls")]
    public KeyCode JumpTrigger = KeyCode.J;
    public KeyCode VictoryTrigger = KeyCode.V;
    public KeyCode HitTrigger = KeyCode.H;
    public KeyCode AttackTrigger = KeyCode.A;
    public KeyCode DeathTrigger = KeyCode.X;
    public KeyCode DrawTrigger = KeyCode.W;
    public KeyCode TauntTrigger = KeyCode.T;
    public KeyCode EdgyTrigger = KeyCode.E;
    public KeyCode PushTrigger = KeyCode.P;
    public KeyCode FreeAnimTrigger = KeyCode.F;
    public KeyCode GymTrigger = KeyCode.F;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animatorController.SetInteger("CellNumber", cellNumber);

        float stateLength = _animatorController.GetCurrentAnimatorStateInfo(0).length;
        _animatorController.SetFloat("CycleOffset", Random.Range(0, stateLength));

        _animatorController.applyRootMotion = false;

        switch (cellNumber)
        {
            case 5:
                _animatorController.SetTrigger("FreeAnimTrigger");
                break;
            case 6:
            case 0:
                if (weapon != null)
                {
                    weapon.SetParent(undrawnWeaponBone);
                    weapon.gameObject.SetActive(true);
                }
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
                if (transform.parent.GetSiblingIndex() == 3)
                {
                    _animatorController.SetFloat("Caracterisation", 1f);
                }
                break;
            case 10:
                _animatorController.SetFloat("LocomotionBlendingValue", 2f);
                _animatorController.SetFloat("Caracterisation", 1f);
                break;
            case 12:
                _animatorController.SetTrigger("HeavyWeaponRunTrigger");
                if (weapon != null)
                {
                    weapon.SetParent(drawnWeaponBone);
                    weapon.gameObject.SetActive(true);
                }
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
        enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchAKey) || Input.GetButtonUp("Cross"))
        {
            switch (cellNumber)
            {
                case 2:
                    _animatorController.SetTrigger("VictoryTrigger");
                    break;
                case 3:
                    _animatorController.SetTrigger("DeathTrigger");
                    break;
                case 4:
                    _animatorController.SetTrigger("TauntTrigger");
                    break;
                case 6:
                case 15:
                    _animatorController.SetTrigger("AttackTrigger");
                    break;
                case 11:
                    _animatorController.SetTrigger("JumpTrigger");
                    break;
            }
        }
        else if (Input.GetKeyDown(switchBKey) || Input.GetButtonUp("Square"))
        {
            switch (cellNumber)
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
            switch (cellNumber)
            {
                case 6:
                    if (_animatorController.GetCurrentAnimatorStateInfo(0).IsName("WeaponIdle") && ! _shoot)
                    {
                        _animatorController.SetTrigger("AttackTrigger");
                        _shoot = true;
                    }
                    else
                    {
                        _animatorController.SetTrigger("DrawTrigger");
                        _shoot = false;
                    }                    
                    break;
            }
        }




        if (cellNumber == 0)
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

        if (cellNumber == 13) { 
            _animatorController.applyRootMotion = Input.GetKey(accelerationKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical")) > 0;

            if (Input.GetKey(accelerationKey) || JoysticksHelper.FilterAndEase(Input.GetAxis("LeftStickVertical")) > 0)
            {
                transform.Translate(0, 0, acceleration * Time.deltaTime);
                transform.parent.Find("Cube").transform.Translate(-acceleration * Time.deltaTime, 0, 0);
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
            }

            Vector3 localPosition = transform.localPosition;
            if (localPosition.x < limits.y)
            {
                transform.localPosition = new Vector3(limits.x, 0, 0);
                transform.parent.Find("Cube").transform.localPosition = new Vector3(2.2f, 0.5f, 0);
            }
        }

        if (cellNumber == 13 && enableFollowCameraAnchorPoint && cameraAnchor != null && cameraAnchor._currentTargetId == 44)
        {
            /*cameraAnchor.transform.position = cameraAnchorPoint.transform.position;
            cameraAnchor.transform.rotation = cameraAnchorPoint.transform.rotation;*/
            cameraAnchor.transform.position = transform.position;
            cameraAnchor.transform.rotation = transform.rotation;
        }
        if (_disappear || _reappear)
        {
            float apparition = 0;
            foreach (Renderer render in GetComponentsInChildren<Renderer>())
            {
                apparition = _disappear ? render.material.GetFloat("_Apparition") - (disappearSpeed * Time.deltaTime) : render.material.GetFloat("_Apparition") + (reappearSpeed * Time.deltaTime);
                
                render.material.SetFloat("_Apparition", apparition);
               
            }
            if (apparition <= -1)
            {
                _disappear = false;
                _reappear = true;
            }
            if (apparition >= 1)
            {
                _reappear = false;
            }
        }
        if (_zoneEffect || _zoneEffectFade)
        {
            float cutoff = transform.parent.Find("ZoneEffect").GetComponent<Renderer>().material.GetFloat("_Cutoff");
            cutoff = _zoneEffect ? cutoff - (5 * disappearSpeed * Time.deltaTime) : cutoff + (5 * reappearSpeed * Time.deltaTime);

            transform.parent.Find("ZoneEffect").GetComponent<Renderer>().material.SetFloat("_Cutoff", cutoff);

            if (cutoff <= -1.7)
            {
                _zoneEffect = false;
                _zoneEffectFade = true;
            }
            if (cutoff >= 1.7)
            {
                _zoneEffectFade = false;
            }
        }        

        if (cellNumber == 15)
        {
            GetComponent<SphereCollider>().radius = _animatorController.GetFloat("AttackImpact");
            GetComponent<SphereCollider>().center = new Vector3(0, 1, _animatorController.GetFloat("AttackImpact"));
        }
    }


    public void DrawWeapon()
    {
        if(weapon.parent == undrawnWeaponBone)
        {
            weapon.SetParent(drawnWeaponBone);
            weapon.localPosition = new Vector3(-0.178f, -0.007f, -0.056f);
            weapon.localRotation = Quaternion.Euler(2.621f, 18.328f, -10.259f);
        }
        else
        {
            weapon.SetParent(undrawnWeaponBone);
            weapon.localPosition = new Vector3(-0.101f, -0.237f, -0.13f);
            weapon.localRotation = Quaternion.Euler(77.75f, 90f, -50.557f);
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void StartParticleSystem()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void StopParticleSystem(ParticleSystem particle)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
    }

    public void Disappear()
    {
        _disappear = true;
    }

    public void ZoneEffect()
    {
        _zoneEffect = true;
    }
}
