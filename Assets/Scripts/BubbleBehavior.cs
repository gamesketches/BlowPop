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
    bool haveCollided;

    BubbleBehavior myConnectedBubble;
    bool beenPopped;
    public GameObject residue;
    public GameObject myTape;
    public bool beenBlown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Toothbrush")
        {
            if (transform.parent != null)
            {
                PopMyself();
                BubbleResidueOnPop(collision.transform.position);
                transform.parent.GetComponent<BlowerBehavior>().EarlyPopBubble();
            }
            else
            {
                PopMyself();
                BubbleResidueOnPop(collision.transform.position);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blower")
        {
            if (collision.gameObject.transform != gameObject.transform.parent)
            {
                if (beenBlown)
                {
                    collision.gameObject.GetComponent<BlowerBehavior>().AttachResidue(transform.localScale.x);
                    PopMyself();
                    BubbleResidueOnPop(collision.transform.position);
                }
                else
                {
                    if (collision.gameObject.transform != gameObject.transform.parent)
                    {
                        PopMyself();
                        BubbleResidueOnPlayer(collision.transform.position, collision.transform);
                        transform.parent.GetComponent<BlowerBehavior>().EarlyPopBubble();
                    }

                }
            }
            
        }

        if (collision.gameObject.tag == "Tooth")
        {



            if (transform.parent != null)
            {
                PopMyself();
                BubbleResidueOnPop(collision.transform.position);
                transform.parent.GetComponent<BlowerBehavior>().EarlyPopBubble();
            }
            else {
                PopMyself();
                BubbleResidueOnPop(collision.transform.position);
            }
             
        }


        if (collision.gameObject.tag == "Poppers")
        {
            PopMyself();
        }

        if (collision.gameObject.tag == "Bubble")
        {
         

            if (!haveCollided)
            {
                //if (Mathf.Abs(collision.gameObject.transform.localScale.x - transform.localScale.x) < 1.5f)
                if(collision.gameObject.transform.localScale.x > transform.localScale.x)
                {
                    Vector3 closestCollPoint = collision.contacts[0].point;
                    GameObject tempTape = Instantiate(myTape, new Vector3(closestCollPoint.x, closestCollPoint.y), Quaternion.identity);
                    tempTape.transform.localScale = new Vector3(gameObject.transform.localScale.x * .76f, gameObject.transform.localScale.y * .5f, gameObject.transform.localScale.z * .2f);
                    tempTape.transform.parent = gameObject.transform;
                }
                myJoint = gameObject.AddComponent<HingeJoint2D>();
                myJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                myConnectedBubble = myJoint.connectedBody.GetComponent<BubbleBehavior>();
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
        shake_intensity = transform.localScale.x;
        shake_speed = transform.localScale.x - .5f;
    }


    void PopMyself() {
        if (!beenPopped)
        {
            //if (myConnectedBubble != null)
            //{
            //    myConnectedBubble.PopMyself();
            //}
            beenPopped = true;
            gameObject.SetActive(false);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        if (haveCollided)
        {
           
        }

        if (beenBlown) {
            Wobble();
        }

       // print(beenBlown);
    }

    void Wobble() {
            rb.AddForce(Vector3.right * Random.Range(-shake_speed, shake_speed));
            rb.AddForce(Vector3.up * Random.Range(-shake_speed, shake_speed));
            transform.Rotate(0, 0, shake_intensity);
    }

    void BubbleResidueOnPop(Vector3 popSpot) {
        Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.3f, .3f), popSpot.y + Random.Range(-.1f, .1f)), Quaternion.identity);
        Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.1f, .1f), popSpot.y + Random.Range(-.3f, .3f)), Quaternion.identity);
        Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.2f, .2f), popSpot.y + Random.Range(-.2f, .2f)), Quaternion.identity);
    }

    void BubbleResidueOnPlayer(Vector3 popSpot, Transform blower)
    {
        GameObject tempRes = Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.3f, .3f), popSpot.y + Random.Range(-.1f, .1f)), Quaternion.identity);
        tempRes.transform.parent = blower;
        GameObject tempRes2 = Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.1f, .1f), popSpot.y + Random.Range(-.3f, .3f)), Quaternion.identity);
        tempRes2.transform.parent = blower;
        GameObject tempRes3 = Instantiate(residue, new Vector3(popSpot.x + Random.Range(-.2f, .2f), popSpot.y + Random.Range(-.2f, .2f)), Quaternion.identity);
        tempRes3.transform.parent = blower;
    }
}
