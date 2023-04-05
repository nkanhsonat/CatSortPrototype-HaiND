using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rows : MonoBehaviour
{
    public GameObject rowPrefab;

    public GameObject[] birdPrefab;

    public int numberOfRow = 6;

    public Row pickedRow;

    List<GameObject> poolOfBirds;

    void Start()
    {
        pickedRow = null;
        poolOfBirds = new List<GameObject>();

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

        //create rows
        for (int i = 0; i < numberOfRow; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            Row rowScript = row.GetComponent<Row>();
            rowScript.idRow = i;

            //display birds on row, row has horizontal layout group
            for (int j = 0; j < 4; j++)
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

                    // add bird to row
                    rowScript.birds.Add(bird.GetComponent<Bird>());
                    poolOfBirds.RemoveAt(poolOfBirds.IndexOf(bird));
                    bird.SetActive(true);
                    bird.transform.SetParent(row.transform);
                    bird.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    void Update()
    {
        OnClick();
        OnClearRow();
    }

    // each row has collider
    // if click mouse on row, red
    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Row row = hit.collider.GetComponent<Row>();
                if (row != null)
                {
                    if (pickedRow == null)
                    {
                        pickedRow = row;
                        //highlight
                        pickedRow.GetComponent<SpriteRenderer>().color =
                            Color.red;
                    }
                    else {
                        // blue
                        OnBirdMove(pickedRow, row);
                        pickedRow.GetComponent<SpriteRenderer>().color =
                            Color.white;
                        pickedRow = null;
                    }
                }
            }
        }
    }

    public void MoveBird(Bird bird, Row to)
    {
        // move bird position
        bird.transform.SetParent(to.transform);
        bird.transform.localPosition = Vector3.zero;
    }

    public void OnBirdMove(Row from, Row to)
    {

        int numberOfBirdMove = GetNumberOfBirdMove(from);
        // check if bird can move
        if (from.birds.Count == 0 || to.birds.Count == 4)
        {
            return;
        }

        // check if bird can move
        if (to.birds.Count == 0)
        {
            for (int i = 0; i < numberOfBirdMove; i++)
            {
                Bird bird = from.birds[from.birds.Count - 1];
                MoveBird(bird, to);
                from.birds.RemoveAt(from.birds.Count - 1);
                to.birds.Add(bird);
            }
        }
        else
        {
            // check if bird can move
            if (from.birds[from.birds.Count - 1].idBird ==
                to.birds[to.birds.Count - 1].idBird)
            {
                for (int i = 0; i < numberOfBirdMove; i++)
                {
                    if (to.birds.Count == 4)
                    {
                        break;
                    }
                    Bird bird = from.birds[from.birds.Count - 1];
                    MoveBird(bird, to);
                    from.birds.RemoveAt(from.birds.Count - 1);
                    to.birds.Add(bird);
                }
            }
        }
    }

    private int GetNumberOfBirdMove(Row from)
    {
        int numberOfBirdMove = 1;
        for (int i = from.birds.Count - 1; i > 0; i--)
        {
            if (from.birds[i].idBird == from.birds[i - 1].idBird)
            {
                numberOfBirdMove++;
            }
            else
            {
                break;
            }
        }
        return numberOfBirdMove;
    }

    private void OnClearRow(){
        // clear row
        for (int i = 0; i < transform.childCount; i++)
        {
            Row row = transform.GetChild(i).GetComponent<Row>();
            if (row.birds.Count == 4){
                // all same id
                if (row.birds[0].idBird == row.birds[1].idBird &&
                    row.birds[1].idBird == row.birds[2].idBird &&
                    row.birds[2].idBird == row.birds[3].idBird)
                {
                    for (int j = 0; j < row.birds.Count; j++)
                    {
                        Bird bird = row.birds[j];
                        bird.gameObject.SetActive(false);
                        poolOfBirds.Add(bird.gameObject);
                    }
                    row.birds.Clear();
                }
            }
        }
    }

}
