using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    public GameObject branchPrefab;
    // public void Spawn(int numberOfRow){
    //     // Instantiate(branchPrefab, transform.position, Quaternion.identity);
    //     for (int i = 0; i < numberOfRow; i++)
    //     {
    //         GameObject branch = Instantiate(branchPrefab, transform.position, Quaternion.identity);
    //         branch.GetComponent<Branch>().idBranch = i;
    //         branch.transform.SetParent(transform);
    //         // branch.transform.localPosition = new Vector3(0, 0, 0);
    //         // half of row = ceil(numberOfRow/2)
    //         int halfOfRow = Mathf.CeilToInt(numberOfRow/2f);
    //         Vector3 position = Vector3.zero;
    //         if (i % 2 == 0){
    //         position = new Vector3(-3.3f, 3 + (halfOfRow - 3) * 1.5f - 1.5f*i, 0);
    //         }
    //         else {
    //         position = new Vector3(3.3f, 3 + (halfOfRow - 3) * 1.5f - 1.5f*i, 0);
    //         }
    //         branch.transform.localPosition = position;
    //     }
    // }
    public Branch[] Spawn(int numberOfRow){
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject branch = Instantiate(branchPrefab, transform.position, Quaternion.identity);
            branch.GetComponent<Branch>().idBranch = i;
            branch.transform.SetParent(transform);
            // branch.transform.localPosition = new Vector3(0, 0, 0);
            // half of row = ceil(numberOfRow/2)
            int halfOfRow = Mathf.CeilToInt(numberOfRow/2f);
            Vector3 position = Vector3.zero;
            if (i % 2 == 0){
            position = new Vector3(-3.3f, 3 + (halfOfRow - 3) * 1.5f - 1.5f*i, 0);
            }
            else {
            position = new Vector3(3.3f, 3 + (halfOfRow - 3) * 1.5f - 1.5f*i, 0);
            }
            branch.transform.localPosition = position;
        }
        return GetComponentsInChildren<Branch>();

    }

    public void RemoveBranch(){
        while (transform.childCount > 0)
        {
            GameObject branch = transform.GetChild(0).gameObject;
            Destroy(branch);
        }
    }
}
