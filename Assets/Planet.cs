using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Camera upCamera;

    private bool _isCrash;
    private void OnCollisionEnter(Collision other)
    {
        if (other != null && _isCrash == false)
        {
            upCamera.transform.position = new Vector3(0, 0, -50);
            _isCrash = true;
        }
    }
}
