using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public static CatSpawner instance;

    public int numberOfRow;

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        numberOfRow = GameManager.instance.numberOfRow;
    }

    public void SpawnCat(Branch branch)
    {
        for (int i = 0; i < numberOfRow - 2; i++)
        {
            // Spawn Cat
            if (CatPools.instance.catPool.Count > 0 && branch.catStack.Count < 4)
            {
                GameObject cat = CatPools.instance.GetCatRandom();
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
            CatPools.instance.catPool.Add(cat);
            cat.SetActive(false);
            // Destroy(cat);
        }
    }

    public void ReturnCat(Branch branch)
    {
        // return 4 cat RemoveCat latest
        for (int i = 0; i < 4; i++)
        {
            GameObject cat = CatPools.instance.catPool[CatPools.instance.catPool.Count - 1];
            CatPools.instance.catPool.Remove(cat);
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
