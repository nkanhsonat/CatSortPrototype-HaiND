using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int numberOfRow = 6;
    
    public BranchManager branchManager;

    public CatPools catPools;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        catPools.CreateCatPool(numberOfRow);
    }
}
