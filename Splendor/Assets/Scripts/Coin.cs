using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerClickHandler
{
    public string color;
    public GameObject gameManager;
    public bool validPiece;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Coin pressed via EventSystem!");

        //yellow coin click
        if (color == "yellow")
        {
            gameManager.GetComponent<GameManager>().clickedData[0] += 1;
        }



        //brown coin click
        if (color == "brown")
        {
            gameManager.GetComponent<GameManager>().clickedData[1] += 1;
        }

        //red coin click
        if (color == "red")
        {
            gameManager.GetComponent<GameManager>().clickedData[2] += 1;
        }

        //green coin click
        if (color == "green")
        {
            gameManager.GetComponent<GameManager>().clickedData[3] += 1;
        }

        //blue coin click
        if (color == "blue")
        {
            gameManager.GetComponent<GameManager>().clickedData[4] += 1;
        }

        //diamond coin click
        if (color == "diamond")
        {
            gameManager.GetComponent<GameManager>().clickedData[5] += 1;
        }



    }
}