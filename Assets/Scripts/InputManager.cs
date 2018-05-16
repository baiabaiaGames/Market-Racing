using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private const float deadZone = 100.0f;

    public static InputManager Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, turnLeft, turnRight;
    private Vector2 swipeDelta, startTouch;
    private Vector3 acceleration;

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool TurnLeft { get { return turnLeft; } }
    public bool TurnRight { get { return turnRight; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeDown = swipeUp = false;

        //Inputs

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }        
        #endregion

        //Calcula distance
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Let's chech if we are beyond the deadzone
        if (swipeDelta.magnitude > deadZone)
        {
            //This is a confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
