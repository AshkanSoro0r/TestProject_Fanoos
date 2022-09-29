using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlackHoleCenter : MonoBehaviour
{
    public float waitTime;
    
    private bool stuck;
    private Rigidbody2D StuckedBall;
    
    void Update()
    {
        if (stuck)
        {
            StuckedBall.bodyType = RigidbodyType2D.Static;
            StuckedBall.transform.position = Vector3.MoveTowards(StuckedBall.transform.position, transform.position, 0.2f * Time.deltaTime);
            if (StuckedBall.transform.position == transform.position)
            {
                StuckedBall.velocity = Vector2.zero;
                Vector2 rndDirection = (Random.insideUnitCircle).normalized;
                StartCoroutine(ShootBall(rndDirection));
                stuck = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stuck = true;
            StuckedBall = other.gameObject.GetComponent<Rigidbody2D>();

        }
    }
    
    //shoot ball to random pos 
    IEnumerator ShootBall(Vector2 dir)
    {
        transform.parent.GetComponent<BlackHole>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        StuckedBall.bodyType = RigidbodyType2D.Dynamic;
        StuckedBall.AddForce(dir * 1.5f ,ForceMode2D.Impulse); 
        
        yield return new WaitForSeconds(1f);    //cool down for black hole 1 second
        
        transform.parent.GetComponent<BlackHole>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }
}
