using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public Button button;

    [SerializeField]
    // public TMPro.TMP_InputField numberOfRowInput;
    public TMP_InputField numberOfRowInput;
    public GameObject gameManager;
    public GameObject game;
    public GameObject menu;

    public int numberOfRows;

    private void LoadGame()
    {
        gameManager.SetActive(true);
        // enable game object
        game.SetActive(true);
        menu.SetActive(false);
    }
    public void ReplayRandomGame(){
        gameManager.SetActive(false);
        game.SetActive(false);
        numberOfRows = Random.Range(6, 10);

        gameManager.SetActive(true);
        game.SetActive(true);

    }
    
    public void OnButtonPress()
    {
        try {
            numberOfRows = int.Parse(numberOfRowInput.text);
            if (numberOfRows > 5 && numberOfRows < 10)
            {
                LoadGame();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }
}
