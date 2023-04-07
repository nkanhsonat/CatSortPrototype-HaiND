using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPools : MonoBehaviour
{
    public List<GameObject> catPool;

    public GameObject[] catPrefab;

    public void CreateCatPool(int numberOfRow)
    {
        for (int i = 0; i < numberOfRow - 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject cat = Instantiate(catPrefab[i], transform);
                cat.SetActive(false);
                catPool.Add(cat);
            }
        }
    }

    public GameObject GetCatRandomFromPool()
    {
        int randomIndex = Random.Range(0, catPool.Count);
        GameObject cat = catPool[randomIndex];
        catPool.RemoveAt(randomIndex);
        return cat;
    }

    public int GetCatPoolCount()
    {
        return catPool.Count;
    }

}
