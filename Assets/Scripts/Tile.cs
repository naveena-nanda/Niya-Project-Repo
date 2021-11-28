using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Tile : MonoBehaviour
{

    public GameObject tileTree;
    public GameObject tileSymbol;

    private GameManager gameManagerScript;
    private PlayerArray playerArray;

    public int xPos;
    public int yPos;

    public bool open = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        playerArray = GameObject.FindObjectOfType<PlayerArray>().GetComponent<PlayerArray>();
        // = GameObject.FindObjectOfType<Confirm>().GetComponent<Confirm>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnMouseDown()
    {
//        Debug.Log(gameManagerScript.lastPickedSymbol);

        if (gameManagerScript.canClick && !gameManagerScript.gameOver)
        {
            if (playerArray.turns == 0)
            {
                gameManagerScript.pickedTile = transform.GetComponent<Tile>();

                gameManagerScript.lastPickedTree = tileTree;
                gameManagerScript.lastPickedSymbol = tileSymbol;

                gameManagerScript.lastPickedText.text = "Last Picked: " +
                    gameManagerScript.lastPickedTree.name +
                    " " + gameManagerScript.lastPickedSymbol.name;


            }

            gameManagerScript.clickedTile = transform.GetComponent<Tile>();

            gameManagerScript.clickedSymbol = tileSymbol;
            gameManagerScript.clickedTree = tileTree;

            //      Debug.Log((xPos, yPos));

            gameManagerScript.lastClickedText.text = "Last Clicked: " +
                    gameManagerScript.clickedSymbol.name +
                    " " + gameManagerScript.clickedTree.name;

            gameManagerScript.CheckIfValid();
            //      Debug.Log("validity checked");

            if (gameManagerScript.validMove)
            {
                gameManagerScript.confirmScreen.SetActive(true);
                gameManagerScript.canClick = false;
            }
        }
        
       

    }

   
}
