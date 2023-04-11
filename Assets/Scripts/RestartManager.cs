using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{

    public BranchManager branchManager;

    public void RestartGame()
    {
        branchManager.RestartGame();
    }

}
