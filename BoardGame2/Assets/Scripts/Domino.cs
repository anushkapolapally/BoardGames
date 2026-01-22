using UnityEngine;
public class Domino : MonoBehaviour
{


   [SerializeField] GameManager gameManager;

    //0 = straight up, 1== 90 deg.clockwise, 2 = 180 clockwise, 3 = 270 clockwise
    public int orientation;
  //  [SerializeField] GameObject gameObject;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

      

        orientation = 0;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //FIX THESE ANGLES LATER
        if (orientation == 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 90, 0);
        }
        else if (orientation == 1)
        {
            gameObject.transform.rotation = new Quaternion(0, 270, 90, 0);
        }
        else if (orientation == 2)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 90, 0);
        }
        else if (orientation == 3)
        {
            gameObject.transform.rotation = new Quaternion(0, 90, 90, 0);
        }
        */

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
