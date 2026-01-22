using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    //vert grid runs from left to right
    public List<GameObject> VertBoard;
    //horiz grid runs from down to up
    public List<GameObject> HorizBoard;
    
    //domino[0] is top num, domino[1] is bottom num
    public List<int[]> vertBoardNums = new List<int[]>();
    
    public List<int[]> horizBoardNums = new List<int[]>();

    public int width;
    public int height;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //indexing starts from bottom corner
    void Start()
    {
        //initalize board
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height/2; j++)
            {
                int[] nullArray = { -1, -1};

                vertBoardNums.Add(nullArray);
            }
        }

        printVertBoard();

        for(int i = 0; i< height; i++)
        {
            for(int j=0; j< width/2; j++)
            {
                int[] nullArray = { -1, -1 };

                horizBoardNums.Add(nullArray);
            }

        }
        
        printHorizBoard();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void printVertBoard()
    {
        for (int i = 0; i < vertBoardNums.Count; i++)
        {
            Debug.Log("[ " + vertBoardNums[i][0] + ", "+ vertBoardNums[i][1] + "] "+ " Row= " + i/width);
        }
    }

    private void printHorizBoard()
    {
        for(int i=0; i< horizBoardNums.Count; i++)
        {
            Debug.Log("[ " + horizBoardNums[i][0] + ", " + horizBoardNums[i][1] + "] " + "Column= " + i / height);
        }
    }

    public void placeOnBoard(GameObject domino, int[] dominoNum, int[] connectionNum)
    {
        //if this domino is the first one on the bard
        if (connectionNum[0] == -1)
        {
            Debug.Log("made it into the board script");
            domino.transform.position = VertBoard[0].transform.position;
            VertBoard[0] = domino;
            vertBoardNums[0] = dominoNum;

        }
        else
        {
            //find which orientation the connecting piece is and orient this piece perpendicular if possible

            // if 0 = veritcal 1= horizontal
            int adjOrientation = -1;
            if(vertBoardNums.Contains(connectionNum)){
                adjOrientation = 0;
            }
            else if (horizBoardNums.Contains(connectionNum))
            {
                adjOrientation = 1;
            }


        }
    }
}
