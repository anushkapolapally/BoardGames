using UnityEngine;


public class Ship : MonoBehaviour
{

    public bool clicked = false;

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
        
        if (clicked == true)
        {
            clicked = false;
        }
        else if (clicked == false)
        {

            clicked = true;
        }
    }

    public bool getClicked()
    {
        return clicked;
    }
    public void setClicked(bool v)
    {
        clicked = v;
    }
}
