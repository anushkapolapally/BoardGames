using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosingPlayersScript : MonoBehaviour
{
    
    [SerializeField] Button player2Button;
    [SerializeField] Button player3Button;
    [SerializeField] Button player4Button;

    [SerializeField] GameObject gameManager;

    public int numPlayers = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player2Button.onClick.AddListener(button2Pressed);
        player3Button.onClick.AddListener(button3Pressed);
        player4Button.onClick.AddListener(button4Pressed);

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void button2Pressed()
    {

        numPlayers = 2;
        gameManager.GetComponent<GameManager>().numOfPlayers = numPlayers;


        SceneManager.LoadScene("GamePlay");
    }
    void button3Pressed()
    {
        numPlayers = 3;
        gameManager.GetComponent<GameManager>().numOfPlayers = numPlayers;

        SceneManager.LoadScene("GamePlay");
    }
    void button4Pressed()
    {
        numPlayers = 4;
        gameManager.GetComponent<GameManager>().numOfPlayers = numPlayers;

        SceneManager.LoadScene("GamePlay");
    }
}
