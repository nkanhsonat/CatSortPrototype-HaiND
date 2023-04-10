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

    private void LoadGame(int numberOfRows)
    {
        gameManager.SetActive(true);
        GameManager.instance.numberOfRow = numberOfRows;
        // enable game object
        game.SetActive(true);
    }

    // if input field is 6,7,8,9, when press button, load GameScene, and set numberOfRows equal to inputField.text
    public void OnButtonPress()
    {
        int numberOfRows;
        if (int.TryParse(numberOfRowInput.text, out numberOfRows))
        {
            switch (numberOfRows)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                    LoadGame(numberOfRows);
                    break;
                default:
                    break;
            }
        }
    }
}
