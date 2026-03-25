using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardPiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public int value;
    public bool placedOnMainBoard = false;

    public GameManager gameManager;
    public bool pressed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        pressed = true;
        gameManager.pieceClicked = true;
        Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        pressed = false;
        gameManager.pieceClicked = false;
        Debug.Log(name + "No longer being clicked");
    }


}
