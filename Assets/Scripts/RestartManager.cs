using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{

    public static RestartManager instance;

    public bool isRestarting = false;

    void Awake()
    {
        instance = this;
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
