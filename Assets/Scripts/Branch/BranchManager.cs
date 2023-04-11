using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BranchManager : MonoBehaviour, IObserver, MoveManager
{
    public static BranchManager instance;

    public BranchSpawner branchSpawner;

    public Branch[] branches;

    public Branch selectedBranch;

    public ActionManager actionManager;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        branches = branchSpawner.Spawn(GameManager.instance.numberOfRow);
        selectedBranch = null;
        foreach (Branch branch in branches)
        {
            branch.AddObserver(this);
        }
    }

    public void OnBranchSelected(Branch branch)
    {
        if (branch.isJumping || branch.isCheering)
        {
            return;
        }
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
        int movedCat = 0;
        for (int i = 0; i < numberOfCatMove; i++)
        {
            if (IsMoveValid(branch1, branch2))
            {
                movedCat++;
                GameObject cat = branch1.catStack.Pop();
                cat.GetComponent<Cat>().SetJumpAndLanding();
                branch2.catStack.Push (cat);
                cat.transform.SetParent(branch2.transform);

                Vector3 newPosition =
                    new Vector3(-2.4f + 1.8f * (branch2.catStack.Count - 1),
                        0,
                        0);
                branch2.isJumping = true;
                cat
                    .transform
                    .DOLocalJump(newPosition, 3f, 1, 0.5f)
                    .OnComplete(() =>
                    {
                        branch2.isJumping = false;
                        if ((branch1.idBranch + branch2.idBranch) % 2 != 0)
                        {
                            cat.GetComponent<Cat>().FlipCat();
                        }
                    });
            }
            else
            {
                break;
            }
        }

        if (movedCat > 0)
        {
            actionManager.AddAction(new Action(branch1, branch2, movedCat));
        }

        if (branch2.IsBranchWinning())
        {
            branch2.SetCheerAnimation();
            branch2.isCheering = true;
            DOVirtual
                .DelayedCall(1.5f,
                () =>
                {
                    branch2.OnClear();
                    branch2.isCheering = false;
                    if (IsGameOver())
                    {
                        GameManager.instance.RestartGame();
                    }
                });
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
        if (branch1.GetIdCatOnTop() == branch2.GetIdCatOnTop())
        {
            return true;
        }
        return false;
    }

    public bool IsGameOver()
    {
        foreach (Branch branch in branches)
        {
            if (!branch.IsEmpty())
            {
                return false;
            }
        }
        return true;
    }

    public void DestroyAllBranch()
    {
        // remove branch from branches and destroy
        foreach (Branch branch in branches)
        {
            branch.SlipOutHorizontal();            
        }   
    }

    public void RestartGame()
    {
        foreach (Branch branch in branches)
        {
            branch.SlipOutHorizontal();
        }
        CatPools.instance.ClearCatPool();
    }

}
