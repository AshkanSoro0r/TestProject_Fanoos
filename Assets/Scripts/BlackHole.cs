using UnityEngine;

public class BlackHole : MonoBehaviour
{
    #region Singleton

    public static BlackHole instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion


    public Transform ball;     //ball that init in game
    private Rigidbody2D ballRb;
    private float distanceToBall;   //distance between ball and blackhole
    private Vector2 pullForce;

    public float influenceRange;    // gravity range
    public float intensity;     //gravity power  
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("getBall",0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball != null)
        {
            distanceToBall = Vector2.Distance(ball.position, transform.position);
            if (distanceToBall <= influenceRange)
            {
                pullForce = (transform.position - ball.position).normalized / distanceToBall * intensity;
                ballRb.AddForce(pullForce, ForceMode2D.Force);
            }
        }
        else
        {
           // ball = GameObject.FindWithTag("Player").transform;
        }
    }

    private void getBall()
    {
        ball = GameObject.FindWithTag("Player").transform;
        ballRb = ball.GetComponent<Rigidbody2D>();
    }
    
    
}
