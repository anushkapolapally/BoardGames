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
    public int turn = -1;
    public bool gameStarted;
    public bool gameSetUp = false;

    //rows go from bottom to top

    //-1 = empty space, 1 = 1 value pieces, 2= 2 value pieces, 3= 3 value pieces
    // if a player on that space, +3 to space value. 4 = player on 1 piece value, 5 = player on 2 piece value, 6 = player on 3 piece value
    public int[,] board = new int[8, 8];


    public List<GameObject> value1Gameobjects = new List<GameObject>();
    public List<GameObject> value2Gameobjects = new List<GameObject>();
    public List<GameObject> value3Gameobjects = new List<GameObject>();
    public List<GameObject> boardGameObjects = new List<GameObject>();

    public List<GameObject> playerPiecesGameObjects = new List<GameObject>();

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
            else if(gameSetUp == true)
            {
                Turn();

            }
        }
    }

    private void Turn()
    {
        //choosing inital positions for pieces
        if (turn == -1)
        {/*
            Debug.Log("Inside of placing pieces");
            for (int i = 0; i < players; i++)
            {
                bool placedPiece = false;
                while (placedPiece == false)
                {
                    for (int j = 0; j < 64; j++)
                    {
                        int row = i / 8;
                        int col = i % 8;

                        if (row % 2 == 0)
                        {

                        }
                        else if (row % 2 == 1)
                        {
                            //put exception for empty spaces
                            if (col == 0)
                            {
                                continue;
                            }
                            else
                            {

                            }
                        }
                    }


                }
            }
            //normal turns */
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
                    //assigning coresponding piece in boardGameObjectBoard

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
        placingGamePieces();
        gameSetUp = true;
        Debug.Log("Piece Value Setup done");
    }
    private void creatingGameObjectBoard()
    {


        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++) {
                if (board[i, j] == 1)
                {
                    for(int k=0; k < 30; k++)
                    {
                        if (value1Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard == false)
                        {
                            boardGameObjects[i*8+j] = value1Gameobjects[k];
                            value1Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard = true;
                            break;
                        }
                    }
                }
                else if (board[i, j] == 2)
                {
                    for (int k = 0; k < 20; k++)
                    {
                        if (value2Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard == false)
                        {
                            boardGameObjects[i * 8 + j] = value2Gameobjects[k];
                            value2Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard = true;
                            break;
                        }
                    }
                }
                else if (board[i, j] == 3)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (value3Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard == false)
                        {
                            boardGameObjects[i * 8 + j] = value3Gameobjects[k];
                            value3Gameobjects[k].GetComponent<BoardPiece>().placedOnMainBoard = true;
                            break;
                        }
                    }
                }
            }
        }

    }

    private void placingGamePieces()
    {
        Debug.Log("Inside of placing Game Pieces");
        float firstXpos = -6;
        float firstYpos = -5;
        //x distance between pieces on the same row is sqrt(3)
        // x values shifted -sqrt(3)/2 for odd value rows
        // y values shifted +3/2 between rows

        for(int i = 0; i< 64; i++)
        {
            int row = i / 8;
            int col = i % 8;

            if (row % 2 == 0)
            {
                boardGameObjects[i].transform.position = new Vector3(firstXpos + 2 * col, (float)(firstYpos + 1.72 * row), 0);
            }
            else if(row % 2 == 1)
            {
                //put exception for empty spaces
                if(col == 0)
                {
                    continue;
                }
                else
                {
                    boardGameObjects[i].transform.position = new Vector3(firstXpos - 1 + 2 * col, (float)(firstYpos + 1.72 * row), 0);
                }
            }
        }

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
