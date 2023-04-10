using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    // CatSpawner
    // CatPools and Branch
    // Branch spawn Cat
    public CatPools catPools;

    public int numberOfRow;

    void Start()
    {
        //get CatPools from GameManager
        catPools = GameManager.instance.catPools;

        //get numberOfRow from GameManager
        numberOfRow = GameManager.instance.numberOfRow;
    }

    public void SpawnCat(Branch branch)
    {
        for (int i = 0; i < numberOfRow - 2; i++)
        {
            // Spawn Cat
            if (catPools.catPool.Count > 0)
            {
                GameObject cat = catPools.GetCatRandom();
                cat.transform.SetParent(branch.transform);
                cat.transform.localPosition = new Vector3(0, 0, 0);

                branch.catStack.Push(cat);
            }
        }
    }
}
