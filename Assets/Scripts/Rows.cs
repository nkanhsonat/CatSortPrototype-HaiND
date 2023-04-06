using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rows : MonoBehaviour
{
    public GameObject rowPrefab;

    public GameObject[] birdPrefab;

    public int numberOfRow = 6;

    public Row pickedRow;
    public Row lastRow;

    public List<GameObject> poolOfBirds;

    public bool coroutineAllowed = true;

    void Start()
    {
        pickedRow = null;
        lastRow = null;

        // create 4 each bird
        for (int i = 0; i < numberOfRow - 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject bird = Instantiate(birdPrefab[i], transform);
                bird.SetActive(false);
                poolOfBirds.Add (bird);
            }
        }

        // create rows
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            Row rowScript = row.GetComponent<Row>();
            rowScript.idRow = i;
        }

    }


    void Update()
    {
        OnClick();
        if (coroutineAllowed && lastRow != null){
            ClearRow(lastRow);
            lastRow = null;
        }
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0) && coroutineAllowed)
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Row row = hit.collider.GetComponent<Row>();
                if (row != null)
                {
                    if (row == pickedRow)
                    {
                        pickedRow.GetComponent<SpriteRenderer>().color =
                            Color.white;
                        pickedRow = null;
                    }
                    else if (pickedRow == null)
                    {
                        pickedRow = row;
                        pickedRow.GetComponent<SpriteRenderer>().color =
                            Color.red;
                    }
                    else
                    {
                        OnBirdMove (pickedRow, row);
                        pickedRow.GetComponent<SpriteRenderer>().color =
                            Color.white;
                        pickedRow = null;
                        lastRow = row;
                    }
                }
            }
        }
    }

    public void MoveBird(Bird bird, Slot to)
    {
        // bird.transform.position = to.transform.position;
        Vector3 from = bird.transform.position;
        Vector3 toPos = to.transform.position;
        StartCoroutine(MoveBirdLinear(from, toPos, bird));
        bird.transform.SetParent(to.transform);
    }

    public void ClearRow(Row to)
    {
        if (to.CheckRowClear())
        {
            for (int i = to.NumberOfBirdOnRow() - 1; i >= 0; i--)
            {
                Bird bird = to.slotOfBirds[i].bird;
                bird.gameObject.SetActive(false);
                poolOfBirds.Add(bird.gameObject);
                to.slotOfBirds[i].bird = null;
                to.slotOfBirds[i].SetBirdHold();
            }
        }
    }

    IEnumerator MoveBirdLinear(Vector3 from, Vector3 to, Bird bird)
    {
        //Using Vector3.Lerp
        coroutineAllowed = false;
        float timeElapsed = 0;
        float time = 1f;
        while (timeElapsed < time)
        {
            bird.transform.position =
                Vector3.Lerp(from, to, timeElapsed / time);
            timeElapsed += Time.deltaTime * 2;
            yield return null;
        }
        coroutineAllowed = true;
    }

    public void OnBirdMove(Row from, Row to)
    {
        if (from.NumberOfBirdOnRow() == 0 || to.NumberOfBirdOnRow() == 4)
        {
            return;
        }

        int numberOfBirdMove = from.GetNumberOfBirdMove();

        if (IsSameBird(from, to))
        {
            for (int i = 0; i < numberOfBirdMove; i++)
            {
                if (to.NumberOfBirdOnRow() == 4)
                {
                    break;
                }
                Slot fromSlot = from.slotOfBirds[from.NumberOfBirdOnRow() - 1];
                Slot toSlot = to.slotOfBirds[to.NumberOfBirdOnRow()];
                if ((from.idRow + to.idRow) % 2 != 0)
                {
                    fromSlot.bird.FlipBirdImage();
                }
                SwapBird (fromSlot, toSlot);
            }
        }
    }

    public void SwapBird(Slot from, Slot to)
    {
        Bird bird = from.bird;
        MoveBird (bird, to);
        from.bird = to.bird;
        to.bird = bird;
        from.SetBirdHold();
        to.SetBirdHold();
    }

    public bool IsSameBird(Row from, Row to)
    {
        if (to.NumberOfBirdOnRow() == 0)
        {
            return true;
        }
        else
        {
            return from.slotOfBirds[from.NumberOfBirdOnRow() - 1].idBirdHold ==
            to.slotOfBirds[to.NumberOfBirdOnRow() - 1].idBirdHold;
        }
    }

}
