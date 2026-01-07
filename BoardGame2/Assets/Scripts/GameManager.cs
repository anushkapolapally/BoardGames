using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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

    [SerializeField] int turn = 0;

    public Camera targetCamera;

    public bool startPressed = false;

    [SerializeField] Button startButton;
    [SerializeField] Button turnButton;

    public bool domClicked = false;
    public bool turnClicked = false;

    private bool turnPlayed = false;

    

    void Start()
    {

        Debug.Log("Start Game");
        startGame();

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(changeStartPressed);

        Button turn = turnButton.GetComponent<Button>();
        turn.onClick.AddListener(changeturnClicked);
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
        
    }

    private void startGame()
    {
        //initalize the boneyard
        //7+6+5+4+3+2+1

        int index = 0;
            for(int j=0; j< 7; j++)
            {
                for(int k=0; k<7-j; k++)
                {
                    boneyard[index, 0] = j;
                    boneyard[index, 1] = k;
                    boneyard[index, 2] = 1; 
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

                Debug.Log(randIndex);

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
                //selectingDomino();
            }
            else if(turn == 1)
            {
                turn = 3;
                domClicked = false;
                //selectingDomino();
            }
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
        //preview turns
        if (turn == 0)
        {
            int range = player1.Count;
        }
        else if (turn == 1)
        {
            int range = player2.Count;
        }
        //actual turn

        //player 0

        if(turn == 2 && turnPlayed==false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9)){
                //figure out which one was pressed
                int pressed;
                if (Input.GetKeyDown(KeyCode.Alpha1)){ pressed = 1;}
                else if (Input.GetKeyDown(KeyCode.Alpha2)){ pressed = 2;}
                else if (Input.GetKeyDown(KeyCode.Alpha3)){ pressed = 3;}
                else if (Input.GetKeyDown(KeyCode.Alpha4)){ pressed = 4;}
                else if (Input.GetKeyDown(KeyCode.Alpha5)){ pressed = 5;}
                else if (Input.GetKeyDown(KeyCode.Alpha6)){ pressed = 6;}
                else if (Input.GetKeyDown(KeyCode.Alpha7)){ pressed = 7;}
                else if (Input.GetKeyDown(KeyCode.Alpha8)) { pressed = 8;}
                else { pressed = 9;}

                //Go through the list of dominos array to find if there is a valid move
                if (board.Count == 0)
                {
                    //valid
                    //place on board in code
                    player1obj[pressed-1].transform.position = new Vector3(-4, 0, 0);
                    Debug.Log("Place on board");
                    board.Add(player1[pressed - 1]);
                    boardobj.Add(player1obj[pressed - 1]);
                    player1.RemoveAt(pressed - 1);
                    player1obj.RemoveAt(pressed - 1);


                    //rearrange the players hand to have no gaps
                    RearrangeHand(0);

                    turn = 1;
                    turnPlayed = false;

                }
                else
                {
                    for (int i = 0; i < board.Count; i++)
                    {
                        if (board[i][0] == player1[pressed][0] || board[i][1] == player1[pressed][0] || board[i][0] == player1[pressed][1] || board[i][1] == player1[pressed][1])
                        {
                            //place
                            Debug.Log("Valid Move");
                        }
                    }
                }

                //if there is a valid move then place it adjacent to that domino in the right orientation by labelling each domino either horizontal or vertical and using the dimensions

            }
        }
        else if (turn == 3 && turnPlayed == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
                {
                    //figure out which one was pressed
                    int pressed;
                    if (Input.GetKeyDown(KeyCode.Alpha1)) { pressed = 1; }
                    else if (Input.GetKeyDown(KeyCode.Alpha2)) { pressed = 2; }
                    else if (Input.GetKeyDown(KeyCode.Alpha3)) { pressed = 3; }
                    else if (Input.GetKeyDown(KeyCode.Alpha4)) { pressed = 4; }
                    else if (Input.GetKeyDown(KeyCode.Alpha5)) { pressed = 5; }
                    else if (Input.GetKeyDown(KeyCode.Alpha6)) { pressed = 6; }
                    else if (Input.GetKeyDown(KeyCode.Alpha7)) { pressed = 7; }
                    else if (Input.GetKeyDown(KeyCode.Alpha8)) { pressed = 8; }
                    else { pressed = 9; }

                    //Go through the list of dominos array to find if there is a valid move
                    if (board.Count == 0)
                    {
                        //valid
                        //place on board in code
                        player2obj[pressed - 1].transform.position = new Vector3(-4, 0, 0);
                        Debug.Log("Place on board");
                        board.Add(player2[pressed - 1]);
                        boardobj.Add(player2obj[pressed - 1]);
                        player2.RemoveAt(pressed - 1);
                        player2obj.RemoveAt(pressed - 1);


                        //rearrange the players hand to have no gaps
                        RearrangeHand(1);

                        turn = 1;
                        turnPlayed = false;

                    }
                    else
                    {
                        bool valid = false;
                        List<int> validBoardPos = new List<int>();
                        for (int i = 0; i < board.Count; i++)
                        {
                            if (board[i][0] == player2[pressed][0] || board[i][1] == player2[pressed][0] || board[i][0] == player2[pressed][1] || board[i][1] == player2[pressed][1])
                            {
                                valid = true;
                                validBoardPos.Add(i);
                                //DO TMRW
                                Debug.Log("Valid Move");
                                
                            }
                        }
                    }

                    //if there is a valid move then place it adjacent to that domino in the right orientation by labelling each domino either horizontal or vertical and using the dimensions

                }
            }
        
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
    
}
