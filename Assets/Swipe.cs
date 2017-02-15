using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour
{


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    public GameObject menu;

    public float menuStartPos = -500f;
    public float menuTargetPos = 200f;
    bool menuMov = false;
    bool menuClick = false;
    Vector3 targetPos;
    Vector3 refVelocity;


    // Update is called once per frame

    private void Start()
    {
        menu = GameObject.Find("Canvas1/Menu");
    }

    void Update()
    {
        if (menuMov)
            MoveMenu();

        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    //MOVE RIGHT
                                    targetPos = new Vector3(menuTargetPos, menu.transform.position.y, menu.transform.position.z);
                                    menuClick = true;
                                    menuMov = true;
                                }
                                else
                                {
                                    //MOVE LEFT
                                    targetPos = new Vector3(menuStartPos, menu.transform.position.y, menu.transform.position.z);
                                    menuClick = false;
                                    menuMov = true;
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    // MOVE UP
                                }
                                else
                                {
                                    // MOVE DOWN
                                }
                            }

                        }

                        break;
                }
            }
        }

    }
    void MoveMenu()
    {
        menu.transform.position = Vector3.SmoothDamp(menu.transform.position, targetPos, ref refVelocity, Time.deltaTime * 10);

        if (targetPos == transform.position)
        {
            //selectedPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            menuMov = false;
        }

    }
    public void MenuClicked()
    {
        if (menuClick)
        {
            menuClick = false;
            targetPos = new Vector3(menuStartPos, menu.transform.position.y, menu.transform.position.z);
            menuMov = true;
        }
        else
        {
            menuClick = true;
            targetPos = new Vector3(menuTargetPos, menu.transform.position.y, menu.transform.position.z);
            menuMov = true;
        }
    }
}
