using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour, IObserver, MoveManager
{
    public static BranchManager instance;

    public BranchSpawner branchSpawner;

    public Branch[] branches;

    public Branch selectedBranch;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        branchSpawner.Spawn(GameManager.instance.numberOfRow);
        branches = FindObjectsOfType<Branch>();
        selectedBranch = null;
        foreach (Branch branch in branches)
        {
            branch.AddObserver(this);
        }
    }

    public void OnBranchSelected(Branch branch)
    {
        Debug.Log("BranchManager: OnBranchSelected");
        if (selectedBranch == null)
        {
            if (!branch.IsEmpty())
            {
                selectedBranch = branch;
                selectedBranch.SetSelectedAnimation();
            }
        }
        else
        {
            if (selectedBranch == branch)
            {
                selectedBranch.SetIdleAnimation();
                selectedBranch = null;
            }
            else if (branch.IsFull())
            {
                selectedBranch.SetIdleAnimation();
                selectedBranch = branch;

                selectedBranch.SetSelectedAnimation();
            }
            else
            {
                if (IsMoveValid(selectedBranch, branch))
                {
                    selectedBranch.SetIdleAnimation();
                    OnMove (selectedBranch, branch);
                    selectedBranch = null;
                }
                else
                {
                    selectedBranch.SetIdleAnimation();
                    selectedBranch = branch;
                    selectedBranch.SetSelectedAnimation();
                }
            }
        }
    }

    public void OnMove(Branch branch1, Branch branch2)
    {
        int numberOfCatMove = branch1.CatAbleMove();
        for (int i = 0; i < numberOfCatMove; i++)
        {
            if (IsMoveValid(branch1, branch2))
            {
                GameObject cat = branch1.catStack.Pop();
                branch2.catStack.Push (cat);
                cat.transform.SetParent(branch2.transform);
                cat.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                break;
            }
        }
    }

    public bool IsMoveValid(Branch branch1, Branch branch2)
    {
        if (branch2.IsEmpty())
        {
            return true;
        }
        if (branch1.IsEmpty() || branch2.IsFull())
        {
            return false;
        }
        int idCatTopOfStack1 =
            branch1.catStack.Peek().GetComponent<Cat>().idCat;
        int idCatTopOfStack2 =
            branch2.catStack.Peek().GetComponent<Cat>().idCat;
        if (idCatTopOfStack1 == idCatTopOfStack2)
        {
            return true;
        }
        return false;
    }
}
