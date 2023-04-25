using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    public static BranchSpawner instance;

    public GameObject branchPrefab;

    private void Awake()
    {
        instance = this;
    }

    public List<Branch> Spawn(int numberOfRow)
    {
        float x = 9.9f;
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject branch =
                Instantiate(branchPrefab,
                transform.position,
                Quaternion.identity);
            branch.GetComponent<Branch>().idBranch = i;
            branch.transform.SetParent (transform);
            branch.GetComponent<Branch>().SpawnCatWhenStart();
            Vector3 position = Vector3.zero;
            if (i % 2 == 0)
            {
                position =
                    new Vector3(-x, 4.5f - Mathf.FloorToInt(i / 2f) * 3f, 0);
            }
            else
            {
                position =
                    new Vector3(x, 4.5f - Mathf.FloorToInt(i / 2f) * 3f, 0);
            }
            branch.transform.localPosition = position;
        }
        return new List<Branch>(GetComponentsInChildren<Branch>());
    }

    public void RemoveBranch()
    {
        while (transform.childCount > 0)
        {
            GameObject branch = transform.GetChild(0).gameObject;
            Destroy (branch);
        }
    }

    public Branch SpawnOne()
    {
        GameManager.instance.numberOfRow++;
        int numberOfRow = GameManager.instance.numberOfRow - 1;
        GameObject branch =
            Instantiate(branchPrefab, transform.position, Quaternion.identity);
        branch.GetComponent<Branch>().idBranch = numberOfRow;
        branch.transform.SetParent (transform);
        Vector3 position = Vector3.zero;
        if (numberOfRow % 2 == 0)
        {
            position =
                new Vector3(-9.9f,
                    4.5f - Mathf.FloorToInt(numberOfRow / 2f) * 3f,
                    0);
        }
        else
        {
            position =
                new Vector3(9.9f,
                    4.5f - Mathf.FloorToInt(numberOfRow / 2f) * 3f - 1.5f,
                    0);
        }
        branch.transform.localPosition = position;
        return branch.GetComponent<Branch>();
    }
}
