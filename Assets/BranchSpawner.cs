using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    public GameObject branchPrefab;

    public void Spawn(int numberOfRow){
        // Instantiate(branchPrefab, transform.position, Quaternion.identity);
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject branch = Instantiate(branchPrefab, transform.position, Quaternion.identity);
            branch.transform.SetParent(transform);
            branch.transform.localPosition = new Vector3(0, 0, 0);
            // Set idBranch
            branch.GetComponent<Branch>().idBranch = i;
        }
    }

}
