using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public SnakeController SnakeControllerScript;
    private float halfScreenWidth;
    public FixedTouchField_Android FixedTouchFieldScript;

    private void Start()
    {
        halfScreenWidth = Screen.width / 2;
    }
    private void Update()
    {
        NewTouchController();
    }

    private void OldTouchController()
    {
        
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x > halfScreenWidth)
            {
                SnakeControllerScript.TouchDirection = 1;
            }

            if (touch.position.x < halfScreenWidth)
            {
                SnakeControllerScript.TouchDirection = -1;
            }
        }
        else
        {
            SnakeControllerScript.TouchDirection = 0;
        }
    }

    public void NewTouchController()
    {
        if (FixedTouchFieldScript.XPos > halfScreenWidth)
        {
            SnakeControllerScript.TouchDirection = 1;
        }
        else if(FixedTouchFieldScript.XPos == 0)
        {
            SnakeControllerScript.TouchDirection = 0;
        }
        else if (FixedTouchFieldScript.XPos < halfScreenWidth)
        {
            SnakeControllerScript.TouchDirection = -1;
        }
    }
}
