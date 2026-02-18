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

    [SerializeField] Button upButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button downButton;
    [SerializeField] Button leftButton;

    public List<float> player1rowpositions = new List<float>();
    public List<float> player1columnpositions = new List<float>();

    public List<float> player2rowpositions = new List<float>();
    public List<float> player2columnpositions = new List<float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initalInstructions.text = "";

        Button up = upButton.GetComponent<Button>();
        //up.onClick.AddListener(changeUp());
    }



    // Update is called once per frame
    void Update()
    {
        if (startedGame == false)
        {
            startGame();
        }
    }


    private void startGame()
    {
        if (turn == 0)
        {
            initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on. Typed: ";

            for (int i = 0; i < player1ships.Count; i++)
            {
                if (player1ships[i].GetComponent<Ship>().getClicked() == true)
                {
                   
                        placingPiecesText(player1ships[i]);
                        //Debug.Log(typedText);
                    
                }
            }


            //player 1 places pieces

        }
        else if (turn == 1)
        {
            //player 2 places pieces
            startedGame = true;
        }
    }

    private void placingPiecesText(GameObject ship)
    {
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
        }
        else
        {
            placingPiecesPosition(typedText, ship);
        }

        // Shows live typing in Console
        //Debug.Log("Currently typing: " + typedText);
        initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on. Typed: " + typedText;

    }

    private void placingPiecesPosition(string typedText, GameObject ship)
    {
        Debug.Log("placingpiecesposition");
        int row = -1;
        int column = -1;
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


        ship.transform.position = new Vector3(player1rowpositions[row], 6.5f, (player1columnpositions[column] + player1columnpositions[column])/2);
        Debug.Log("row: " + row + " column: " + column);
    }
}

   




