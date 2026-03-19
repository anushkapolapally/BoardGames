using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject playerPanel;
    [SerializeField] Button Twoplayerbutton;
    [SerializeField] Button Threeplayerbutton;
    [SerializeField] Button Fourplayerbutton;

    public int players;
    public bool gameStarted;
    public bool gameSetUp = false;

    //rows go from bottom to top
    public int[,] board = new int[8, 8];


    public List<GameObject> value1Gameobjects = new List<GameObject>();
    public List<GameObject> value2Gameobjects = new List<GameObject>();
    public List<GameObject> value3Gameobjects = new List<GameObject>();
    public List<GameObject> boardGameObjects = new List<GameObject>();

    System.Random random = new System.Random();

   

    /*
     * Notes
     * Even index row piece (a, b) connects to odd index row piece at same column or plus one (a+1, b), (a+1, b+1) (a-1, b) (a-1, b+1)
     * Odd index row piece (a, b) connects to even index row piece at same column or minus one (a+1, b) (a+1, b-1) (a-1, b) (a-1, b-1)
     */
    void Start()
    {
        
        Button button = Twoplayerbutton.GetComponent<Button>();
        button.onClick.AddListener(twoButtonPressed);

        Button turn = Threeplayerbutton.GetComponent<Button>();
        turn.onClick.AddListener(threeButtonPressed);

        Button draw = Fourplayerbutton.GetComponent<Button>();
        draw.onClick.AddListener(fourButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (gameSetUp == false)
            {
                playerPanel.SetActive(false);

                intializingBoard();

                assigningPieceValues();
                //printing debug for board
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {

                        Debug.Log("i= " + i + ", j= " + j + " is " + board[i, j]);
                    }
                }
            }


        }
    }
    private void intializingBoard()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (i % 2 == 0)
            {
                //even row
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    board[i,j] = 0;
                }
            }
            else
            {
                //odd row
                board[i, 0] = -1;
                for (int j = 1; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }
        }
    }

    private void assigningPieceValues()
    {
        
        //1 value pieces
        for(int i = 0; i< 30; i++)
        {
            bool placed = false;
            while (placed == false)
            {
                int randomRow = random.Next(0, 8);
                int randomCol;
                if (randomRow % 2 == 0)
                {
                    randomCol = random.Next(0, 8);
                }
                else
                {
                    randomCol = random.Next(1, 8);
                }

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 1;
                    placed = true;
                    Debug.Log("Value one piece placed");
                }

            }  
        }
        //2 value pieces

        for (int i = 0; i < 20; i++)
        {
            bool placed = false;
            while (placed == false)
            {
                int randomRow = random.Next(0, 8);
                int randomCol;
                if (randomRow % 2 == 0)
                {
                    randomCol = random.Next(0, 8);
                }
                else
                {
                    randomCol = random.Next(1, 8);
                }

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 2;
                    placed = true;
                    Debug.Log("Value two piece placed");
                }

            }
        }

        //3 value pieces
        for (int i = 0; i < 10; i++)
        {
            bool placed = false;
            while (placed == false)
            {
                int randomRow = random.Next(0, 8);
                int randomCol;
                if (randomRow % 2 == 0)
                {
                    randomCol = random.Next(0, 8);
                }
                else
                {
                    randomCol = random.Next(1, 8);
                }

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 3;
                    placed = true;
                    Debug.Log("Value two piece placed");
                }

            }
        }
        creatingGameObjectBoard();
        gameSetUp = true;
        Debug.Log("Piece Value Setup done");
    }
    private void creatingGameObjectBoard()
    {

    }
    private void twoButtonPressed()
    {
        Debug.Log("in two pressed");
        players = 2;
        gameStarted = true;
    }
    private void threeButtonPressed()
    {
        players = 3;
        gameStarted = true;
    }
    private void fourButtonPressed()
    {
        players = 4;
        gameStarted = true;
    }
}
