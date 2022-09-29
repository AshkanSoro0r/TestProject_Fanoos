using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.reInstantiat = true;
            Destroy(gameObject);    // must use pulling system for better performance
            BlackHole.instance.ball = null;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("booster"))
        {
            GameManager.instance.enterPoint = gameObject.transform.position;
        }
        
        if (other.CompareTag("booster2"))
        {
            GameManager.instance.enterPoint = gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //add ball power up
        if (other.CompareTag("booster"))
        {
            GameManager.instance.exitPoint = gameObject.transform.position;
            
            
            GameObject newBall;
            newBall = Instantiate(GameManager.instance.ball, other.transform.position, quaternion.identity);

            Vector2 direction = ( GameManager.instance.exitPoint - GameManager.instance.enterPoint);
            direction = new Vector2(direction.x, direction.y + 0.8f);
            newBall.GetComponent<Rigidbody2D>().AddForce(direction.normalized  * ((gameObject.GetComponent<Rigidbody2D>().velocity.magnitude) / 10),ForceMode2D.Impulse);
            //booster cooldown
            StartCoroutine(BoosterCooldown());
        }
        
        
        //add speed power up
        if (other.CompareTag("booster2"))
        {
            GameManager.instance.exitPoint = gameObject.transform.position;
            

            Vector2 direction = ( GameManager.instance.exitPoint - GameManager.instance.enterPoint);
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized  * ((gameObject.GetComponent<Rigidbody2D>().velocity.magnitude) / 5),ForceMode2D.Impulse);
            
            //booster cooldown
            StartCoroutine(BoosterCooldown());
        }

        IEnumerator BoosterCooldown()
        {
            other.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);
            other.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
