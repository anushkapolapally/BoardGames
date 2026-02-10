using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class GameManager : MonoBehaviour
{
    /* 
I'm thinking of building a collection of 3D games so that the user can
select what game they willl be playing. I will first build dominos.

Dominos start:
- scene of table with the pool of dominos in the center
- 28 tiles total in the boneyard (total set of dominos)
    - 
- Each player draws 7 tiles


Objective: Reach a point goal (150)

First turn: highest double starts the game. if no doubel then highest value (sum of pips) tile person starts 
Remaining turns: play to the right

All turns: 
-place a tile with an end that matches the open end of a domino on the board
-doubles can have dominos placed on all four sides

-if players cannot make a valid move, they must draw from the boneyard until they can play or the boneyard is empty
-if players can't play and the boneyard is empty, they must pass their turn and let the player to their right play
-if all players pass, the round is over and no points are awarded

-once someone plays all their tiles, the round is over
-add up the pips on the remaining tiles of the oppponents tile and add it to the winner's score

-keep playing rounds until someone reaches the point goal

domSet = [suite, value]
domSetPos = [xPos in board, yPos in board, isAvailable (1 yes or 0 no)]



*/


    public int[,] boneyard = new int[28, 3];
    public List<int[]> player1 = new List<int[]>();
    public List<GameObject> player1obj = new List<GameObject>();
    public List<int[]> player2 = new List<int[]>();
    public List<GameObject> player2obj = new List<GameObject>();

    public List<int[]> board = new List<int[]>();
    public List<GameObject> boardobj = new List<GameObject>();

    public List<GameObject> gameObjects = new List<GameObject>();

    public List<GameObject> placeholder1 = new List<GameObject>();
    public List<GameObject> placeholder2 = new List<GameObject>();

    public List<GameObject> vertBoard = new List<GameObject>();
    public List<GameObject> horizBoard = new List<GameObject>();

    public List<GameObject> placeholderBoard = new List<GameObject>();

    public int placehold = 0;

    [SerializeField] int turn = 0;

    public Camera targetCamera;

    public bool startPressed = false;

    [SerializeField] Button startButton;
    [SerializeField] Button turnButton;

    public bool domClicked = false;
    public bool turnClicked = false;

    private bool turnPlayed = false;

    [SerializeField] Text options;

    public int pointGoal;
    private int player1scoreNum = 0;
    [SerializeField] Text player1Score;
    private int player2scoreNum = 0;
    [SerializeField] Text player2Score;

    [SerializeField] Text WinText;

    [SerializeField] GameObject Board;

    [SerializeField] Text turnText;

    public List<Button> playerButtons;

    public bool buttonPressed = false;

    public int pressedNum = -1;

    public int boardNum = -1;

    private bool invalidTurn = false;

    [SerializeField] Button drawButton;

    private bool drewNewDomino = false;

    public bool drawNewDominoTest = false;


    void Start()
    {
        WinText.enabled = false;
        options.text = "";
        Debug.Log("Start Game");
        startGame();

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(changeStartPressed);

        Button turn = turnButton.GetComponent<Button>();
        turn.onClick.AddListener(changeturnClicked);

        Button draw = drawButton.GetComponent<Button>();
        draw.onClick.AddListener(drawDomino);

        for (int i = 0; i < playerButtons.Count; i++)
        {
            playerButtons[i].GetComponent<Button>().enabled = false;
           
        }

        drawButton.transform.position = new Vector3(-100, -100, 0);
        turnButton.transform.position = new Vector3(-100, -100, 0);



    }
    private void drawDomino()
    {
        Debug.Log("Inside draw function");
    }
    void changeStartPressed()
    {
        startPressed = true;
        startButton.transform.position = new Vector3(-100, -100, 0);
    }

    void changeturnClicked()
    {
        turnClicked = true;
        
        
        Debug.Log("change turn button");
    }
    // Update is called once per frame
    void Update()
    {
        cameraPosition();

       

        if (startPressed)
        {
            drawingInitalDominos();
            startButton.enabled = false;
        }

        changeTurn();
        Turn();
        //selectingDomino();
        
        player1Score.text = "PLayer 1 score: " + player1scoreNum.ToString();
        player2Score.text = "Player 2 score:" + player2scoreNum.ToString();

        if(player2scoreNum > pointGoal || player1scoreNum > pointGoal)
        {

            WinText.enabled = true;
            if (player1scoreNum >= pointGoal)
            {
                WinText.text = "Player 1 won";
            }
            else if (player2scoreNum >= pointGoal)
            {
                WinText.text = "Player 2 won";
            }
        }

        if (turn == 0 || turn == 2) {
            turnText.text = "Turn: Player 1";
            for(int i=0; i< player1obj.Count; i++)
            {
                playerButtons[i].enabled = true;
            }
        }
        else if(turn == 1 || turn == 3)
        {
            turnText.text = "Turn: Player 2";
            for (int i = 0; i < player1obj.Count; i++)
            {
                playerButtons[i].enabled = true;
            }
        }

        numButtonPressed();

        if (drawNewDominoTest == true)
        {
            drawNewDomino();
            drawNewDominoTest = false;
        }
        

    }



    private void startGame()
    {

        int index = 0;
            for(int j=6; j>-1; j--)
            {
                for(int k=0; k<7-j; k++)
                {
                    boneyard[index, 0] = j;
                    boneyard[index, 1] = 6-k;
                    boneyard[index, 2] = 1;
                    //Debug.Log(boneyard[index, 0] + " " + boneyard[index, 1] + " "+ boneyard[index, 2]);
                    index++;
                }
                
            }

        
        
    }

    private void drawingInitalDominos()
    {
        int count = 0;

        while (count < 7)
        {

            int randIndex = Random.Range(0, 28);
            if (boneyard[randIndex,2] == 1)
            {
                int[] domino = new int[2];
                domino[0] = boneyard[randIndex, 0];
                domino[1] = boneyard[randIndex, 1];

                player1.Add(domino);
                player1obj.Add(gameObjects[randIndex]);
                boneyard[randIndex, 2] = 0;

                Debug.Log(boneyard[randIndex, 0] + " " + boneyard[randIndex, 1] );

                gameObjects[randIndex].transform.position = placeholder1[count].transform.position;
                gameObjects[randIndex].transform.rotation = new Quaternion(0, 180, 90,0);

                count++;



            }
        }


        int count2 = 0;

        while (count2 < 7)
        {

            int randIndex = Random.Range(0, 28);
            if (boneyard[randIndex, 2] == 1)
            {
                int[] domino = new int[2];
                domino[0] = boneyard[randIndex, 0];
                domino[1] = boneyard[randIndex, 1];

                player2.Add(domino);
                player2obj.Add(gameObjects[randIndex]);
                boneyard[randIndex, 2] = 0;

                gameObjects[randIndex].transform.position = placeholder2[count2].transform.position;
                gameObjects[randIndex].transform.rotation = new Quaternion(0, 180, 90, 0);
                count2++;

            }
        }

        startPressed = false;
       /* for (int i = 0; i < player1.Count; i++)
        {
            Debug.Log(player1[i][0] + " " + player1[i][1]);
        }*/
    }

    private void cameraPosition()
    {
        if (turn == 0)
        {
            Vector3 originalRotation = new Vector3(42, 0, 0);
            targetCamera.transform.position = new Vector3(0, 28, -43);
            targetCamera.transform.eulerAngles = originalRotation;
        }
        else if (turn == 1)
        {
            Vector3 originalRotation = new Vector3(120, 0, 180);
            targetCamera.transform.position = new Vector3(0, 28, 27);
            targetCamera.transform.eulerAngles = originalRotation;
        }
        else if (turn == 2)
        {
            targetCamera.transform.position = new Vector3(0, 55, -4);
            Vector3 originalRotation = new Vector3(90, 0, 0);
            targetCamera.transform.eulerAngles = originalRotation;
        }
        else if(turn == 3)
        {
            targetCamera.transform.position = new Vector3(0, 55, -4);
            Vector3 originalRotation = new Vector3(90, 180, 0);
            targetCamera.transform.eulerAngles = originalRotation;
        }
    }

    private void changeTurn()
    {
       if(domClicked == true)
        {
            if(turn == 0)
            {
                turn = 2;
                domClicked = false;
                drewNewDomino = false;
                turnClicked = false;
                //selectingDomino();
            }
            else if(turn == 1)
            {
                turn = 3;
                domClicked = false;
                drewNewDomino = false;
                turnClicked = false;
                //selectingDomino();
            }
        }

       if(turnClicked == true)
        {
            if(turn == 2)
            {
                turn = 1;
                domClicked = false;
                drewNewDomino = false;
                turnClicked = false;
            }
            else if(turn == 3)
            {
                turn = 0;
                domClicked = false;
                drewNewDomino = false;
                turnClicked = false;
            }
        }
    }

    private void numButtonPressed()
    {
        Button button0 = playerButtons[0].GetComponent<Button>();
        button0.onClick.AddListener(setPressed0);

        Button button1 = playerButtons[1].GetComponent<Button>();
        button1.onClick.AddListener(setPressed1);

        Button button2 = playerButtons[2].GetComponent<Button>();
        button2.onClick.AddListener(setPressed2);

        Button button3 = playerButtons[3].GetComponent<Button>();
        button3.onClick.AddListener(setPressed3);

        Button button4 = playerButtons[4].GetComponent<Button>();
        button4.onClick.AddListener(setPressed4);

        Button button5 = playerButtons[5].GetComponent<Button>();
        button5.onClick.AddListener(setPressed5);

        Button button6 = playerButtons[6].GetComponent<Button>();
        button6.onClick.AddListener(setPressed6);

        Button button7 = playerButtons[7].GetComponent<Button>();
        button7.onClick.AddListener(setPressed7);

        Button button8 = playerButtons[8].GetComponent<Button>();
        button8.onClick.AddListener(setPressed8);

        Button button9 = playerButtons[9].GetComponent<Button>();
        button9.onClick.AddListener(setPressed9);

        Button button10 = playerButtons[10].GetComponent<Button>();
        button10.onClick.AddListener(setPressed10);

        //Debug.Log(pressedNum);
    }
    private void setPressed0()
    {
        if (buttonPressed == false)
        {
            pressedNum = 0;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 0;
        }
    }
    private void setPressed1()
    {
        if (buttonPressed == false)
        {
            pressedNum = 1;
            buttonPressed = true;
        }

        if(buttonPressed == true && boardNum == -1)
        {
            boardNum = 1;
        }
    }
    private void setPressed2()
    {
        if (buttonPressed == false)
        {
            pressedNum = 2;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 2;
        }
    }
    private void setPressed3()
    {
        if (buttonPressed == false)
        {
            pressedNum = 3;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 3;
        }
    }
    private void setPressed4()
    {
        if (buttonPressed == false)
        {
            pressedNum = 4;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 4;
        }
    }
    private void setPressed5()
    {
        if (buttonPressed == false)
        {
            pressedNum = 5;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 5;
        }
    }
    private void setPressed6()
    {
        if (buttonPressed == false)
        {
            pressedNum = 6;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 6;
        }
    }
    private void setPressed7()
    {
        if (buttonPressed == false)
        {
            pressedNum = 7;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 7;
        }
    }
    private void setPressed8()
    {
        if (buttonPressed == false)
        {
            pressedNum = 8;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 8;
        }
    }
    private void setPressed9()
    {
        if (buttonPressed == false)
        {
            pressedNum = 9;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 9;
        }
    }
    private void setPressed10()
    {
        if (buttonPressed == false)
        {
            pressedNum = 10;
            buttonPressed = true;
        }

        if (buttonPressed == true && boardNum == -1)
        {
            boardNum = 10;
        }
    }




    private void selectingDomino()
    {
           if(turn == 0)
        {
            int range = player1.Count;
        }
        else if (turn == 1) {
            int range = player2.Count;
        }


        if (Input.GetKey(KeyCode.Alpha0) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[0].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[0].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if(turn ==3)
            {
                player2obj[0].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[0].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }



        }
        else if (Input.GetKey(KeyCode.Alpha1) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[1].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[1].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[1].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[1].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }
            



        }
        else if (Input.GetKey(KeyCode.Alpha2) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[2].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[2].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[2].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[2].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[3].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[3].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[3].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[3].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }

        }
        else if (Input.GetKey(KeyCode.Alpha4) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[4].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[4].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[4].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[4].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }

        }
        else if (Input.GetKey(KeyCode.Alpha5) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[5].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[5].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[5].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[5].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }

        }
        else if (Input.GetKey(KeyCode.Alpha6) && (turn == 2 || turn == 3) && turnPlayed == false)
        {
            if (turn == 2)
            {
                //board.Add(player1[0]);
                //player1.Remove(player1[0]);
                player1obj[6].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player1obj[6].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;

            }
            else if (turn == 3)
            {
                player2obj[6].transform.position = new Vector3(-3, 0, -11);
                //fix the rotation
                player2obj[6].transform.rotation = new Quaternion(0, 180, 90, 0);

                turnPlayed = true;
            }

        }

        if (turn == 2)
            {
                Debug.Log("check1");
                if (turnClicked == true)
                {
                    Debug.Log("check2");
                    turn = 1;
                    turnClicked = false;
                    turnPlayed= false;
                }
                
            }
        else if (turn == 3)
        {
            if (turnClicked == true)
            {
                turn = 0;
                turnClicked = false;
                turnPlayed= false;
            }

        }
        

       




    }

   

    private void checkValid()
    {
        Debug.Log("checking if a valid move");
    }

    public void setIsPressed()
    {
        domClicked = true;
        Debug.Log("changed through setter");
    }

    public int getTurn()
    {
        return turn;
    }


    private void Turn()
    {

        if (turn == 0)
        {
            int range = player1.Count;
        }
        else if (turn == 1)
        {
            int range = player2.Count;
        }


        if (turn == 2 && turnPlayed == false && pressedNum != -1)
        {
            int pressed = pressedNum;

            if (board.Count == 0)
            {
                int[] firstMoveAdj = { -1, -1 };
                Board.GetComponent<Board>().placeOnBoard(player1obj[pressed], player1[pressed], firstMoveAdj);
                placehold++;

                player2scoreNum += player1[pressed][0] + player1[pressed][1];

                board.Add(player1[pressed]);
                boardobj.Add(player1obj[pressed]);
                player1.RemoveAt(pressed);
                player1obj.RemoveAt(pressed);

                RearrangeHand(0);

                turn = 1;
                turnPlayed = false;
                buttonPressed = false;
                pressedNum = -1;


            }
            else
            {
                //FIX PLAYER 1
                bool hasValidMove = false;
                List<int> validButtons = new List<int>();

                for (int i = 0; i < board.Count; i++)
                {
                    for (int j = 0; j < player1.Count; j++)
                    {
                        playerButtons[j].enabled = false;
                        if (board[i][0] == player1[j][0] || board[i][1] == player1[j][0] || board[i][0] == player1[j][1] || board[i][1] == player1[j][1])
                        {
                            hasValidMove = true;
                            validButtons.Add(j);
                            playerButtons[j].enabled = true;

                        }
                    }
                }




                //activating buttons


                //if there is a valid move, give options to choose which valid move they want
                if (hasValidMove == true)
                {
                    Debug.Log("ValidButtons contains pressed: " + validButtons.Contains(pressed));
                    if (validButtons.Contains(pressed))
                    {
                        //bool valid = false;

                        List<int> validBoardPos = new List<int>();
                        Debug.Log("Player 1 pressed: " + player1[pressed][0] + ", " + player1[pressed][1]);
                        //testing which positions are on the board for the VALID domino
                        for (int i = 0; i < board.Count; i++)
                        {
                            Debug.Log("testing in the for loop");
                            if (board[i][0] == player1[pressed][0] || board[i][1] == player1[pressed][0] || board[i][0] == player1[pressed][1] || board[i][1] == player1[pressed][1])
                            {
                                //valid = true;
                                validBoardPos.Add(i);
                            }
                        }
                        //Debug.Log("valid: " + valid);



                        options.text = "Choose which valid domino you would like to place your move next to: ";

                        Debug.Log("boardNum: " + boardNum);



                        //activate a text that shows them options and asks them to type the option they want
                        for (int i = 0; i < validBoardPos.Count; i++)
                        {
                            options.text += " " + validBoardPos[i] + ", ";
                        }

                        if (validBoardPos.Contains(boardNum))
                        {
                            //Debug.Log("Move domino next to (" + board[validBoardPos[boardNum]][0] + ", " + board[validBoardPos[boardNum]][1] + ")");
                            // moveDominos(validBoardPos[boardNum], pressed);
                            // Debug.Log("placeholder: " + placehold);

                            player1obj[pressed].transform.position = placeholderBoard[placehold].transform.position;
                            Quaternion placeholdrotation = new Quaternion(placeholderBoard[placehold].transform.rotation.x, placeholderBoard[placehold].transform.rotation.y, placeholderBoard[placehold].transform.rotation.z, placeholderBoard[placehold].transform.rotation.w);
                            player1obj[pressed].transform.rotation = placeholdrotation;
                            placeholderBoard[placehold].SetActive(false);
                            placehold++;

                            SetOutline(placeholderBoard[placehold - 1], false);
                            turnPlayed = false;
                            //NEW
                            turn = 1;
                            boardNum = -1;
                        }
                        else
                        {
                            boardNum = -1;
                        }


                    }
                    else
                    {
                        Debug.Log("choose another");
                        buttonPressed = false;
                        pressed = -1;
                    }


                }
                else
                {
                    drawButton.transform.position = new Vector3(-665, -214, 0);
                    Debug.Log("Invalid turn");
                    if (drewNewDomino == false)
                    {
                        drawNewDomino();
                    }
                    else
                    {
                        turnButton.transform.position = new Vector3(-708, -31, 0);
                        //PUT TURN BUTTON POSITION IN FRAME
                    }

                }


                //if there is a valid move then place it adjacent to that domino in the right orientation by labelling each domino either horizontal or vertical and using the dimensions

            }
        }
        else if (turn == 3 && turnPlayed == false && pressedNum != -1)
        {
            //Debug.Log("Button pressed for player 2: " + pressedNum);

            //figure out which one was pressed
            int pressed = pressedNum;


            //Go through the list of dominos array to find if there is a valid move
            if (board.Count == 0)
            {
                //valid
                //place on board in code
                player2obj[pressed].transform.position = vertBoard[0].transform.position;
                Debug.Log("Place on board");
                board.Add(player2[pressed]);
                boardobj.Add(player2obj[pressed]);
                player2.RemoveAt(pressed);
                player2obj.RemoveAt(pressed);


                //rearrange the players hand to have no gaps
                RearrangeHand(1);

                turn = 1;
                turnPlayed = false;

            }
            else
            {
                /*bool valid = false;

                List<int> validBoardPos = new List<int>();
                Debug.Log(player2[pressed][0] + " " + player2[pressed][1]);
                for (int i = 0; i < board.Count; i++)
                {
                Debug.Log("testing in the for loop");
                    if (board[i][0] == player2[pressed][0] || board[i][1] == player2[pressed][0] || board[i][0] == player2[pressed][1] || board[i][1] == player2[pressed][1])
                    {
                        valid = true;
                        validBoardPos.Add(i);
                    }
                }
                Debug.Log("valid: " + valid);*/

                //checking which buttons should be valid
                bool hasValidMove = false;
                List<int> validButtons = new List<int>();

                for (int i = 0; i < board.Count; i++)
                {
                    for (int j = 0; j < player2.Count; j++)
                    {
                        playerButtons[j].enabled = false;
                        if (board[i][0] == player2[j][0] || board[i][1] == player2[j][0] || board[i][0] == player2[j][1] || board[i][1] == player2[j][1])
                        {
                            hasValidMove = true;
                            validButtons.Add(j);
                            playerButtons[j].enabled = true;

                        }
                    }
                }




                //activating buttons


                //if there is a valid move, give options to choose which valid move they want
                if (hasValidMove == true)
                {
                    Debug.Log("ValidButtons contains pressed: " + validButtons.Contains(pressed));
                    if (validButtons.Contains(pressed))
                    {
                        //bool valid = false;

                        List<int> validBoardPos = new List<int>();
                        Debug.Log("Player 2 pressed: " + player2[pressed][0] + ", " + player2[pressed][1]);
                        //testing which positions are on the board for the VALID domino
                        for (int i = 0; i < board.Count; i++)
                        {
                            Debug.Log("testing in the for loop");
                            if (board[i][0] == player2[pressed][0] || board[i][1] == player2[pressed][0] || board[i][0] == player2[pressed][1] || board[i][1] == player2[pressed][1])
                            {
                                //valid = true;
                                validBoardPos.Add(i);
                            }
                        }
                        //Debug.Log("valid: " + valid);



                        options.text = "Choose which valid domino you would like to place your move next to: ";

                        Debug.Log("boardNum: " + boardNum);



                        //activate a text that shows them options and asks them to type the option they want
                        for (int i = 0; i < validBoardPos.Count; i++)
                        {
                            options.text += " " + validBoardPos[i] + ", ";
                        }

                        if (validBoardPos.Contains(boardNum))
                        {
                            Debug.Log("Move domino next to (" + board[validBoardPos[boardNum]][0] + ", " + board[validBoardPos[boardNum]][1] + ")");
                            board.Add(player2[pressed]);
                            moveDominos(validBoardPos[boardNum], pressed);
                            Debug.Log("placeholder: " + placehold);
                            player2obj[pressed].transform.position = placeholderBoard[placehold].transform.position;
                            Quaternion placeholdrotation = new Quaternion(placeholderBoard[placehold].transform.rotation.x, placeholderBoard[placehold].transform.rotation.y, placeholderBoard[placehold].transform.rotation.z, placeholderBoard[placehold].transform.rotation.w);
                            player2obj[pressed].transform.rotation = placeholdrotation;
                            placeholderBoard[placehold].SetActive(false);
                            placehold++;
                            SetOutline(placeholderBoard[placehold - 1], false);
                            turnPlayed = false;
                            player2.RemoveAt(pressed);

                            //NEW
                            turn = 0;
                            boardNum = -1;
                        }
                        else
                        {
                            boardNum = -1;
                        }


                    }
                    else
                    {
                        Debug.Log("choose another");
                        buttonPressed = false;
                        pressed = -1;
                    }


                }
                else
                {
                    drawButton.transform.position = new Vector3(-665, -214, 0);
                    Debug.Log("Invalid turn");
                    if (drewNewDomino == false)
                    {
                        drawNewDomino();
                    }
                    else
                    {
                        //PUT TURN BUTTON POSITION IN FRAME                        
                        turnButton.transform.position = new Vector3(-708, -31, 0);
                    }



                }



                //if there is a valid move then place it adjacent to that domino in the right orientation by labelling each domino either horizontal or vertical and using the dimensions

            }


        }
    }

    private void drawNewDomino()
    {
        Debug.Log("Inside drawing new domino method");
        bool drawn = false;
        int[] domino = new int[2];
        Debug.Log("Draw new domino method");
        int randIndex = -1;
        while (drawn == false)
        {
            randIndex = Random.Range(0, 28);
            if (boneyard[randIndex, 2] == 1)
            {
                drawn = true;
              
                domino[0] = boneyard[randIndex, 0];
                domino[1] = boneyard[randIndex, 1];
                drewNewDomino = true;
            }
        }



        
            if (turn == 2)
            {
            
                    player1.Add(domino);
                    player1obj.Add(gameObjects[randIndex]);
                    boneyard[randIndex, 2] = 0;

                    Debug.Log("Drawing player 1");

                    gameObjects[randIndex].transform.position = placeholder1[player1.Count].transform.position;
                    gameObjects[randIndex].transform.rotation = new Quaternion(0, 180, 90, 0);
                    


                }
                else if (turn == 3)
                {
                    int randIndex2 = Random.Range(0, 28);
               
                        player2.Add(domino);
                        player2obj.Add(gameObjects[randIndex2]);
                        boneyard[randIndex2, 2] = 0;

                        Debug.Log("drawing player 2");

                        gameObjects[randIndex2].transform.position = placeholder2[player2.Count].transform.position;
                        gameObjects[randIndex2].transform.rotation = new Quaternion(0, 180, 90, 0);
                       
                    }
                

            }
        
    
    

    private void moveDominos(int boardPos, int pressed)
    {
        Debug.Log("inside move domino function");
        //REMINDER: use raycast from the last position in position grid to find which other positions are open and highlight the border of that domino.
        player2obj[pressed].transform.position = boardobj[boardPos].transform.position;
        //create grid for the dominos to go based on if the one being placed in next to vertical or horizontal
        player2obj[pressed].transform.rotation = new Quaternion(0, 180, 90, 0);
    }

    private void RearrangeHand(int player)
    {
        if (player == 0)
        {
            for (int i = 0; i < player1obj.Count; i++)
            {
                player1obj[i].transform.position = placeholder1[i].transform.position;
            }
        }
        else if (player == 1)
        {
            for (int i = 0; i < player2obj.Count; i++)
            {
                player2obj[i].transform.position = placeholder2[i].transform.position;
            }
        }
    }

    public void SetOutline(GameObject placeholder, bool on)
    {
        MeshRenderer mesh = placeholder.GetComponent<MeshRenderer>();
        mesh.enabled = on;
        Transform outline = placeholder.transform.Find("Placeholder_Outline");
        if (outline != null)
            outline.gameObject.SetActive(on);
    }


}
