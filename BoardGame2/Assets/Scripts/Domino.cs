using UnityEngine;

public class Domino : MonoBehaviour
{

    public bool isPressed = false;
    public int currturn;
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
        isPressed = true;
        Debug.Log(isPressed);
    }
    public bool getIsPressed()
    {
        return isPressed;
    }

    
}
