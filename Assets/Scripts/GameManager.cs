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
        
    [HideInInspector]public Vector3 enterPoint;
    [HideInInspector]public Vector3 exitPoint;
    [HideInInspector]public bool reInstantiat;

    private bool rightIsActive;
    private bool leftIsActive;
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
            pins.pinLeft.GetComponent<Rigidbody2D>().AddTorque(25f , ForceMode2D.Force);
        }
        else
        {
            pins.pinLeft.GetComponent<Rigidbody2D>().AddTorque(-25 , ForceMode2D.Force);
        }
        
        //right pin
        if (rightIsActive)
        {
            //rotate
            pins.pinRight.GetComponent<Rigidbody2D>().AddTorque(-25f , ForceMode2D.Force);
        }
        else
        {
            pins.pinRight.GetComponent<Rigidbody2D>().AddTorque(25f , ForceMode2D.Force);
        }
        
        
        
        //re spawn after missing ball
        if (reInstantiat)
        {
            //StartCoroutine(RunFunc_withDelay(StartGame));
            StartGame();
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
        GameObject ballInit;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        ballInit = Instantiate(ball, SpawnPos.position, quaternion.identity);
        ballInit.GetComponent<Rigidbody2D>().AddForce(50 * randomDirection , ForceMode2D.Force);
    }

    IEnumerator RunFunc_withDelay(Action func)
    {
        yield return new WaitForSeconds(2f);
        func();
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
