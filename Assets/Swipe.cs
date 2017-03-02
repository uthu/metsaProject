using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    public GameObject menu;
    public GameObject info;

    public float menuStartPos = -500f;
    public float menuTargetPos = 200f;

    public float infoStartPos = 0f;
    public float infoTargetPos = 0f;
    bool menuMov = false;
    public bool menuClick = false;
    Vector3 targetPos;
    Vector3 infotargetPos;
    Vector3 refVelocity;

    Image starImage2;


    private void Start()
    {
        menu = GameObject.Find("Canvas1/Menu");
        info = GameObject.Find("Canvas1/Info");

        starImage2 = GameObject.Find("Canvas1/Menu/HealthInfo/star2").GetComponent<Image>();

        infoStartPos = info.transform.position.x;
        menuStartPos = menu.transform.position.x;
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
                                    infotargetPos = new Vector3(infoTargetPos, info.transform.position.y, info.transform.position.z);
                                    menuClick = true;
                                    menuMov = true;
                                    starImage2.enabled = false;
                                }
                                else
                                {
                                    //MOVE LEFT
                                    targetPos = new Vector3(menuStartPos, menu.transform.position.y, menu.transform.position.z);
                                    infotargetPos = new Vector3(infoStartPos, info.transform.position.y, info.transform.position.z);
                                    menuClick = false;
                                    menuMov = true;

                                    starImage2.enabled = false;
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
        info.transform.position = Vector3.Lerp(info.transform.position, infotargetPos, Time.deltaTime * 10);

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
            infotargetPos = new Vector3(infoStartPos, info.transform.position.y, info.transform.position.z);
            menuMov = true;

        }
        else
        {
            menuClick = true;
            targetPos = new Vector3(menuTargetPos, menu.transform.position.y, menu.transform.position.z);
            infotargetPos = new Vector3(infoTargetPos, info.transform.position.y, info.transform.position.z);
            menuMov = true;

            starImage2.enabled = false;
        }
    }
}
