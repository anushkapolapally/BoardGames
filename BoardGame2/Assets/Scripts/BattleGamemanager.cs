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

    [SerializeField] Button upButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button downButton;
    [SerializeField] Button leftButton;

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
            initalInstructions.text = "Place your ships in their starting positions. Click a ship to select it and then type the row and column you would like the bottomest/leftmost part of the ship to be on.";

            for(int i = 0; i < player1ships.Count; i++)
            {
                if (player1ships[i].GetComponent<Ship>().getClicked() == true)
                {
                    placingPieces(player1ships[i]);

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

    private void placingPieces(GameObject ship)
    {
       
    }

    //button method

   



}
