using System.Collections.Generic;
using UnityEngine;

public enum Swipe { None, Up, Down, Left, Right };
public class SeaManager : MonoBehaviour
{
    public float minSwipeLength = 5f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Vector2 firstClickPos;
    Vector2 secondClickPos;

    public int waveCharge=0;
    public GameManager GM;
    public static Swipe swipeDirection;

    public GameObject waveSprite;


    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    public Transform positionIniWave;
    public List<Transform> range;
    void Start()
    {
        startTime = Time.time;
        positionIniWave.position = waveSprite.transform.position;

        journeyLength = Vector3.Distance(waveSprite.transform.position, range[0].position);
    }
    void Update()
    {
        waveCharge=GM.waveCharge;
        DetectSwipe();

    }

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)//detecta dedo
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Up;
                    // Swipe down
                    if (waveCharge == 0)
                    {
                        Debug.Log("NO PUEDEH HACER NADA PARGUELA");


                        //not charged, do nothing
                    }
                    else if (waveCharge == 1)
                    {

                        //Clean first row
                        //float distCovered = (Time.time - startTime) * speed;
                        //float fracJourney = distCovered / journeyLength;
                        //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[0].position, fracJourney);
                        waveSprite.GetComponent<Animator>().SetTrigger("wave1");
                        GM.Wave(waveCharge);

                        GM.reinitiateClicksWave();
                    }
                    else if (waveCharge == 2)
                    {
                        //Clean second round
                        //float distCovered = (Time.time - startTime) * speed;
                        //float fracJourney = distCovered / journeyLength;
                        //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[1].position, fracJourney);
                        waveSprite.GetComponent<Animator>().SetTrigger("wave2");
                        GM.Wave(waveCharge);

                        GM.reinitiateClicksWave();
                    }
                    else if (waveCharge == 3)
                    {
                        //Clean third row
                        //float distCovered = (Time.time - startTime) * speed;
                        //float fracJourney = distCovered / journeyLength;
                        //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[2].position, fracJourney);
                        waveSprite.GetComponent<Animator>().SetTrigger("wave3");
                        GM.Wave(waveCharge);

                        GM.reinitiateClicksWave();
                    }
                    else if (waveCharge == 4)
                    {
                        //Clean fourth row
                        //float distCovered = (Time.time - startTime) * speed;
                        //float fracJourney = distCovered / journeyLength;
                        //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[3].position, fracJourney);
                        waveSprite.GetComponent<Animator>().SetTrigger("wave4");
                        GM.Wave(waveCharge);

                        GM.reinitiateClicksWave();
                    }
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Down;
                    // Swipe left
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Left;
                    // Swipe right
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Right;
                }
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else
            {
                swipeDirection = Swipe.None;
                //Debug.Log ("None");
            }
            if (Input.GetMouseButtonUp(0))
            {
                secondClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector3(secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                //Swipe directional check
                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Up;
                    Debug.Log("Up");

                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        swipeDirection = Swipe.Up;
                        // Swipe down
                        if (waveCharge == 0)
                        {
                            Debug.Log("NO PUEDEH HACER NADA PARGUELA");


                            //not charged, do nothing
                        }
                        else if (waveCharge == 1)
                        {

                            //Clean first row
                            //float distCovered = (Time.time - startTime) * speed;
                            //float fracJourney = distCovered / journeyLength;
                            //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[0].position, fracJourney);
                            waveSprite.GetComponent<Animator>().SetTrigger("wave1");
                            GM.Wave(waveCharge);

                            GM.reinitiateClicksWave();
                        }
                        else if (waveCharge == 2)
                        {
                            //Clean second round
                            //float distCovered = (Time.time - startTime) * speed;
                            //float fracJourney = distCovered / journeyLength;
                            //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[1].position, fracJourney);
                            waveSprite.GetComponent<Animator>().SetTrigger("wave2");
                            GM.Wave(waveCharge);

                            GM.reinitiateClicksWave();
                        }
                        else if (waveCharge == 3)
                        {
                            //Clean third row
                            //float distCovered = (Time.time - startTime) * speed;
                            //float fracJourney = distCovered / journeyLength;
                            //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[2].position, fracJourney);
                            waveSprite.GetComponent<Animator>().SetTrigger("wave3");
                            GM.Wave(waveCharge);

                            GM.reinitiateClicksWave();
                        }
                        else if (waveCharge == 4)
                        {
                            //Clean fourth row
                            //float distCovered = (Time.time - startTime) * speed;
                            //float fracJourney = distCovered / journeyLength;
                            //waveSprite.transform.position = Vector3.Lerp(positionIniWave.position, range[3].position, fracJourney);
                            waveSprite.GetComponent<Animator>().SetTrigger("wave4");
                            GM.Wave(waveCharge);

                            GM.reinitiateClicksWave();
                        }
                    }
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    //swipeDirection = Swipe.Down;
                    //Debug.Log("Down");

                    // Swipe left
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //swipeDirection = Swipe.Left;
                    //Debug.Log("Left");
                    // Swipe right
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //swipeDirection = Swipe.Right;
                    //Debug.Log("right");
                }
            }

        }
    }
}