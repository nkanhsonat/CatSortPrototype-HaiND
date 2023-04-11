using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int numberOfRow;
    
    public BranchManager branchManager;

    public CatPools catPools;

    public MenuController menuController;

    public bool isRestarting = false;

    private void Awake() {
        instance = this;
    }

    void OnEnable()
    {
        numberOfRow = menuController.numberOfRows;
        catPools.CreateCatPool(numberOfRow);
    }

    public void RestartGame(){
        if (isRestarting) return;
        isRestarting = true;
        branchManager.RestartGame();
        // menuController.ReplayRandomGame();
        // wait 2s to call menuController.ReplayRandomGame()
        Invoke("ReplayRandomGame", 1f);
    }

    public void ReplayRandomGame(){
        menuController.ReplayRandomGame();
        isRestarting = false;
    }

}
