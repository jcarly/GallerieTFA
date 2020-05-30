using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 _offset;

    private Transform _transformComponent;

    private void Awake()
    {
        _transformComponent = GetComponent<Transform>();
    }

    void LateUpdate ()
    {
        _transformComponent.position = target.position + _offset;
        _transformComponent.rotation = target.rotation;
    }
}
