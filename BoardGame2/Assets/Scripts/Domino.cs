using UnityEngine;
public class Domino : MonoBehaviour
{


    [SerializeField] GameManager gameManager;

    public int horizontal;
    [SerializeField] GameObject gameObject;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

      

        horizontal = 0;

        // Mesh mesh = gameObject.GetComponent<Mesh>();

        Debug.Log("Turn off mesh");
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        
        Mesh mesh = meshRenderer.GetComponent<Mesh>();

        mesh.vertices = new Vector3[4];

       

        
        
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
