using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int numberOfRow;
    
    public BranchManager branchManager;

    public CatPools catPools;

    public bool isRestarting = false;

    private void Awake() {
        instance = this;
    }

    void OnEnable()
    {
        numberOfRow = MenuController.instance.numberOfRows;
        catPools.CreateCatPool(numberOfRow);
    }

    public void RestartGame(){
        if (isRestarting) return;
        isRestarting = true;
        branchManager.RestartGame();
        Invoke("ReplayRandomGame", 1f);
    }

    public void ReplayRandomGame(){
        MenuController.instance.ReplayRandomGame();
        isRestarting = false;
    }

}
