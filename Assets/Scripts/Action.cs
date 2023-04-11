using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Action
{
    public Branch fromBranch;
    public Branch toBranch;
    public int movedCat;

    public Action(Branch fromBranch, Branch toBranch, int movedCat)
    {
        this.fromBranch = fromBranch;
        this.toBranch = toBranch;
        this.movedCat = movedCat;
    }

    public void Undo()
    {
        for (int i = 0; i < movedCat; i++)
        {
            GameObject cat = toBranch.catStack.Pop();
            cat.GetComponent<Cat>().SetJumpAndLanding();
            fromBranch.catStack.Push(cat);
            cat.transform.SetParent(fromBranch.transform);

            Vector3 newPosition = new Vector3(-2.4f + 1.8f * (fromBranch.catStack.Count - 1), 0, 0);
            cat.transform.DOLocalJump(newPosition, 3f, 1, 0.5f).OnComplete(() =>
            {
                if ((toBranch.idBranch + fromBranch.idBranch) % 2 != 0){
                    cat.GetComponent<Cat>().FlipCat();
                }
            });
        }
    }
}
