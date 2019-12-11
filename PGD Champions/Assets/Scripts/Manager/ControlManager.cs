using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager controlManager = null;

    private float firstTouch = 0;
    private float minHold = 0;

    public float turnHold = 0;
    public float breakHold = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(controlManager == null)
        {
            controlManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }

    private void ControlMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (firstTouch == 0)
            {
                firstTouch = touchPos.x;
            }
            else
            {
                float secondTouch = touchPos.x;
                if (secondTouch > firstTouch && turnHold <= 2)
                {
                    turnHold += 2 * Time.deltaTime;
                }
                else if (secondTouch < firstTouch && turnHold >= -2)
                {
                    turnHold -= 2 * Time.deltaTime;
                }
                else if (firstTouch == secondTouch)
                {
                    if (minHold > 0.1f && breakHold <= 5)
                    {
                        breakHold += 3 * Time.deltaTime;
                    }
                }
            }
            minHold += 1f * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            firstTouch = 0;
            minHold = 0;
            turnHold = 0;
            breakHold = 0;
        }
    }
}
