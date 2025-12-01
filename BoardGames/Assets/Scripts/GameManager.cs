using UnityEngine;
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
public class GameManager : MonoBehaviour
{
    public int turn = 0;
    public int[] boneyard;
    public int[][] board;
    public int[][] boardPositions;

    public int[][] player1;

    public bool gameStarted = false;
    void Start()
    {
        public bool gameStarted = false;
        numPlayer = 2;
        Debug.Log("Game Started");
        domSet = [[0,0], [0,1], [0,2], [0,3], [0,4], [0,5], [0,6],
                 [1,1], [1,2], [1,3], [1,4], [1,5], [1,6],
                 [2,2], [2,3], [2,4], [2,5], [2,6],
                 [3,3], [3,4], [3,5], [3,6],
                 [4,4], [4,5], [4,6],
                 [5,5], [5,6],
                 [6,6]];
        
        domSetPos = [[0,1], [1,1], [2,1], [3,1], [4,1], [5,1], [6,1],
                          [7,1], [8,1], [9,1], [10,1], [11,1], [12,1],
                          [13,1], [14,1], [15,1], [16,1], [17,1],
                          [18,1], [19,1], [20,1], [21,1],
                          [22,1], [23,1], [24,1],
                          [25,1], [26,1],
                          [27,1]];

        
    }

    void Update()
    {
        
    }


    public void drawDominos():
    {
    //drawing for player 1
        for i in range(0,7):
        {
            validDraw = false;
            while (!validDraw):
            {
                randIndex = Random.Range(0, len(domSetPos));
                if (domSetPos[randIndex][2] == 1):
                {
                    player1.append(domSet[randIndex]);
                    domSetPos[randIndex][2] = 0;
                    validDraw = true;

                    //Animation of game object moving from boneyard to player hand
                }
            }
        }
    //drawing for player 2
        for i in range(0,7):
        {
            validDraw = false;
            while (!validDraw):
            {
                randIndex = Random.Range(0, len(domSetPos));
                if (domSetPos[randIndex][2] == 1):
                {
                    player2.append(domSet[randIndex]);
                    domSetPos[randIndex][2] = 0;
                    validDraw = true;

                    //Animation of game object moving from boneyard to player hand
                }
            }
        }
    }
}
