using UnityEngine;

public class Domino : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    
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
        if (gameManager.GetComponent<GameManager>().getTurn() == 0 || gameManager.GetComponent<GameManager>().getTurn() == 1)
        {
            gameManager.GetComponent<GameManager>().setIsPressed();
            Debug.Log("isPressed");
        }
        
        


    }
    

    
}
