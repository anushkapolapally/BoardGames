using UnityEngine;
using UnityEngine.EventSystems;

public class IconScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject gameManager;
    public int correspondingPlayer;


  
    public void OnPointerEnter(PointerEventData eventData)
    {
        int numOfPlayer = gameManager.GetComponent<GameManager>().numOfPlayers;
        int turn = gameManager.GetComponent<GameManager>().turn ;

        for (int i = 0; i < numOfPlayer; i++)
        {
            
                gameManager.GetComponent<GameManager>().playerPrefabs[i].transform.position = new Vector3(gameManager.GetComponent<GameManager>().playerPrefabs[i].transform.position.x + 15, gameManager.GetComponent<GameManager>().playerPrefabs[i].transform.position.y, gameManager.GetComponent<GameManager>().playerPrefabs[i].transform.position.z);
            
        }

        
        Debug.Log("HOVER START");
        // do your hover effect here

    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HOVER END");

        
            gameManager.GetComponent<GameManager>().playerPrefabs[0].transform.position = new Vector3(-16.775f, 2.9325f, -3f);
        
       
            gameManager.GetComponent<GameManager>().playerPrefabs[1].transform.position = new Vector3(-16.7752f, 1.29f, 0f);
        
         gameManager.GetComponent<GameManager>().playerPrefabs[2].transform.position = new Vector3(-16.7752f, -0.4f, 0f);
        
            gameManager.GetComponent<GameManager>().playerPrefabs[3].transform.position = new Vector3(-16.7752f, -2.15f, 0f);

        
        // undo hover effect here
    }

    
}