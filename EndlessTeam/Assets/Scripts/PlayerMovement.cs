using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    [SerializeField]
    float movSpeed;

    //0 Left, 1 Mid, 2 Right
    [SerializeField]
    Transform[] positions;

    Vector2 startPos, swipeDelta;

    bool isTouching, canSwipe;
    enum Swipe
    {
        Null, Left, Right, Up, Down, Mid
    }

    string currentState = "Mid";

    Swipe swipeEn;

    private void Start()
    {
        swipeEn = Swipe.Null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                canSwipe = true;
                isTouching = true;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Reset();
                isTouching = false;
            }

            if (isTouching)
            {
                swipeDelta = touch.position - startPos;
            }
        }

        if (swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            //Destra
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                    swipeEn = Swipe.Right;
                else swipeEn = Swipe.Left;
            }
            else
            {
                if (y > 0)
                    swipeEn = Swipe.Up;
                else swipeEn = Swipe.Down;
            }


            if (canSwipe)
            {
                StopCoroutine("ChangingPosition");
                StartCoroutine(ChangingPosition(swipeEn.ToString()));
                canSwipe = false;
            }    
        }

        controller.Move(transform.forward * movSpeed * Time.deltaTime);
    }

    private void Reset()
    {
        startPos = swipeDelta = Vector2.zero;
    }

    IEnumerator ChangingPosition(string target)
    {
        Vector3 dest = transform.position;
        //Calcolo della posizione finale
        Vector3 finalPos = default;
        if (target == "Left")
        {
            if (currentState == "Right")
            {
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            else
            {
                finalPos = positions[0].position;
                currentState = "Left";
            }
        }
        else if (target == "Right")
        {
            if (currentState == "Left")
            {
                finalPos = positions[1].position;
                currentState = "Mid";
            }
            else
            {
                finalPos = positions[2].position;
                currentState = "Right";
            }
        }
        
        finalPos.y = finalPos.z = 0;



        while (transform.position.x != finalPos.x)
        {
            
            dest.x = Mathf.Lerp(dest.x, finalPos.x, .2f);
            dest.y = transform.position.y;
            dest.z = transform.position.z;

            if (Mathf.Abs(dest.x - finalPos.x) < .2f)
                dest.x = finalPos.x;

            transform.position = dest;


            yield return null;
        }

    }
}
