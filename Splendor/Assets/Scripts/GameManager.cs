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
    private void refreshingPrefabs()
    {
        if (numOfPlayers == 2)
        {
            playerPrefabs[0].transform.Find("Canvas/YellowCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/YellowBonus").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/BrownCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/BrownBonus").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/RedCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/RedBonus").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/GreenCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/GreenBonus").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/BlueCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/BlueBonus").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/DiamondCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/DiamondBonus").GetComponent<TextMeshProUGUI>().text = "T";


            playerPrefabs[0].transform.Find("Canvas/TotalCoins").GetComponent<TextMeshProUGUI>().text = "T";
            playerPrefabs[0].transform.Find("Canvas/TotalPoints").GetComponent<TextMeshProUGUI>().text = "T";


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
