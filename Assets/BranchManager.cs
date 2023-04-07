using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public GameObject branchPrefab;

    public CatPools catPools;

    public void CreateBranch(int numberOfRow)
    {
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject branch = Instantiate(branchPrefab, transform);
            branch.GetComponent<Branch>().idBranch = i;
        }
    }

}
