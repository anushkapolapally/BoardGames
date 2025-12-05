using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


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
    public List<int[]> player2 = new List<int[]>();

    public List<GameObject> gameObjects = new List<GameObject>();

    public List<GameObject> placeholder1 = new List<GameObject>();

    [SerializeField] int turn = 0;

    public Camera targetCamera;

    public bool startPressed = false;

    [SerializeField] Button startButton;
   

    void Start()
    {

        Debug.Log("Start Game");
        startGame();

        Button button = startButton.GetComponent<Button>();
        button.onClick.AddListener(changeStartPressed);
    }
    void changeStartPressed()
    {
        startPressed = true;
        startButton.transform.position = new Vector3(-100, -100, 0);
        Debug.Log("changed start Pressed");
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

        for (int j = 0; j < boneyard.Length; j++)
        {
           // Debug.Log(boneyard[j, 0]);
            //Debug.Log(boneyard[j, 1]);

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
                boneyard[randIndex, 2] = 0;
                count2++;

            }
        }

        for (int i = 0; i < player2.Count; i++)
        {
           // Debug.Log(player2[i][0].ToString());
           // Debug.Log(player2[i][1].ToString());
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
            targetCamera.transform.position = new Vector3(0, 41, 30);
            targetCamera.transform.eulerAngles = originalRotation;
        }
    }
}
