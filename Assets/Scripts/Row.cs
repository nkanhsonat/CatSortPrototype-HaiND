using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public int idRow;

    public GameObject slotPrefab;

    public List<Slot> slotOfBirds;

    void Start()
    {
        // create 4 slotOfBirds has Row is parent
        for (int i = 0; i < 4; i++)
        {
            GameObject slot = Instantiate(slotPrefab, transform);
            slot.transform.localPosition = Vector3.zero;
            Slot slotScript = slot.GetComponent<Slot>();
            slotScript.idSlot = i;
            slotOfBirds.Add (slotScript);
        }
        AddBirdToEachSlot();

        if (idRow % 2 == 1){
            // scale x = -x
            // Get transform of row
            Transform rowTransform = GetComponent<Transform>();
            // Get scale of row
            Vector3 rowScale = rowTransform.localScale;
            // Set scale of row
            rowTransform.localScale = new Vector3(-rowScale.x, rowScale.y, rowScale.z);
        }

    }

    public bool CheckRowClear()
    {
        // check if row is full and slot has same idBirdHold
        int numberOfBirdMove = GetNumberOfBirdMove();
        if (numberOfBirdMove == 4)
        {
            // check slots has same idBirdHold
            Slot[] slots = GetComponentsInChildren<Slot>();
            int idBirdHold = slots[0].idBirdHold;
            for (int i = 1; i < slots.Length; i++)
            {
                if (slots[i].idBirdHold != idBirdHold)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public int NumberOfBirdOnRow()
    {
        Slot[] slots = GetComponentsInChildren<Slot>();
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsHasBird())
            {
                count++;
            }
        }
        return count;
    }

    public void AddBirdToEachSlot()
    {
        // get all slotOfBirds
        Slot[] slots = GetComponentsInChildren<Slot>();

        // Get poolOfBirds from Rows
        Rows rows = GetComponentInParent<Rows>();
        List<GameObject> poolOfBirds = rows.poolOfBirds;

        // from poolOfBirds, get bird and add to slotOfBirds
        for (int i = 0; i < slots.Length; i++)
        {
            //random
            if (poolOfBirds.Count == 0)
            {
                break;
            }
            else
            {
                GameObject bird =
                    poolOfBirds[Random.Range(0, poolOfBirds.Count)];
                poolOfBirds.Remove (bird);
                bird.SetActive(true);
                bird.transform.SetParent(slots[i].transform);
                bird.transform.localPosition = Vector3.zero;
                slots[i].bird = bird.GetComponent<Bird>();
            }
        }
    }

    public int GetNumberOfBirdMove()
    {
        int numberOfBirdOnRow = NumberOfBirdOnRow();
        int numberOfBirdMove = 0;
        Slot[] slots = GetComponentsInChildren<Slot>();
        if (numberOfBirdOnRow == 0)
        {
            return 0;
        }
        else
        {
            int idBirdMove = slots[numberOfBirdOnRow - 1].idBirdHold;
            numberOfBirdMove = 1;
            for (int i = numberOfBirdOnRow - 2; i >= 0; i--)
            {
                if (slots[i].idBirdHold == idBirdMove)
                {
                    numberOfBirdMove++;
                }
                else
                {
                    break;
                }
            }
        }
        return numberOfBirdMove;
    }
}
