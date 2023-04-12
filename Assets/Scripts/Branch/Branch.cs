using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Branch : MonoBehaviour, IObservable
{
    public int idBranch;

    public Stack<GameObject> catStack;

    private List<IObserver> observers = new List<IObserver>();

    public bool isCheering = false;

    public bool isJumping = false;

    void Start()
    {
        catStack = new Stack<GameObject>();
        CatSpawner.instance.SpawnCat(this);
        FlipBranch();
        UpBranch();
        SlipInHorizontal();
    }

    public void FlipBranch()
    {
        if (idBranch % 2 == 1)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    public void UpBranch(){
        // if number of row is even
        if (GameManager.instance.numberOfRow % 2 == 0){
            if (idBranch % 2 == 1){
                // move up 1.5f y
                transform.position += new Vector3(0, 1.5f, 0);
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Branch " + idBranch + " clicked");
        NotifyObservers();
    }

    public int GetIdCatOnTop()
    {
        return catStack.Peek().GetComponent<Cat>().idCat;
    }

    public bool IsFull()
    {
        return catStack.Count == 4;
    }

    public bool IsEmpty()
    {
        return catStack.Count == 0;
    }

    public bool IsBranchWinning()
    {
        if (IsFull())
        {
            int idCatTopOfStack = GetIdCatOnTop();
            foreach (GameObject cat in catStack)
            {
                if (cat.GetComponent<Cat>().idCat != idCatTopOfStack)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public void SetCheerAnimation()
    {
        foreach (GameObject cat in catStack)
        {
            cat.GetComponent<Cat>().SetCheer();
        }
    }

    public void OnClear()
    {
        CatSpawner.instance.RemoveCat(this);
        catStack.Clear();
    }

    public void OnReturn()
    {
        CatSpawner.instance.ReturnCat(this);
    }

    public int CatAbleMove()
    {
        int catAbleMove = 0;
        int idCatTopOfStack = GetIdCatOnTop();
        foreach (GameObject cat in catStack)
        {
            if (cat.GetComponent<Cat>().idCat == idCatTopOfStack)
            {
                catAbleMove++;
            }
            else
            {
                break;
            }
        }
        return catAbleMove;
    }

    public void SetSelectedAnimation()
    {
        for (int i = 0; i < CatAbleMove(); i++)
        {
            catStack.ToArray()[i].GetComponent<Cat>().SetSelect();
        }
    }

    public void SetIdleAnimation()
    {
        foreach (GameObject cat in catStack)
        {
            cat.GetComponent<Cat>().SetIdle();
        }
    }

    public void AddObserver(IObserver observer)
    {
        observers.Add (observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove (observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.OnBranchSelected(this);
        }
    }

    public void SlipOutHorizontal()
    {
        // move from x to x * 3
        transform.DOMoveX(transform.position.x * 3, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void SlipInHorizontal()
    {
        transform.DOMoveX(transform.position.x / 3, 0.5f);
    }

}
