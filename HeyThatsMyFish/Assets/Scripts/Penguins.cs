using UnityEngine;

public class Penguins : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject gameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        if (gameManager == null) return;

        if (gameManager.turn >= 0)
        {
            Debug.Log("clicked on penguin");
            gameManager.SelectPengiun(gameObject);
           
        }
    }
}
