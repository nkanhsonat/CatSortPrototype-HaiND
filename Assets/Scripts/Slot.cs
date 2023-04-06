using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Bird bird;

    public int idSlot;

    public int idBirdHold;

    public bool IsHasBird()
    {
        return bird != null;
    }

    public void SetBirdHold()
    {
        if (IsHasBird())
        {
            idBirdHold = bird.idBird;
        }
        else
        {
            idBirdHold = -1;
        }
    }

    void Start()
    {
        SetBirdHold();
    }
}
