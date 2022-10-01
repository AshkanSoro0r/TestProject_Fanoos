using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion
    
    public Pins pins;
    public GameObject ball;     //ball prefab
    public Transform SpawnPos;
    public List<GameObject> ModList = new List<GameObject>();
    [HideInInspector]public List<GameObject> balls = new List<GameObject>();
    [HideInInspector]public Vector3 enterPoint;
    [HideInInspector]public Vector3 exitPoint;
    [HideInInspector]public bool reInstantiat;
    private bool rightIsActive;
    private bool leftIsActive;
    private GameObject ballInit;
    private int modNum = 0;

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        //left pin
        if (leftIsActive)
        {
            //rotate
            pins.pinLeft.GetComponent<Rigidbody2D>().AddTorque(100f     , ForceMode2D.Force);
        }
        else
        {
            pins.pinLeft.GetComponent<Rigidbody2D>().AddTorque(-100 , ForceMode2D.Force);
        }
        
        //right pin
        if (rightIsActive)
        {
            //rotate
            pins.pinRight.GetComponent<Rigidbody2D>().AddTorque(-100f , ForceMode2D.Force);
        }
        else
        {
            pins.pinRight.GetComponent<Rigidbody2D>().AddTorque(100f , ForceMode2D.Force);
        }
        
        
        
        //re spawn after missing ball
        if (reInstantiat)
        {
            //StartCoroutine(RunFunc_withDelay(StartGame))
            //StartGame();
            reInstantiat = false;
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].GetComponent<BallScript>().missed)
                {
                    balls[i].transform.position = SpawnPos.position;
                    balls[i].GetComponent<BallScript>().missed = false;
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    balls[i].GetComponent<Rigidbody2D>().AddForce(50 * randomDirection , ForceMode2D.Force);
                }
            }


        }
        
    }

    public void PinLeft_Activator()
    {
        leftIsActive = true;
    }
    public void PinRight_Activator()
    {
        rightIsActive = true;
    }
    public void PinLeft_Deactivator()
    {
        leftIsActive = false;
    }
    public void PinRight_Deactivator()
    {
        rightIsActive = false;
    }

    public void StartGame()
    {
        reInstantiat = false;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        ballInit = Instantiate(ball, SpawnPos.position, quaternion.identity);
        ballInit.GetComponent<Rigidbody2D>().AddForce(50 * randomDirection , ForceMode2D.Force);
        balls.Add(ballInit);

    }

    IEnumerator RunFunc_withDelay(Action func)
    {
        yield return new WaitForSeconds(2f);
        func();
    }

    public void changeMode()
    {
        if (modNum < 3)
            modNum++;
        else
            modNum = 0;


        for (int i = 0; i < ModList.Count; i++)
        {
            if (i == modNum)
            {
                ModList[i].SetActive(true);
            }
            else
            {
                ModList[i].SetActive(false);
            }
        }

    }
}



[System.Serializable]
public class Pins
{
    public Rigidbody2D pinRight;
    public Rigidbody2D pinLeft;
    public PhysicsMaterial2D pinRightBounc;
    public PhysicsMaterial2D pinLeftBounc;
}
