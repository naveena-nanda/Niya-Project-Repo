using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    private PlayerArray playerArray;

    public int posOrNeg;
    

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(setConfirm);

        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerArray = GameObject.FindObjectOfType<PlayerArray>().GetComponent<PlayerArray>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void setConfirm()
    {
        if (posOrNeg > 0)
        {
            gameManager.confirmClicked = 1;
      
            gameManager.lastPickedTree = gameManager.clickedTile.tileTree;
            gameManager.lastPickedSymbol = gameManager.clickedTile.tileSymbol;

            gameManager.pickedTile = gameManager.clickedTile;

            gameManager.lastPickedText.text = "Last Picked: " +
                gameManager.pickedTile.tileTree.name +
                " " + gameManager.pickedTile.tileSymbol.name;

            gameManager.pickedTile.open = false;
            playerArray.PlacePlayer(gameManager.pickedTile, playerArray.activePlayer);
//            Debug.Log(playerArray.totalCardSpaces[gameManager.pickedTile.xPos, gameManager.pickedTile.yPos].open);
            playerArray.switchPlayers();
            

        }
        else if (posOrNeg < 0)
        {
            gameManager.confirmClicked = -1;
        }
     //   Debug.Log(gameObject.name + " was clicked");
        gameManager.confirmScreen.SetActive(false);
        gameManager.canClick = true;

    }
}
