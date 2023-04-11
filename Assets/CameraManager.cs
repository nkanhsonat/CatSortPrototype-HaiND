using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (Screen.height / Screen.width > 1.9f)
        {
            mainCamera.orthographicSize = 11f;
        }
        else
        {
            mainCamera.orthographicSize = 10f;
        }
    }
}
