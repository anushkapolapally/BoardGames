using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardPiece : MonoBehaviour
    //, IPointerDownHandler, IPointerUpHandler
{

    public int value;
    public bool placedOnMainBoard = false;

    public GameManager gameManager;
    public bool pressed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("testing how many times this script is called");
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Click registered once!");

            gameManager.pressedPiece = gameObject;
            pressed = true;
            gameManager.pieceClicked = true;
            
        



            //StartCoroutine(WaitAndDoSomething());

            //gameObject.SetActive(false);
       
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pressed = false;
            gameManager.pieceClicked = false;
            Debug.Log(name + "No longer being clicked");
        } */
    }



    IEnumerator WaitAndDoSomething()
{
    Debug.Log("Before wait: " + Time.time);

    yield return new WaitForSeconds(5f); //Test diff times


    Debug.Log("After wait: " + Time.time);
}
    /*    public void OnPointerDown(PointerEventData pointerEventData)
        {
            pressed = true;
            gameManager.pieceClicked = true;
            gameObject.SetActive(false);
            Debug.Log(name + "Game Object Click in Progress");
        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            pressed = false;
            gameManager.pieceClicked = false;
            Debug.Log(name + "No longer being clicked");
        }
    */

    private void OnMouseDown()
    {
        if (gameManager == null) return;

        // gameManager.pressedPiece = gameObject;
        //gameManager.pieceClicked = true;

        gameManager.SelectBoardPiece(gameObject);
        Debug.Log(name + " clicked");
    }

}
