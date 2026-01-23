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
        else //not first one on board, connects to another piece
        {
            //find which orientation the connecting piece is and orient this piece perpendicular if possible
            if(vertBoardNums.Contains(connectionNum)){
                int[] possiblePlace = findingPossiblePlacements(true, connectionNum, dominoNum);
                
            }
            else if (horizBoardNums.Contains(connectionNum))
            {

                int[] possiblePlace = findingPossiblePlacements(false , connectionNum, dominoNum);
            }


        }
    }

    private int[] findingPossiblePlacements(bool vertAdj, int[] connectionNum, int[] dominoNum)
    {
        int[] possbilities = {};
        if (vertAdj)
        {
            int adjIndex = vertBoardNums.IndexOf(connectionNum);

            if (adjIndex % width - 2 >= 0 && adjIndex % width + 2 <width)
            {
                //CASE WHERE THERE CAN BE TWO DOMINOS PLACED ON EITHER SIDE

                //check upper


                //check lower
            }
            else if (adjIndex % width - 2 < 0 && adjIndex % width + 2 < width)
            {
                //CASE WHERE THE CAN BE A DOMINO PLACED ON THE RIGHT SIDE 
            }
            else if (adjIndex % width - 2 >= 0 && adjIndex % width + 2 >= width)
            {
                //CASE WHERE THERE CAN ONLY BE A DOMINO ON THE LEFT SIDE
            }
        }
        else
        {
            int adjIndex = horizBoardNums.IndexOf(connectionNum);

            if(adjIndex % height -1 >= 0 && adjIndex % height +1 < height)
            {
                //CASE WHERE THERE CAN BE TWO DOMINO PLACED ABOVE AND BELOW
            }
            else if(adjIndex % height -1 >= 0 && adjIndex % height + 1 >= height)
            {
                //CASE WHERE THERE CAN BE ONLY BE A DOMINO PLACED BELOW 
            }
            else if(adjIndex % height - 1 < 0 && adjIndex % height + 1 < height)
            {
                //CASE WHERE THE CAN ONLY BE A DOMINO PLACED ABOVE
            }


        }


            
        return possbilities;
    }
}
