﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        if(character != null)
        {
            GameObject chara = Instantiate(character, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            chara.transform.parent = gameObject.transform;
        }
    }
}