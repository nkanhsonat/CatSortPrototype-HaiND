using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int numberOfRow;
    
    public CatPools catPools;

    private void Awake() {
        instance = this;

        Application.targetFrameRate = 60;
    }

    void OnEnable()
    {
        numberOfRow = MenuController.instance.numberOfRows;
        catPools.CreateCatPool(numberOfRow);
    }

    public void RestartGame(){
        if (RestartManager.instance.isRestarting) return;
        RestartManager.instance.isRestarting = true;
        BranchManager.instance.RestartGame();
        Invoke("ReplayRandomGame", 1f);
    }

    public void ReplayRandomGame(){
        MenuController.instance.ReplayRandomGame();
        RestartManager.instance.isRestarting = false;
    }

}
