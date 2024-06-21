using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    private bool _isCrash;
    private void OnCollisionEnter(Collision other)
    {
        if (other != null && _isCrash == false)
        {
            _isCrash = true;
            BallSpawnPoint.Instance.SetCameraPosition();
        }
    }
}
