using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public int idBird;

    public void FlipBirdImage(){
        // Get transform of bird
        Transform birdTransform = GetComponent<Transform>();
        // Get scale of bird
        Vector3 birdScale = birdTransform.localScale;
        // Set scale of bird
        birdTransform.localScale = new Vector3(-birdScale.x, birdScale.y, birdScale.z);
    }

}
