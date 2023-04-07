using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSlot : MonoBehaviour
{
    public int idSlot;
    public int idCatHolding;

    public CatPools catPools;

    void Start(){
        idCatHolding = -1;
        catPools = GameManager.instance.catPools;
        
        // Take every cat from the pool and put it in the slot until the pool is empty
        if (catPools.GetCatPoolCount() > 0)
        {
            GameObject cat = catPools.GetCatRandomFromPool();
            cat.transform.SetParent(transform);
            cat.transform.localPosition = Vector3.zero;
            cat.SetActive(true);
            idCatHolding = cat.GetComponent<Cat>().idCat;
        }

    }
}
