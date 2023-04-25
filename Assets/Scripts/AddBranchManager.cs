using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBranchManager : MonoBehaviour
{
    public static AddBranchManager instance;

    public Button addBranchButton;

    public bool added = false;

    void Awake()
    {
        instance = this;
    }

    public void AddBranch()
    {
        if (added || RestartManager.instance.isRestarting) return;
        added = true;
        addBranchButton.interactable = false;
        BranchManager.instance.AddBranch();
    }

    public void EnableAddBranchButton()
    {
        added = false;
        addBranchButton.interactable = true;
    }

}
