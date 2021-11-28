using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject tile;

    public Tile clickedTile;
    public Tile pickedTile;

    public GameObject clickedTree;
    public GameObject clickedSymbol;

    public GameObject lastPickedTree = null;
    public GameObject lastPickedSymbol = null;

    public List<GameObject> trees;
    public List<GameObject> symbols;

    public List<(GameObject, GameObject)> allCombos;

    public GameObject maple; public GameObject cherry; public GameObject pine; public GameObject iris;
    public GameObject sun; public GameObject flag; public GameObject bird; public GameObject cloud;

    public bool validMove = true;
    public bool confirmedMove = false;
    public bool gameOver = false;
    public bool canClick = true;

    public int confirmClicked = 5;

    public GameObject confirmScreen;
    public GameObject titleScreen;
    public TextMeshProUGUI lastPickedText;
    public TextMeshProUGUI lastClickedText;
    public TextMeshProUGUI playerTurnText;


    public GameObject playingElements;
    public GameObject playingScene;
    public GameObject restartScreen;

    private PlayerArray playerArray;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        restartScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!restartScreen.activeSelf)
            {
                restartScreen.SetActive(true);
            }
            else if (restartScreen.activeSelf)
            {
                restartScreen.SetActive(false);
            }
            
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);

        gameOver = false;

        playerTurnText.text = "Gold's/Player 1's Turn";

        playingElements.SetActive(true);
        playingScene.SetActive(true);

        playerArray = GameObject.FindObjectOfType<PlayerArray>().GetComponent<PlayerArray>();
        playerArray.activePlayer = playerArray.playerToken;
        generateLists();
        allCombos = generateAllCombos();
        GenerateTiles();
    }

    private void generateLists()
    {
        trees.Add(maple); trees.Add(cherry); trees.Add(pine); trees.Add(iris);
        symbols.Add(sun); symbols.Add(flag); symbols.Add(bird); symbols.Add(cloud);

    }

    private List<(GameObject, GameObject)> generateAllCombos()
    {
        List<(GameObject, GameObject)> allCombos = new List<(GameObject, GameObject)>();
        foreach (GameObject i in trees)
        {
            foreach (GameObject j in symbols)
            {
                
                allCombos.Add( (i, j));

            }
        }
        
        return allCombos;
    }

    public void GenerateTiles()
    {
        for (int i=0; i < 4; i++)
        {
            GenerateRow(i);
        }
    }

    private void GenerateRow(int i)
    {
        for (int j=0; j < 4; j++)
        {
            int randomIndex = Random.Range(0, allCombos.Count);
            //Debug.Log((allCombos[randomIndex].Item1.name, allCombos[randomIndex].Item2.name) + "1");

            Vector3 tilePosition = new Vector3(-4 + 2.5f * j, 0.25f, 4 + -2.5f * i);

            GameObject tree = allCombos[randomIndex].Item1;
            GameObject symbol = allCombos[randomIndex].Item2;
            GameObject newTile =  GenerateTile(tilePosition, allCombos[randomIndex].Item1, allCombos[randomIndex].Item2);

            Instantiate(tree, new Vector3(tilePosition.x - 0.33f, 0.25f, tilePosition.z + 0.33f), tree.transform.rotation );
            Instantiate(symbol, new Vector3(tilePosition.x + 0.25f, 0.25f, tilePosition.z - 0.25f), symbol.transform.rotation);

            newTile.GetComponent<Tile>().xPos = i;
            newTile.GetComponent<Tile>().yPos = j;

            newTile.GetComponent<Tile>().tileTree = tree;
            newTile.GetComponent<Tile>().tileSymbol = symbol;

            playerArray.totalCardSpaces[i, j] = newTile.GetComponent<Tile>();

            //Debug.Log((allCombos[randomIndex].Item1.name, allCombos[randomIndex].Item2.name) + "2" );

            allCombos.Remove(allCombos[randomIndex]);
            
        }   
    }
    public GameObject GenerateTile(Vector3 position, GameObject treeType, GameObject symbolType)
    {

       GameObject newTile = Instantiate(tile, position, tile.transform.rotation);
       return newTile;

    }


    public void CheckIfValid()
    {
        if ((clickedTree == lastPickedTree || clickedSymbol == lastPickedSymbol) && clickedTile.open)
        {
            
            validMove = true;
        }
        else
        {
            
            validMove = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOver = true;
        playerTurnText.text = playerArray.activePlayer.name + " wins :]";
    }

    
}
