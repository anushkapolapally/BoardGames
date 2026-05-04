using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerClickHandler
{
    public string color;
    public GameObject gameManager;
    public bool validPiece;

    public void OnPointerClick(PointerEventData eventData)
    {
        int turn = gameManager.GetComponent<GameManager>().turn;
        int numOfPlayers = gameManager.GetComponent<GameManager>().numOfPlayers;
        Debug.Log("Coin pressed via EventSystem!");

        //yellow coin click
        if (color == "yellow")
        {
            gameManager.GetComponent<GameManager>().clickedData[0] += 1;
            gameManager.GetComponent<GameManager>().goldCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][0] += 1;
        }



        //brown coin click
        if (color == "brown")
        {
            gameManager.GetComponent<GameManager>().clickedData[1] += 1;
            gameManager.GetComponent<GameManager>().brownCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][2] += 1;


        }

        //red coin click
        if (color == "red")
        {
            gameManager.GetComponent<GameManager>().clickedData[2] += 1;
            gameManager.GetComponent<GameManager>().redCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][4] += 1;


        }

        //green coin click
        if (color == "green")
        {
            gameManager.GetComponent<GameManager>().clickedData[3] += 1;
            gameManager.GetComponent<GameManager>().greenCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][6] += 1;


        }

        //blue coin click
        if (color == "blue")
        {
            gameManager.GetComponent<GameManager>().clickedData[4] += 1;
            gameManager.GetComponent<GameManager>().blueCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][8] += 1;


        }

        //diamond coin click
        if (color == "diamond")
        {
            gameManager.GetComponent<GameManager>().clickedData[5] += 1;
            gameManager.GetComponent<GameManager>().diamondCoinNum -= 1;
            gameManager.GetComponent<GameManager>().playerCoinsData[turn % numOfPlayers][10] += 1;


        }



    }
}