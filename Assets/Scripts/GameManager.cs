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

    private void Awake() {
        instance = this;
    }

    void OnEnable()
    {
        numberOfRow = menuController.numberOfRows;
        catPools.CreateCatPool(numberOfRow);
    }

    public void OnGameOver(){
        menuController.ReplayGame();
    }

}
