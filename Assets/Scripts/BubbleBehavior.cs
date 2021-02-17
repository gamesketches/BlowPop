using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circCol;

    public float shake_speed;
    public float shake_intensity;
    Vector3 startPos;
    HingeJoint2D myJoint;
    public bool haveCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blower")
        {

        }

        if (collision.gameObject.tag == "Bubble")
        {
             
          
            if (!haveCollided)
            {
                myJoint = gameObject.AddComponent<HingeJoint2D>();
                myJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                myJoint.anchor = new Vector2(0, 0);
                myJoint.connectedAnchor = gameObject.GetComponent<CircleCollider2D>().ClosestPoint(myJoint.connectedBody.transform.position);
                //myJoint.useMotor = true;
                //JointMotor2D myMotor = myJoint.motor;
                //myMotor.motorSpeed = Random.Range(-10.0f, 10.0f);
                //myJoint.motor = myMotor;
               // myJoint.autoConfigureConnectedAnchor = false;
                haveCollided = true;
            }
           

        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        circCol = gameObject.GetComponent<CircleCollider2D>();
        circCol.radius = Random.Range(.09f, .11f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (haveCollided)
        //{
        //    Debug.DrawLine(gameObject.transform.position, gameObject.GetComponent<CircleCollider2D>().ClosestPoint(myJoint.connectedBody.transform.position));
        //}


        if (haveCollided)
        {
            Wobble();
        }
          
    }

    void Wobble() {
            rb.AddForce(Vector3.right * Random.Range(-shake_speed, shake_speed));
            rb.AddForce(Vector3.up * Random.Range(-shake_speed, shake_speed));
            transform.Rotate(0, 0, shake_intensity);
    }
}
