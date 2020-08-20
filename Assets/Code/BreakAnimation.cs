using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BreakAnimation : ScriptedAnimationBehaviour
{
    private int _count = 0;
    private int _random;
    private int _state;
    public Vector2 randomRange = new Vector2(10, 20);
    public int layerIndex = 0;
    public string trigger = "animationBreakTrigger";
    // Start is called before the first frame update
    void Start()
    {
        _animatorController = GetComponent<Animator>();
        InitCount();
    }

    // Update is called once per frame
    void Update()
    {
        if(_count > 0 && _animatorController.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash != _state)
        {
            InitCount();
        }

        else if(_count >= _random)
        {
            _animatorController.SetTrigger(trigger);
            InitCount();
        }
    }

    public void IncrementCount()
    {
        if (_count == 0)
        {
            _state = _animatorController.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash;
        }
        _count++;
    }

    private void InitCount()
    {
        _count = 0;
        _random = Mathf.RoundToInt(Random.Range(randomRange.x, randomRange.y));
    }


}
