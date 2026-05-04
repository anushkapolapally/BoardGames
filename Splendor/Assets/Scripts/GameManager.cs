using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numOfPlayers;
    public List<GameObject> greenCards = new List<GameObject>();
    public List<GameObject> yellowCards = new List<GameObject>();
    public List<GameObject> blueCards = new List<GameObject>();
    public List<GameObject> nobleTiles = new List<GameObject>();

    public List<GameObject> activeGreenCards = new List<GameObject>();
    public List<GameObject> activeYellowCards = new List<GameObject>();
    public List<GameObject> activeBlueCards = new List<GameObject>();
    public List<GameObject> activeNobleTiles = new List<GameObject>();

    public Text goldCoinText;
    public Text brownCoinText;
    public Text redCoinText;
    public Text greenCoinText;
    public Text blueCoinText;
    public Text diamondCoinText;

    public int goldCoinNum = 5;
    public int brownCoinNum = 7;
    public int redCoinNum = 7;
    public int greenCoinNum = 7;
    public int blueCoinNum = 7;
    public int diamondCoinNum = 7;



    public List<GameObject> allCards = new List<GameObject>();

    public GameObject choosingPlayerBackground;
    public Text choosingPlayerText;
    [SerializeField] Button player2Button;
    [SerializeField] Button player3Button;
    [SerializeField] Button player4Button;

    public List<GameObject> playerIcons= new List<GameObject>();

    public List<GameObject> playerPrefabs = new List<GameObject>();


    //item for each player = [YellowCoins, YellowBonuses, BrownCoins, BrownBonuses, RedCoins, RedBonuses, GreenCoins, GreenBonuses, BlueCoins, BlueBonuses, DiamondCoins, DiamondBonuses,  Points]
    public List<int[]> playerCoinsData = new List<int[]>();

    public int turn = 0;


    public bool p2IconHover = false;
    public bool p3IconHover = false;
    public bool p4IconHover = false;


    // yellow, brown, red, green, blue, diamond
    public int[] clickedData = { 0, 0, 0, 0, 0, 0 };

    public List<GameObject> coinObjects = new List<GameObject>();



    void Start()
    {
        player2Button.onClick.AddListener(button2Pressed);
        player3Button.onClick.AddListener(button3Pressed);
        player4Button.onClick.AddListener(button4Pressed);

        settingInitialBoard();
        int[] startItem = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        




        //initalizing coins
        if (numOfPlayers == 2)
        {
            brownCoinNum = 4;
            redCoinNum = 4;
            greenCoinNum = 4;
            blueCoinNum = 4;
            diamondCoinNum = 4;

            playerIcons[2].SetActive(false);
            playerIcons[3].SetActive(false);


            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);

          
        }
        else if(numOfPlayers == 3) 
        {
            brownCoinNum = 5;
            redCoinNum = 5;
            greenCoinNum = 5;
            blueCoinNum = 5;
            diamondCoinNum = 5;

            playerIcons[3].SetActive(false);

            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);

        }
        else
        {
            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);
            playerCoinsData.Add(startItem);
        }


    }

    void Update()
    {
        refreshingPrefabs();
        setPrefabPositions();
        showingValidPieces();
        Turn();

        if (numOfPlayers == 2) {
            playerIcons[2].SetActive(false);
            playerIcons[3].SetActive(false);
        }
        if (numOfPlayers == 3){playerIcons[3].SetActive(false);}
        //coin text
        goldCoinText.text = goldCoinNum.ToString();
        brownCoinText.text = brownCoinNum.ToString();
        redCoinText.text = redCoinNum.ToString();
        greenCoinText.text = greenCoinNum.ToString();
        blueCoinText.text = blueCoinNum.ToString();
        diamondCoinText.text = diamondCoinNum.ToString();


    }

    private void Turn()
    {
        //player 1 
        if (turn % numOfPlayers == 0) {
            //check if 2 of the same coin have been pressed
            for (int i = 0;i< 6; i++)
            {
                if (clickedData[i] >= 2)
                {
                    //reset all the things
                    
                    clickedData[0] = 0;
                    clickedData[1] = 0;
                    clickedData[2] = 0;
                    clickedData[3] = 0;
                    clickedData[4] = 0;
                    clickedData[5] = 0;

                    for(int j = 0; i<6; i++)
                    {
                        coinObjects[j].transform.GetChild(0).gameObject.SetActive(true);
                        coinObjects[j].GetComponent<CircleCollider2D>().enabled = true;
                    }

                    turn++;
                }
            }
            if (turn % numOfPlayers == 0)
            {
                if(clickedData.Sum()>= 3)
                {
                    turn++;
                }
            }
            
            //OR check if a total of 3 have been pressed
        }
        //player 2

    }

    private void showingValidPieces()
    {

        int[] coinData = {goldCoinNum, brownCoinNum, redCoinNum, greenCoinNum, blueCoinNum, diamondCoinNum};
        //check for coin num conditions
        if(playerCoinsData[turn%numOfPlayers][0] - playerCoinsData[turn % numOfPlayers][1] + playerCoinsData[turn % numOfPlayers][2] - playerCoinsData[turn % numOfPlayers][3] + playerCoinsData[turn % numOfPlayers][4] - playerCoinsData[turn % numOfPlayers][5] + playerCoinsData[turn % numOfPlayers][6] - playerCoinsData[turn % numOfPlayers][7] + playerCoinsData[turn % numOfPlayers][8] - playerCoinsData[turn % numOfPlayers][9] + playerCoinsData[turn % numOfPlayers][10] - playerCoinsData[turn % numOfPlayers][11] < 10)
        {
            //if someone already has a token, check if there are at least 3 of that token left
            for(int i = 0; i< 6; i++)
            {
                if (clickedData[i] >= 1)
                {
                    if(coinData[i] < 3)
                    {
                        coinObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                        coinObjects[i].GetComponent<CircleCollider2D>().enabled = false;
                    }
                    else
                    {
                        coinObjects[i].transform.GetChild(0).gameObject.SetActive(true);
                        coinObjects[i].GetComponent<CircleCollider2D>().enabled = true;

                    }
                }
            }
        }
        else
        {


        }

        //end turn
    }
    private void refreshingPrefabs()
    {
        if (numOfPlayers == 2)
        {
            //player 1 update
            
            playerPrefabs[0].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][0].ToString();
            playerPrefabs[0].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][1].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][2].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][3].ToString();
            playerPrefabs[0].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][4].ToString(); ;
            playerPrefabs[0].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][5].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][6].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][7].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][8].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][9].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][10].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][11].ToString();


            playerPrefabs[0].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[0][0] - playerCoinsData[0][1] + playerCoinsData[0][2] - playerCoinsData[0][3] + playerCoinsData[0][4] - playerCoinsData[0][5] + playerCoinsData[0][6] - playerCoinsData[0][7] + playerCoinsData[0][8] - playerCoinsData[0][9] + playerCoinsData[0][10] - playerCoinsData[0][11]).ToString();
            playerPrefabs[0].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][12].ToString();

            //player 2 update 

            playerPrefabs[1].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][0].ToString();
            playerPrefabs[1].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][1].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][2].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][3].ToString();
            playerPrefabs[1].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][4].ToString(); ;
            playerPrefabs[1].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][5].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][6].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][7].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][8].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][9].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][10].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][11].ToString();


            playerPrefabs[1].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[1][0] - playerCoinsData[1][1] + playerCoinsData[1][2] - playerCoinsData[1][3] + playerCoinsData[1][4] - playerCoinsData[1][5] + playerCoinsData[1][6] - playerCoinsData[1][7] + playerCoinsData[1][8] - playerCoinsData[1][9] + playerCoinsData[1][10] - playerCoinsData[1][11]).ToString();
            playerPrefabs[1].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][12].ToString();
        }
        else if(numOfPlayers == 3)
        {
            //player 1 update

            playerPrefabs[0].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][0].ToString();
            playerPrefabs[0].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][1].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][2].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][3].ToString();
            playerPrefabs[0].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][4].ToString(); ;
            playerPrefabs[0].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][5].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][6].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][7].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][8].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][9].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][10].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][11].ToString();


            playerPrefabs[0].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[0][0] - playerCoinsData[0][1] + playerCoinsData[0][2] - playerCoinsData[0][3] + playerCoinsData[0][4] - playerCoinsData[0][5] + playerCoinsData[0][6] - playerCoinsData[0][7] + playerCoinsData[0][8] - playerCoinsData[0][9] + playerCoinsData[0][10] - playerCoinsData[0][11]).ToString();
            playerPrefabs[0].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][12].ToString();

            //player 2 update 

            playerPrefabs[1].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][0].ToString();
            playerPrefabs[1].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][1].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][2].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][3].ToString();
            playerPrefabs[1].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][4].ToString(); ;
            playerPrefabs[1].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][5].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][6].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][7].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][8].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][9].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][10].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][11].ToString();


            playerPrefabs[1].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[1][0] - playerCoinsData[1][1] + playerCoinsData[1][2] - playerCoinsData[1][3] + playerCoinsData[1][4] - playerCoinsData[1][5] + playerCoinsData[1][6] - playerCoinsData[1][7] + playerCoinsData[1][8] - playerCoinsData[1][9] + playerCoinsData[1][10] - playerCoinsData[1][11]).ToString();
            playerPrefabs[1].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][12].ToString();

            //player 3 update

            playerPrefabs[2].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][0].ToString();
            playerPrefabs[2].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][1].ToString();
            playerPrefabs[2].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][2].ToString();
            playerPrefabs[2].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][3].ToString();
            playerPrefabs[2].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][4].ToString(); ;
            playerPrefabs[2].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][5].ToString();
            playerPrefabs[2].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][6].ToString();
            playerPrefabs[2].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][7].ToString();
            playerPrefabs[2].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][8].ToString();
            playerPrefabs[2].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][9].ToString();
            playerPrefabs[2].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][10].ToString();
            playerPrefabs[2].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][11].ToString();


            playerPrefabs[2].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[2][0] - playerCoinsData[2][1] + playerCoinsData[2][2] - playerCoinsData[2][3] + playerCoinsData[2][4] - playerCoinsData[2][5] + playerCoinsData[2][6] - playerCoinsData[2][7] + playerCoinsData[2][8] - playerCoinsData[2][9] + playerCoinsData[2][10] - playerCoinsData[2][11]).ToString();
            playerPrefabs[2].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][12].ToString();
        }
        else if (numOfPlayers == 4)
        {
            //player 1 update

            playerPrefabs[0].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][0].ToString();
            playerPrefabs[0].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][1].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][2].ToString();
            playerPrefabs[0].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][3].ToString();
            playerPrefabs[0].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][4].ToString(); ;
            playerPrefabs[0].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][5].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][6].ToString();
            playerPrefabs[0].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][7].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][8].ToString();
            playerPrefabs[0].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][9].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][10].ToString();
            playerPrefabs[0].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][11].ToString();


            playerPrefabs[0].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[0][0] - playerCoinsData[0][1] + playerCoinsData[0][2] - playerCoinsData[0][3] + playerCoinsData[0][4] - playerCoinsData[0][5] + playerCoinsData[0][6] - playerCoinsData[0][7] + playerCoinsData[0][8] - playerCoinsData[0][9] + playerCoinsData[0][10] - playerCoinsData[0][11]).ToString();
            playerPrefabs[0].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[0][12].ToString();

            //player 2 update 

            playerPrefabs[1].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][0].ToString();
            playerPrefabs[1].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][1].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][2].ToString();
            playerPrefabs[1].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][3].ToString();
            playerPrefabs[1].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][4].ToString(); ;
            playerPrefabs[1].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][5].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][6].ToString();
            playerPrefabs[1].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][7].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][8].ToString();
            playerPrefabs[1].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][9].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][10].ToString();
            playerPrefabs[1].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][11].ToString();


            playerPrefabs[1].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[1][0] - playerCoinsData[1][1] + playerCoinsData[1][2] - playerCoinsData[1][3] + playerCoinsData[1][4] - playerCoinsData[1][5] + playerCoinsData[1][6] - playerCoinsData[1][7] + playerCoinsData[1][8] - playerCoinsData[1][9] + playerCoinsData[1][10] - playerCoinsData[1][11]).ToString();
            playerPrefabs[1].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[1][12].ToString();

            //player 3 update

            playerPrefabs[2].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][0].ToString();
            playerPrefabs[2].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][1].ToString();
            playerPrefabs[2].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][2].ToString();
            playerPrefabs[2].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][3].ToString();
            playerPrefabs[2].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][4].ToString(); ;
            playerPrefabs[2].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][5].ToString();
            playerPrefabs[2].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][6].ToString();
            playerPrefabs[2].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][7].ToString();
            playerPrefabs[2].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][8].ToString();
            playerPrefabs[2].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][9].ToString();
            playerPrefabs[2].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][10].ToString();
            playerPrefabs[2].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][11].ToString();


            playerPrefabs[2].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[2][0] - playerCoinsData[2][1] + playerCoinsData[2][2] - playerCoinsData[2][3] + playerCoinsData[2][4] - playerCoinsData[2][5] + playerCoinsData[2][6] - playerCoinsData[2][7] + playerCoinsData[2][8] - playerCoinsData[2][9] + playerCoinsData[2][10] - playerCoinsData[2][11]).ToString();
            playerPrefabs[2].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[2][12].ToString();

            //player 4 update

            playerPrefabs[3].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][0].ToString();
            playerPrefabs[3].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][1].ToString();
            playerPrefabs[3].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][2].ToString();
            playerPrefabs[3].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][3].ToString();
            playerPrefabs[3].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][4].ToString(); ;
            playerPrefabs[3].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][5].ToString();
            playerPrefabs[3].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][6].ToString();
            playerPrefabs[3].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][7].ToString();
            playerPrefabs[3].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][8].ToString();
            playerPrefabs[3].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][9].ToString();
            playerPrefabs[3].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][10].ToString();
            playerPrefabs[3].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][11].ToString();


            playerPrefabs[3].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = (playerCoinsData[3][0] - playerCoinsData[3][1] + playerCoinsData[3][2] - playerCoinsData[3][3] + playerCoinsData[3][4] - playerCoinsData[3][5] + playerCoinsData[3][6] - playerCoinsData[3][7] + playerCoinsData[3][8] - playerCoinsData[3][9] + playerCoinsData[3][10] - playerCoinsData[3][11]).ToString();
            playerPrefabs[3].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = playerCoinsData[3][12].ToString();
        }
    }

    private void setPrefabPositions()
    {
        if (numOfPlayers != 0)
        {
            playerPrefabs[turn % numOfPlayers].transform.position = new Vector3(-2f, -4.24f, -3);
        }

        if(turn-1 % numOfPlayers == 0)
        {
           playerPrefabs[0].transform.position = new Vector3(-16.775f, 2.9325f, -12f);


            

           
        }
    }
    private void settingInitialBoard()
    {
        //green cards
        Vector3 greendeckPosition = greenCards[4].transform.position;
        Vector3 nobledeckPosition = nobleTiles[9].transform.position;
        Vector3[] greenCardPositions = {greenCards[0].transform.position, greenCards[1].transform.position, greenCards[2].transform.position, greenCards[3].transform.position};

        var random = new System.Random();
        // define range
        int min = 0;
        int max = 39;

        // create list of numbers in range
        var numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // take first 4
        var result = numbers.Take(4).ToList();
        int j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            greenCards[num].transform.position = greenCardPositions[j];

            //switching in list
            activeGreenCards.Add(greenCards[num]);
            j++;
        }
        int k = 0;
        foreach(var num in result)
        {
            if (!result.Contains(k))
            {
                greenCards[k].transform.position = greendeckPosition;
            }
         
            k++;
        }


        //yellow cards
        Vector3[] yellowCardPositions = { yellowCards[0].transform.position, yellowCards[1].transform.position, yellowCards[2].transform.position, yellowCards[3].transform.position };

        random = new System.Random();
        // define range
        min = 0;
        max = 30;

        // create list of numbers in range
        numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // take first 4
        result = numbers.Take(4).ToList();
        j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            yellowCards[num].transform.position = yellowCardPositions[j];

            //switching in list
            activeYellowCards.Add(yellowCards[num]);
            j++;
        }
        k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                yellowCards[k].transform.position = greendeckPosition;
            }

            k++;
        }

        //blue cards
        Vector3[] blueCardPositions = { blueCards[0].transform.position, blueCards[1].transform.position, blueCards[2].transform.position, blueCards[3].transform.position };

        random = new System.Random();
        // define range
        min = 0;
        max = 20;

        // create list of numbers in range
        numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        // take first 4
        result = numbers.Take(4).ToList();
        j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            blueCards[num].transform.position = blueCardPositions[j];

            //switching in list
            activeBlueCards.Add(blueCards[num]);
            j++;
        }
        k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                blueCards[k].transform.position = greendeckPosition;
            }

            k++;
        }

        //nobles 

       /* random = new System.Random();
        // define range
        min = 0;
        max = 10;



        // create list of numbers in range
        numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        Vector3[] nobleTilePositions;

        if (numOfPlayers == 2)
        {
            //3noble cards
            result = numbers.Take(3).ToList();
            nobleTilePositions = new Vector3[] { nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position};
        }
        else if (numOfPlayers == 3)
        {
            result = numbers.Take(4).ToList();
            nobleTilePositions = new Vector3[] { nobleTiles[0].transform.position, nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position };

        }
        else
        {
            result = numbers.Take(5).ToList();
            nobleTilePositions = new Vector3[] { nobleTiles[0].transform.position, nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position, nobleTiles[4].transform.position };

        }
        j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            nobleTiles[num].transform.position = nobleTilePositions[j];

            //switching in list
            activeNobleTiles.Add(nobleTiles[num]);
            j++;
        }
        k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                nobleTiles[k].transform.position = nobledeckPosition;
            }

            k++;
        }*/



    }
    void button2Pressed()
    {
        numOfPlayers = 2;
        choosingPlayerBackground.SetActive(false);
        choosingPlayerText.text = "";
        player2Button.transform.position = new Vector3(50000, 0, 0);
        player3Button.transform.position = new Vector3(50000, 0, 0);
        player4Button.transform.position = new Vector3(50000, 0, 0);

        var random = new System.Random();
        Vector3 nobledeckPosition = nobleTiles[9].transform.position;

        // define range
        int min = 0;
        int max = 10;



        // create list of numbers in range
        var numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        Vector3[] nobleTilePositions;

        
            var result = numbers.Take(3).ToList();
            nobleTilePositions = new Vector3[] { nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position };
        nobleTiles[0].transform.position = nobledeckPosition;
        nobleTiles[4].transform.position = nobledeckPosition;

        int j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            nobleTiles[num].transform.position = nobleTilePositions[j];

            //switching in list
            activeNobleTiles.Add(nobleTiles[num]);
            j++;
        }
        int k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                nobleTiles[k].transform.position = nobledeckPosition;
            }

            k++;
        }

      goldCoinNum = 5;
    brownCoinNum = 4;
    redCoinNum = 4;
   greenCoinNum = 4;
    blueCoinNum = 4;
   diamondCoinNum = 4;
}
    void button3Pressed()
    {
        numOfPlayers = 3;
        choosingPlayerBackground.SetActive(false);
        choosingPlayerText.text = "";
        player2Button.transform.position = new Vector3(50000, 0, 0);
        player3Button.transform.position = new Vector3(50000, 0, 0);
        player4Button.transform.position = new Vector3(50000, 0, 0);

        var random = new System.Random();
        Vector3 nobledeckPosition = nobleTiles[9].transform.position;

        // define range
        int min = 0;
        int max = 10;



        // create list of numbers in range
        var numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        Vector3[] nobleTilePositions;


        var result = numbers.Take(4).ToList();
        nobleTilePositions = new Vector3[] { nobleTiles[0].transform.position, nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position };
        nobleTiles[4].transform.position = nobledeckPosition;

        int j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            nobleTiles[num].transform.position = nobleTilePositions[j];

            //switching in list
            activeNobleTiles.Add(nobleTiles[num]);
            j++;
        }
        int k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                nobleTiles[k].transform.position = nobledeckPosition;
            }

            k++;
        }

        goldCoinNum = 5;
        brownCoinNum = 5;
        redCoinNum = 5;
        greenCoinNum = 5;
        blueCoinNum = 5;
        diamondCoinNum = 5;

    }
    void button4Pressed()
    {
        numOfPlayers = 4;
        choosingPlayerBackground.SetActive(false);
        choosingPlayerText.text = "";
        player2Button.transform.position = new Vector3(50000, 0, 0);
        player3Button.transform.position = new Vector3(50000, 0, 0);
        player4Button.transform.position = new Vector3(50000, 0, 0);

        var random = new System.Random();
        Vector3 nobledeckPosition = nobleTiles[9].transform.position;

        // define range
        int min = 0;
        int max = 10;



        // create list of numbers in range
        var numbers = Enumerable.Range(min, max - min).ToList();

        // shuffle
        numbers = numbers.OrderBy(x => random.Next()).ToList();
        Vector3[] nobleTilePositions;


        var result = numbers.Take(5).ToList();
        nobleTilePositions = new Vector3[] { nobleTiles[0].transform.position, nobleTiles[1].transform.position, nobleTiles[2].transform.position, nobleTiles[3].transform.position, nobleTiles[4].transform.position };

        int j = 0;

        foreach (var num in result)
        {
            Debug.Log(num);
            //greenCards[j].transform.position = greendeckPosition;
            nobleTiles[num].transform.position = nobleTilePositions[j];

            //switching in list
            activeNobleTiles.Add(nobleTiles[num]);
            j++;
        }
        int k = 0;
        foreach (var num in result)
        {
            if (!result.Contains(k))
            {
                nobleTiles[k].transform.position = nobledeckPosition;
            }

            k++;
        }
        goldCoinNum = 5;
        brownCoinNum = 7;
        redCoinNum = 7;
        greenCoinNum = 7;
        blueCoinNum = 7;
        diamondCoinNum = 7;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
