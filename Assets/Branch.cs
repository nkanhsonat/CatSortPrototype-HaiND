using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour, IObservable
{
    public int idBranch;

    // Stack of Cats
    public Stack<GameObject> catStack;

    public CatSpawner catSpawner;

    private List<IObserver> observers = new List<IObserver>();

    void Start()
    {
        catStack = new Stack<GameObject>();

        // catSpawner finds by tag
        catSpawner =
            GameObject
                .FindGameObjectWithTag("CatSpawner")
                .GetComponent<CatSpawner>();
        catSpawner.SpawnCat(this);
        FlipBranch();
        UpBranch();
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
            int idCatTopOfStack = catStack.Peek().GetComponent<Cat>().idCat;
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
        catSpawner.ReturnCat(this);
        catStack.Clear();
    }

    public int CatAbleMove()
    {
        int catAbleMove = 0;
        int idCatTopOfStack = catStack.Peek().GetComponent<Cat>().idCat;
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
}
