using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private Button Twoplayerbutton;
    [SerializeField] private Button Threeplayerbutton;
    [SerializeField] private Button Fourplayerbutton;

    [SerializeField] private Button OnePenguinsbutton;
    [SerializeField] private Button TwoPenguinsbutton;
    [SerializeField] private Button ThreePenguinsbutton;
    [SerializeField] private Button FourPenguinsbutton;

    public int numPenguins;
    public int players;
    public int turn = -1; // -1 = initial placement, 0 = player 1 first move
    public bool gameStarted;
    public bool gameSetUp = false;

    // -1 = invalid or gap
    // 1,2,3 = empty playable tile with that fish value
    // 4,5,6 = playable tile with penguin on it
    public int[,] board = new int[8, 8];

    public List<GameObject> value1Gameobjects = new List<GameObject>();
    public List<GameObject> value2Gameobjects = new List<GameObject>();
    public List<GameObject> value3Gameobjects = new List<GameObject>();
    public List<GameObject> boardGameObjects = new List<GameObject>();

    public List<GameObject> playerPiecesGameObjects = new List<GameObject>();

    private System.Random random = new System.Random();

    public int placingInitalPiecesTurn = 0;

    private bool destinationClicked = false;
    private GameObject clickedDestinationPiece = null;

    public Vector2Int[] playerPositions = new Vector2Int[4];

    public int[] scores = new int[4];

    /*
     * Even index row piece (a, b) connects to odd index row piece at same column or plus one (a+1, b), (a+1, b+1) (a-1, b) (a-1, b+1)
     * Odd index row piece (a, b) connects to even index row piece at same column or minus one (a+1, b) (a+1, b-1) (a-1, b) (a-1, b-1)
     */

    public Text instructions;

    public Text turnText;

    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text player3ScoreText;
    public Text player4ScoreText;

    void Start()
    {
        Twoplayerbutton.onClick.AddListener(twoButtonPressed);
        Threeplayerbutton.onClick.AddListener(threeButtonPressed);
        Fourplayerbutton.onClick.AddListener(fourButtonPressed);

        OnePenguinsbutton.onClick.AddListener(onePenguinButtonPressed);
        TwoPenguinsbutton.onClick.AddListener(twoPenguinButtonPressed);
        ThreePenguinsbutton.onClick.AddListener(threePenguinButtonPressed);
        FourPenguinsbutton.onClick.AddListener(fourPenguinButtonPressed);
    }

    void Update()
    {
        if (!gameStarted) return;

        if (!gameSetUp)
        {
            playerPanel.SetActive(false);

            intializingBoard();
            assigningPieceValues();

            gameSetUp = true;
        }
        else
        {
            Turn();
        }

        if(turn == -1)
        {
            instructions.text = "In the order you'd like to play, take turns selecting starting positions for each of your penguins. Remember the color of your penguin!";
        }
        else
        {
            instructions.text = "Click where you would like to move. You can only move in a straight line, cannot go through penuins, and cannot go through open spaces.";
        }

        if (turn != -1)
        {
            updateScoresText();
            if (turn % players == 0)
            {
                turnText.text = "Turn: Player 1";
            }
            else if (turn % players == 1)
            {
                turnText.text = "Turn: Player 2";
            }
            else if (turn % players == 2)
            {
                turnText.text = "Turn: Player 3";
            }
            else if (turn % players == 3)
            {
                turnText.text = "Turn: Player 4";
            }
        }
    }

    private void updateScoresText()
    {
        for (int i = 0; i < players; i++)
        {
            if (i == 0)
            {
                player1ScoreText.text = "Player 1: " + scores[0];
            }
            else if (i == 1)
            {
                player2ScoreText.text = "Player 2: " + scores[1];
            }
            else if (i == 2)
            {
                player3ScoreText.text = "Player 3: " + scores[2];
            }
            else if (i == 3)
            {
                player4ScoreText.text = "Player 4: " + scores[3];
            }
        }
    }

    private void Turn()
    {
        if (!destinationClicked || clickedDestinationPiece == null) return;

        int currentPlayer = turn % players;

        
        int destinationIndex = boardGameObjects.IndexOf(clickedDestinationPiece);
        if (destinationIndex == -1)
        {
            destinationClicked = false;
            clickedDestinationPiece = null;
            return;
        }

        int toRow = destinationIndex / 8;
        int toCol = destinationIndex % 8;

        Vector2Int from = playerPositions[currentPlayer];
        
        if (!IsValidPlayableCell(toRow, toCol) || board[toRow, toCol] > 3)
        {
            if (from.x == toRow && from.y == toCol && IsStuck(from.x, from.y))
            {
                Debug.Log("Your stuck");

                //TAKE PENGUIN OUT OF THE GAME AND NOT THE BOARDpIECE
                boardGameObjects[from.x * 8 + from.y].SetActive(false);
                board[from.x, from.y] = -1;
                turn++;
            }
            Debug.Log("Destination is not an empty playable tile.");
            destinationClicked = false;
            clickedDestinationPiece = null;
            return;
        }

        if (from.x == toRow && from.y == toCol)
        {

            if (IsStuck(from.x, from.y))
            {
                Debug.Log("Your stuck");
                boardGameObjects[from.x * 8 + from.y].SetActive(false);
                board[from.x, from.y] = -1;
                turn++;
            }
            Debug.Log("You must move to a different tile.");
            destinationClicked = false;
            clickedDestinationPiece = null;

            
            return;
        }

        if (IsStraightLineMove(from.x, from.y, toRow, toCol) && IsPathClear(from.x, from.y, toRow, toCol))
        {
            scores[turn % players] += board[from.x, from.y] - 3;

            board[from.x, from.y] -= 3;
            board[toRow, toCol] += 3;


            playerPiecesGameObjects[currentPlayer].transform.position =
                new Vector3(clickedDestinationPiece.transform.position.x, clickedDestinationPiece.transform.position.y, -1f);

            board[from.x, from.y] = -1;
            boardGameObjects[from.x * 8 + from.y].SetActive(false);


            playerPositions[currentPlayer] = new Vector2Int(toRow, toCol);

            Debug.Log("Player " + (currentPlayer + 1) + " moved successfully.");

            turn++;
        }
        else
        {
            Debug.Log("Invalid move.");
        }

        destinationClicked = false;
        clickedDestinationPiece = null;
    }


    public void SelectBoardPiece(GameObject clickedPiece)
    {
        int index = boardGameObjects.IndexOf(clickedPiece);
        if (index == -1) return;

        int row = index / 8;
        int col = index % 8;

        // Initial penguin placement
        if (turn == -1)
        {
            if (placingInitalPiecesTurn >= players) return;
            if (!IsValidPlayableCell(row, col)) return;
            if (board[row, col] > 3) return; // already occupied

            playerPiecesGameObjects[placingInitalPiecesTurn].SetActive(true);
            playerPiecesGameObjects[placingInitalPiecesTurn].transform.position =
                new Vector3(clickedPiece.transform.position.x, clickedPiece.transform.position.y, -1f);

            board[row, col] += 3;
            playerPositions[placingInitalPiecesTurn] = new Vector2Int(row, col);

            placingInitalPiecesTurn++;

            if (placingInitalPiecesTurn >= players)
            {
                turn = 0;
                Debug.Log("All players placed. Player 1 turn starts.");
            }

            return;
        }

        // Player 1 first turn: click destination tile
        if (turn >= 0)
        {
            destinationClicked = true;
            clickedDestinationPiece = clickedPiece;
        }
    }

    private void intializingBoard()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }
            else
            {
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
        // 1-fish tiles
        for (int i = 0; i < 30; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int randomRow = random.Next(0, 8);
                int randomCol = (randomRow % 2 == 0) ? random.Next(0, 8) : random.Next(1, 8);

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 1;
                    placed = true;
                }
            }
        }

        // 2-fish tiles
        for (int i = 0; i < 20; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int randomRow = random.Next(0, 8);
                int randomCol = (randomRow % 2 == 0) ? random.Next(0, 8) : random.Next(1, 8);

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 2;
                    placed = true;
                }
            }
        }

        // 3-fish tiles
        for (int i = 0; i < 10; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int randomRow = random.Next(0, 8);
                int randomCol = (randomRow % 2 == 0) ? random.Next(0, 8) : random.Next(1, 8);

                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 3;
                    placed = true;
                }
            }
        }

        creatingGameObjectBoard();
        placingGamePieces();
    }

    private void creatingGameObjectBoard()
    {
        boardGameObjects.Clear();
        for (int i = 0; i < 64; i++)
        {
            boardGameObjects.Add(null);
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == 1)
                {
                    for (int k = 0; k < value1Gameobjects.Count; k++)
                    {
                        BoardPiece bp = value1Gameobjects[k].GetComponent<BoardPiece>();
                        if (!bp.placedOnMainBoard)
                        {
                            boardGameObjects[i * 8 + j] = value1Gameobjects[k];
                            bp.placedOnMainBoard = true;
                            bp.gameManager = this;
                            break;
                        }
                    }
                }
                else if (board[i, j] == 2)
                {
                    for (int k = 0; k < value2Gameobjects.Count; k++)
                    {
                        BoardPiece bp = value2Gameobjects[k].GetComponent<BoardPiece>();
                        if (!bp.placedOnMainBoard)
                        {
                            boardGameObjects[i * 8 + j] = value2Gameobjects[k];
                            bp.placedOnMainBoard = true;
                            bp.gameManager = this;
                            break;
                        }
                    }
                }
                else if (board[i, j] == 3)
                {
                    for (int k = 0; k < value3Gameobjects.Count; k++)
                    {
                        BoardPiece bp = value3Gameobjects[k].GetComponent<BoardPiece>();
                        if (!bp.placedOnMainBoard)
                        {
                            boardGameObjects[i * 8 + j] = value3Gameobjects[k];
                            bp.placedOnMainBoard = true;
                            bp.gameManager = this;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void placingGamePieces()
    {
        float firstXpos = -6f;
        float firstYpos = -5f;

        for (int i = 0; i < 64; i++)
        {
            if (boardGameObjects[i] == null) continue;

            int row = i / 8;
            int col = i % 8;

            if (row % 2 == 0)
            {
                boardGameObjects[i].transform.position = new Vector3(firstXpos + 2f * col, firstYpos + 1.72f * row, 0f);
            }
            else
            {
                if (col == 0) continue;

                boardGameObjects[i].transform.position = new Vector3(firstXpos - 1f + 2f * col, firstYpos + 1.72f * row, 0f);
            }
        }
    }

    private bool IsValidPlayableCell(int row, int col)
    {
        if (row < 0 || row >= 8 || col < 0 || col >= 8) return false;
        if (board[row, col] == -1) return false;
        return true;
    }

    private List<Vector2Int> GetNeighbors(int row, int col)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (row % 2 == 0)
        {
            neighbors.Add(new Vector2Int(row, col - 1));
            neighbors.Add(new Vector2Int(row, col + 1));
            neighbors.Add(new Vector2Int(row - 1, col));
            neighbors.Add(new Vector2Int(row - 1, col + 1));
            neighbors.Add(new Vector2Int(row + 1, col));
            neighbors.Add(new Vector2Int(row + 1, col + 1));
        }
        else
        {
            neighbors.Add(new Vector2Int(row, col - 1));
            neighbors.Add(new Vector2Int(row, col + 1));
            neighbors.Add(new Vector2Int(row - 1, col - 1));
            neighbors.Add(new Vector2Int(row - 1, col));
            neighbors.Add(new Vector2Int(row + 1, col - 1));
            neighbors.Add(new Vector2Int(row + 1, col));
        }

        return neighbors;
    }

    private Vector2Int GetNextInSameDirection(int row, int col, int directionIndex)
    {
        if (row % 2 == 0)
        {
            switch (directionIndex)
            {
                case 0: return new Vector2Int(row, col - 1);     // left
                case 1: return new Vector2Int(row, col + 1);     // right
                case 2: return new Vector2Int(row - 1, col);     // up-left
                case 3: return new Vector2Int(row - 1, col + 1); // up-right
                case 4: return new Vector2Int(row + 1, col);     // down-left
                case 5: return new Vector2Int(row + 1, col + 1); // down-right
            }
        }
        else
        {
            switch (directionIndex)
            {
                case 0: return new Vector2Int(row, col - 1);     // left
                case 1: return new Vector2Int(row, col + 1);     // right
                case 2: return new Vector2Int(row - 1, col - 1); // up-left
                case 3: return new Vector2Int(row - 1, col);     // up-right
                case 4: return new Vector2Int(row + 1, col - 1); // down-left
                case 5: return new Vector2Int(row + 1, col);     // down-right
            }
        }

        return new Vector2Int(row, col);
    }

    private bool IsStraightLineMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        for (int direction = 0; direction < 6; direction++)
        {
            Vector2Int current = GetNextInSameDirection(fromRow, fromCol, direction);

            while (IsValidPlayableCell(current.x, current.y))
            {
                if (current.x == toRow && current.y == toCol)
                {
                    return true;
                }

                current = GetNextInSameDirection(current.x, current.y, direction);
            }
        }

        return false;
    }

    private bool IsPathClear(int fromRow, int fromCol, int toRow, int toCol)
    {
        for (int direction = 0; direction < 6; direction++)
        {
            Vector2Int current = GetNextInSameDirection(fromRow, fromCol, direction);

            while (IsValidPlayableCell(current.x, current.y))
            {
                if (current.x == toRow && current.y == toCol)
                {
                    return board[current.x, current.y] <= 3;
                }

                // Cannot pass through another penguin
                if (board[current.x, current.y] > 3)
                {
                    break;
                }

                current = GetNextInSameDirection(current.x, current.y, direction);
            }
        }

        return false;
    }

    private bool IsStuck(int fromRow, int fromCol)
    {
        if(IsPathClear(fromRow, fromCol, fromRow + 1, fromCol) == false && IsPathClear(fromRow, fromCol, fromRow + 1, fromCol+1) == false && IsPathClear(fromRow, fromCol, fromRow - 1, fromCol) == false && IsPathClear(fromRow, fromCol, fromRow - 1, fromCol-1) == false)
        {
            return true;
        }

        return false;
    }
    private void twoButtonPressed()
    {
        players = 2;
        //gameStarted = true;
        turn = -1;
        placingInitalPiecesTurn = 0;
    }

    private void threeButtonPressed()
    {
        players = 3;
        //gameStarted = true;
        turn = -1;
        placingInitalPiecesTurn = 0;
    }

    private void fourButtonPressed()
    {
        players = 4;
        //gameStarted = true;
        turn = -1;
        placingInitalPiecesTurn = 0;
    }

    private void onePenguinButtonPressed()
    {
        gameStarted = true;
        numPenguins = 1;
    }
    private void twoPenguinButtonPressed()
    {
        numPenguins = 2;
        gameStarted = true;

    }

    private void threePenguinButtonPressed()
    {
        numPenguins = 3;
        gameStarted = true;

    }

    private void fourPenguinButtonPressed()
    {
        numPenguins = 4;
        gameStarted = true;

    }
}