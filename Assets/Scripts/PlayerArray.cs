using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerArray : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerToken;
    public GameObject playerToken2;

    

    public GameObject activePlayer;

    public ParticleSystem placeEffect;

    public bool win = false;
    
    public int[,] cardSpaces = new int[4, 4];
    public int[,] cardSpaces2 = new int[4, 4];
    public Tile[,] totalCardSpaces = new Tile[4, 4];

    public int turns = 0;

    private GameManager gameManagerScript;

    void Start()
    {
        Debug.Log(cardSpaces[0, 0]);
        
        gameManagerScript = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchPlayers()
    {
        turns += 1;
        //     Debug.Log(turns);
        if (!gameManagerScript.gameOver)
        {
            if (activePlayer == playerToken)
            {
                activePlayer = playerToken2;
                gameManagerScript.playerTurnText.text = "Green's/Player 2's Turn";
                //            Debug.Log("switched to 2");
            }
            else if (activePlayer == playerToken2)
            {
                activePlayer = playerToken;
                gameManagerScript.playerTurnText.text = "Gold's/Player 1's Turn";
                //           Debug.Log("switched to 1");
            }
        }
        
    }


    public void PlacePlayer(Tile tile, GameObject player)
    {
         GameObject newPlayerToken = Instantiate(player,
            new Vector3(tile.transform.position.x, tile.transform.position.y + 0.25f, tile.transform.position.z),
            playerToken.transform.rotation);
        Instantiate(placeEffect,
            new Vector3(tile.transform.position.x, tile.transform.position.y + 0.25f, tile.transform.position.z),
            placeEffect.transform.rotation);
        if (player == playerToken)
        {
            cardSpaces[tile.xPos, tile.yPos] = 1;
            bool win = checkWinConditions(cardSpaces,  tile);
            Debug.Log(win);
            if (win)
            {
                gameManagerScript.GameOver();
            }
        }
        else if (player == playerToken2)
        {
            cardSpaces2[tile.xPos, tile.yPos] = 1;
            bool win = checkWinConditions(cardSpaces2, tile);
            Debug.Log(win);
            if (win)
            {
                gameManagerScript.GameOver();
            }
        }

        
        

      //  Debug.Log(cardSpaces);

    }

    public bool checkWinConditions(int[,] playerArray, Tile tile)
    {
        //every column
        for (int i = 0; i < 4; i++)
        {
            if (playerArray[0, i] == 1 && playerArray[1, i] == 1 && playerArray[2, i] == 1 && playerArray[3, i] == 1)
            {
                return true;
            }
        }
        //every row
        for (int i = 0; i < 4; i++)
        {
            if (playerArray[i, 0] == 1 && playerArray[i, 1] == 1 && playerArray[i, 2] == 1 && playerArray[i, 3] == 1)
            {
                return true;
            }
        }
        //diagonal going down right
        if (playerArray[0, 0] == 1 && playerArray[1, 1] == 1 && playerArray[2, 2] == 1 && playerArray[3, 3] == 1)
        {
            return true;
        }
        //diagonal going down left
        if (playerArray[3, 0] == 1 && playerArray[2, 1] == 1 && playerArray[1, 2] == 1 && playerArray[0, 3] == 1)
        {
            return true;
        }

        //testing boxes
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
//                Debug.Log((i, j));
                if(playerArray[i, j] ==  1 && playerArray[i+ 1, j] == 1 && playerArray[i, j+1] == 1 && playerArray[i + 1, j + 1] == 1)
                {
                    return true;
                }
            }
        }

        //testing possible moves left
        if (turns >= 10)
        {
            checkIfPossibleMovesLeft(tile);
        }

        return false;
    }

    private bool checkIfPossibleMovesLeft(Tile tile)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
               if(GeneralCheckIfValid(totalCardSpaces[i, j], tile))
                {
                    return true;
                } 
            }
        }
        return false;
    }

    private bool GeneralCheckIfValid(Tile tileA, Tile tileB)
    {
        if ((tileA.tileTree == tileB.tileTree || tileA.tileSymbol == tileB.tileSymbol) && tileB.open)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
