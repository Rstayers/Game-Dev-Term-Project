using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Helpers
{
    

    public static Vector3 GetCameraForward(Transform playerCamera)
    {
        Vector3 forward = playerCamera.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public static Vector3 GetCameraRight(Transform playerCamera)
    {
        Vector3 right = playerCamera.right;
        right.y = 0;
        return right.normalized;
    }
}

