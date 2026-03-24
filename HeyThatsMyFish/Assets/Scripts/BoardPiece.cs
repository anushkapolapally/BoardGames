using Unity.VisualScripting;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{

    public int value;
    public bool placedOnMainBoard = false;

    public bool pressed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        pressed = true;
        Debug.Log("piece pressed");
    }


}
