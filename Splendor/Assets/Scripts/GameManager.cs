using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numOfPlayers;
    public List<GameObject> greenCards = new List<GameObject>();
    public List<GameObject> yellowCards = new List<GameObject>();
    public List<GameObject> blueCards = new List<GameObject>();
    public List<GameObject> nobleTiles = new List<GameObject>();

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

    

    void Start()
    {
        //initalizing coins
        if(numOfPlayers == 2)
        {
            brownCoinNum = 4;
            redCoinNum = 4;
            greenCoinNum = 4;
            blueCoinNum = 4;
            diamondCoinNum = 4;
        }
        else if(numOfPlayers == 3) 
        {
            brownCoinNum = 5;
            redCoinNum = 5;
            greenCoinNum = 5;
            blueCoinNum = 5;
            diamondCoinNum = 5;
        }

        goldCoinText = GameObject.Find("YellowCoinText").GetComponent<Text>();

    }

    void Update()
    {
        //coin text
        goldCoinText.text = goldCoinNum.ToString();
        brownCoinText.text = brownCoinNum.ToString();
        redCoinText.text = redCoinNum.ToString();
        greenCoinText.text = greenCoinNum.ToString();
        blueCoinText.text = blueCoinNum.ToString();
        diamondCoinText.text = diamondCoinNum.ToString();






    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
