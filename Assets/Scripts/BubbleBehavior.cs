using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blower") {

        }

        if (collision.gameObject.tag == "Bubble")
        {
            HingeJoint2D myJoint = (HingeJoint2D)gameObject.AddComponent<HingeJoint2D>();
            myJoint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
