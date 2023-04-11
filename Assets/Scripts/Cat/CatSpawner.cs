using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    // CatSpawner
    // CatPools and Branch
    // Branch spawn Cat
    // isntance

    public static CatSpawner instance;

    public CatPools catPools;

    public int numberOfRow;

    void OnEnable()
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
            if (catPools.catPool.Count > 0 && branch.catStack.Count < 4)
            {
                GameObject cat = catPools.GetCatRandom();
                branch.catStack.Push(cat);
                cat.transform.SetParent(branch.transform);
                cat.transform.localPosition = new Vector3(-2.4f + 1.8f*(branch.catStack.Count - 1), 0, 0);
            }
        }
    }

    public void RemoveCat(Branch branch)
    {
        while (branch.catStack.Count > 0)
        {
            GameObject cat = branch.catStack.Pop();
            // send cat to CatPools
            catPools.catPool.Add(cat);
            cat.SetActive(false);
            // Destroy(cat);
        }
    }

    public void ReturnCat(Branch branch)
    {
        // return 4 cat RemoveCat latest
        for (int i = 0; i < 4; i++)
        {
            GameObject cat = catPools.catPool[catPools.catPool.Count - 1];
            catPools.catPool.Remove(cat);
            branch.catStack.Push(cat);
            cat.transform.SetParent(branch.transform);
            cat.transform.localPosition = new Vector3(-2.4f + 1.8f*(branch.catStack.Count - 1), 0, 0);
            cat.SetActive(true);
            cat.GetComponent<Cat>().SetIdle();
        }
    }


    // public void ReturnCat(Branch branch)
    // {
    //     while (branch.catStack.Count > 0)
    //     {
    //         GameObject cat = branch.catStack.Pop();
    //         cat.transform.SetParent(transform);
    //         cat.transform.localPosition = new Vector3(0, 0, 0);
    //         cat.SetActive(false);
    //         catPools.catPool.Add(cat);
    //         // cat.SetActive(false);
    //         // catPools.catPool.Add(cat);
    //     }
    // }

}
