using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPools : MonoBehaviour
{
    public static CatPools instance;

    public List<GameObject> catPool;

    public GameObject[] catPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void CreateCatPool(int numberOfRow)
    {
        for (int i = 0; i < numberOfRow - 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject cat =
                    Instantiate(catPrefab[i],
                    transform.position,
                    Quaternion.identity);
                cat.SetActive(false);
                cat.GetComponent<Cat>().idCat = i;
                catPool.Add (cat);
            }
        }
    }

    public GameObject GetCatRandom()
    {
        GameObject cat = catPool[Random.Range(0, catPool.Count)];
        cat.SetActive(true);
        catPool.Remove (cat);
        return cat;
    }

    public void ClearCatPool()
    {
        catPool.Clear();
    }

}
