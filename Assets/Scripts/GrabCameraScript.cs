using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrabCameraScript : MonoBehaviour
{

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(camera.projectionMatrix);
        var radAngle = camera.fieldOfView * Mathf.Deg2Rad;
        var radHFOV = 2 * Math.Atan(Mathf.Tan(radAngle / 2) * camera.aspect);
        var hFOV = Mathf.Rad2Deg * radHFOV;
        Debug.Log(hFOV);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
