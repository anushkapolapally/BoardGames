using System;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BattleGamemanager : MonoBehaviour
{
    public int turn = 0;
    private bool startedGame = false;

    public List<GameObject> player1ships = new List<GameObject>();
    public List<GameObject> player2ships = new List<GameObject>();

    [SerializeField] Text initalInstructions;

    private string typedText = "";
    public int placedNum = 0;

    [SerializeField] Button upButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button downButton;
    [SerializeField] Button leftButton;

    public List<float> player1rowpositions = new List<float>();
    public List<float> player1columnpositions = new List<float>();
    public int[,] player1board = new int[10, 10];

    public List<float> player2rowpositions = new List<float>();
    public List<float> player2columnpositions = new List<float>();
    public int[,] player2board = new int[10, 10];

    public Camera targetCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initalInstructions.text = "";

        //Button up = upButton.GetComponent<Button>();
        //up.onClick.AddListener(changeUp());
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                player1board[i, j] = -1;
                player2board[i, j] = -1;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (startedGame == false)
        {
            startGame();
        }
        cameraPosition(turn);

        if(startedGame == true)
        {
            initalInstructions.text = "";
            Turn();
        }
    }


    private void startGame()
    {
        if (turn == 0)
        {
            if (placedNum == 9)
            {
                turn = 1;
                placedNum = 0;
            }
            initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on. Typed: ";

            for (int i = 0; i < player1ships.Count; i++)
            {
                if (player1ships[i].GetComponent<Ship>().getClicked() == true)
                {
                    Debug.Log("Debug the index: " + i);
                    placingPiecesText(player1ships[i]);
                    //Debug.Log(typedText);

                }
            }


            //player 1 places pieces

        }
        else if (turn == 1)
        {
            //player 2 places pieces
            if (placedNum == 9)
            {
                turn = 0;
                startedGame = true;
                initalInstructions.text = "";

            }

            initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on. Typed: ";

            for (int i = 0; i < player2ships.Count; i++)
            {
                if (player2ships[i].GetComponent<Ship>().getClicked() == true)
                {

                    placingPiecesText(player2ships[i]);
                    //Debug.Log(typedText);

                }

            }
        }
    }

    private void Turn()
    {
        Debug.Log("Game started. Turn = "+ turn);

        if(turn == 0)
        {
            
            Debug.Log("Enter placesPiecesText");
            if (typedText.Length < 2)
            {

                foreach (char c in Input.inputString)
                {
                    if (c == '\b') // Backspace
                    {
                        if (typedText.Length > 0)
                            typedText = typedText.Substring(0, typedText.Length - 1);
                    }
                    else if (c == '\n' || c == '\r') // Enter
                    {
                        Debug.Log("Final Input: " + typedText);
                        typedText = ""; // Clear after enter (optional)
                    }
                    else
                    {
                        typedText += c;
                    }
                }
                initalInstructions.text = "Type the coordinate of the position you would like to hit on the opponents board: " + typedText;
            }
            else
            {
                initalInstructions.text = typedText + " is the spot you have choose to hit";
                int row = -1;
                int col = -1;
                translateText(typedText, row, col);
                Debug.Log("Row: " + row);
                Debug.Log("Col: " + col);
                if (player1board[col, row] == 0)
                {
                    initalInstructions.text = typedText + " is the spot you have choose to hit. It has a ship";
                }
                else if (player1board[col, row] == -1)
                {
                    initalInstructions.text = typedText + " is the spot you have choose to hit. It does not has a ship";
                }
            }
            
        }
    }

    private void translateText(string text, int row, int column)
    {
        
        //getting letter
        if (typedText[0] == 'A' || typedText[0] == 'a')
        {
            row = 0;
        }
        else if (typedText[0] == 'B' || typedText[0] == 'b')
        {
            row = 1;
        }
        else if (typedText[0] == 'C' || typedText[0] == 'c')
        {
            row = 2;
        }
        else if (typedText[0] == 'D' || typedText[0] == 'd')
        {
            row = 3;
        }
        else if (typedText[0] == 'E' || typedText[0] == 'e')
        {
            row = 4;
        }
        else if (typedText[0] == 'F' || typedText[0] == 'f')
        {
            row = 5;
        }
        else if (typedText[0] == 'G' || typedText[0] == 'g')
        {
            row = 6;
        }
        else if (typedText[0] == 'H' || typedText[0] == 'h')
        {
            row = 7;
        }
        else if (typedText[0] == 'I' || typedText[0] == 'i')
        {
            row = 8;
        }
        else if (typedText[0] == 'J' || typedText[0] == 'j')
        {
            row = 9;
        }
        //getting column
        if (typedText[1] == '1')
        {
            column = 0;
        }
        else if (typedText[1] == '2')
        {
            column = 1;
        }
        else if (typedText[1] == '3')
        {
            column = 2;
        }
        else if (typedText[1] == '4')
        {
            column = 3;
        }
        else if (typedText[1] == '5')
        {
            column = 4;
        }
        else if (typedText[1] == '6')
        {
            column = 5;
        }
        else if (typedText[1] == '7')
        {
            column = 6;
        }
        else if (typedText[1] == '8')
        {
            column = 7;
        }
        else if (typedText[1] == '9')
        {
            column = 8;
        }
        else if (typedText[1] == 'T')
        {
            column = 9;

        }
    }

    private void placingPiecesText(GameObject ship)
    {
        Debug.Log("Enter placesPiecesText");
        if (typedText.Length < 3)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Backspace
                {
                    if (typedText.Length > 0)
                        typedText = typedText.Substring(0, typedText.Length - 1);
                }
                else if (c == '\n' || c == '\r') // Enter
                {
                    Debug.Log("Final Input: " + typedText);
                    typedText = ""; // Clear after enter (optional)
                }
                else
                {
                    typedText += c;
                }
            }
        }
        else
        {
            placingPiecesPosition(typedText, ship);
            ship.GetComponent<Ship>().setClicked(false);
            placedNum++;
            typedText = "";
        }

        // Shows live typing in Console
        //Debug.Log("Currently typing: " + typedText);
        initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on and whether u want to rotate the ship. Typed: " + typedText;

    }

    private void placingPiecesPosition(string typedText, GameObject ship)
    {
        Debug.Log("Enter placingpiecesposition");
        int row = -1;
        int column = -1;
        bool rotate = false;
        //getting letter
        if (typedText[0]=='A' || typedText[0] == 'a')
        {
            row = 0;
        }
        else if (typedText[0] == 'B' || typedText[0] == 'b')
        {
            row = 1;
        }
        else if (typedText[0] == 'C' || typedText[0] == 'c')
        {
            row = 2;
        }
        else if (typedText[0] == 'D' || typedText[0] == 'd')
        {
            row = 3;
        }
        else if (typedText[0] == 'E' || typedText[0] == 'e')
        {
            row = 4;
        }
        else if (typedText[0] == 'F' || typedText[0] == 'f')
        {
            row = 5;
        }
        else if (typedText[0] == 'G' || typedText[0] == 'g')
        {
            row = 6;
        }
        else if (typedText[0] == 'H' || typedText[0] == 'h')
        {
            row = 7;
        }
        else if (typedText[0] == 'I' || typedText[0] == 'i')
        {
            row = 8;
        }
        else if (typedText[0] == 'J' || typedText[0] == 'j')
        {
            row = 9;
        }
        //getting column
        if (typedText[1] == '1')
        {
            column = 0;
        }
        else if (typedText[1] == '2')
        {
            column = 1;
        }
        else if (typedText[1] == '3')
        {
            column = 2;
        }
        else if (typedText[1] == '4')
        {
            column = 3;
        }
        else if (typedText[1] == '5')
        {
            column = 4;
        }
        else if (typedText[1] == '6')
        {
            column = 5;
        }
        else if (typedText[1] == '7')
        {
            column = 6;
        }
        else if (typedText[1] == '8')
        {
            column = 7;
        }
        else if (typedText[1] == '9')
        {
            column = 8;
        }
        else if (typedText[1] == 'T')
        {
            column = 9;

        }
        // getting rotation
        if (typedText[2] == 'r')
        {
            rotate = true;
        }
        else if (typedText[2] == 'n')
        {
            rotate = false;
        }

        int shiplength = ship.GetComponent<Ship>().length;

        if (shiplength == 2)
        {
            if (rotate == true)
            {
                ship.transform.rotation = Quaternion.Euler(-90, 0, 450);
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column, row-1] = 0;


                    ship.transform.position = new Vector3((player1rowpositions[row] + player1rowpositions[row - 1]) / 2, 6.5f, player1columnpositions[column]);
                }
                else if (turn == 1)
                {
                    player2board[column, row] = 0;
                    player2board[column, row - 1] = 0;


                    Debug.Log("Printing player 2 board");

                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Debug.Log(player2board[i, j]);
                        }
                    }

                    ship.transform.position = new Vector3((player2rowpositions[row] + player2rowpositions[row - 1]) / 2, 6.5f, player2columnpositions[column]);
                }
            }
            else if (rotate == false)
            {
                ship.transform.rotation = Quaternion.Euler(-90, 0, 180);
                //fix the rotation here
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column - 1, row] = 0;
                    /*Debug.Log("Printing player 1 board");

                   

                    for (int i = 0; i< 10; i++)
                    {
                        for(int j = 0; j < 10; j++)
                        {
                            Debug.Log(player1board[i, j]);
                        }
                    }*/
                    ship.transform.position = new Vector3(player1rowpositions[row], 6.5f, (player1columnpositions[column] + player1columnpositions[column - 1]) / 2);
                }
                else if(turn == 1)
                {
                    


                    player2board[column, row] = 0;
                    player2board[column - 1, row] = 0;


                    Debug.Log("Testing if it reaches here");
                    ship.transform.position = new Vector3(player2rowpositions[row], 6.5f, (player2columnpositions[column] + player2columnpositions[column - 1]) / 2);
                }
            }
        }
        else if (shiplength == 3)
        {
            if (rotate == true)
            {
                ship.transform.rotation = Quaternion.Euler(-90, 0,270);
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column, row - 1] = 0;
                    player1board[column, row - 2] = 0;

                    ship.transform.position = new Vector3(player1rowpositions[row - 1], 6.5f, player1columnpositions[column]);
                }
                else if (turn == 1)
                {
                    player2board[column, row] = 0;
                    player2board[column, row - 1] = 0;
                    player2board[column, row - 2] = 0;

                    ship.transform.position = new Vector3(player2rowpositions[row - 1], 6.5f, player2columnpositions[column]);
                }
            }
            else if (rotate == false)
            {
                
                ship.transform.rotation = Quaternion.Euler(-90, 0, 180);
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column - 1, row] = 0;
                    player1board[column - 2, row] = 0;

                    Debug.Log("entered correct if");
                    ship.transform.position = new Vector3(player1rowpositions[row], 6.5f, player1columnpositions[column - 1]);
                }
                else if (turn == 1)
                {
                    player2board[column, row] = 0;
                    player2board[column - 1, row] = 0;
                    player2board[column - 2, row] = 0;

                    ship.transform.position = new Vector3(player2rowpositions[row], 6.5f, player2columnpositions[column - 1]);
                }
            }
        }
        else if (shiplength == 4)
        {
            if (rotate == true)
            {
                ship.transform.rotation = Quaternion.Euler(-90, 0, 270);
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column, row - 1] = 0;
                    player1board[column, row - 2] = 0;
                    player1board[column, row - 3] = 0;
                    ship.transform.position = new Vector3((player1rowpositions[row - 1] + player1rowpositions[row - 2]) / 2, 6.5f, player1columnpositions[column]);
                }
                else if(turn == 1)
                {
                    player2board[column, row] = 0;
                    player2board[column, row - 1] = 0;
                    player2board[column, row - 2] = 0;
                    player2board[column, row - 3] = 0;
                    ship.transform.position = new Vector3((player2rowpositions[row - 1] + player2rowpositions[row - 2]) / 2, 6.5f, player2columnpositions[column]);
                }
            }
            else if (rotate == false)
            {
                ship.transform.rotation = Quaternion.Euler(-90, 0, 180);
                if (turn == 0)
                {
                    player1board[column, row] = 0;
                    player1board[column - 1, row] = 0;
                    player1board[column - 2, row] = 0;
                    player1board[column - 3, row] = 0;
                    ship.transform.position = new Vector3(player1rowpositions[row], 6.5f, (player1columnpositions[column - 1] + player1columnpositions[column - 2]) / 2);
                }
                else if(turn == 1)
                {
                    player2board[column, row] = 0;
                    player2board[column - 1, row] = 0;
                    player2board[column - 2, row] = 0;
                    player2board[column - 3, row] = 0;
                    ship.transform.position = new Vector3(player2rowpositions[row], 6.5f, (player2columnpositions[column - 1] + player2columnpositions[column - 2]) / 2);
                }
            }
        }

        Debug.Log("row: " + row + " column: " + column);
    }


    private void cameraPosition(int turn)
    {
        if(turn == 0)
        {
            Vector3 originalRotation = new Vector3(37, 0, 0);
            targetCamera.transform.position = new Vector3(3, 19, -13);
            targetCamera.transform.eulerAngles = originalRotation;
        }
        else if(turn == 1)
        {
            Vector3 originalRotation = new Vector3(37, -180, 0);
            targetCamera.transform.position = new Vector3(3, 22, 25);
            targetCamera.transform.eulerAngles = originalRotation;
        }
    }
}

   




