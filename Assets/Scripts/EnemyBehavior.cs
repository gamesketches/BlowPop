using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform myShadow;
    Rigidbody2D rb;
    bool iveDropped;
    CircleCollider2D myCol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Toothbrush" && iveDropped)
        {
            //transform.rotation = Quaternion.identity;
            var magnitude = 100;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * magnitude);
            transform.GetChild(0).gameObject.SetActive(false);
            myCol.enabled = false;
            rb.gravityScale = 1f;
            Destroy(myShadow.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
            rb = gameObject.GetComponent<Rigidbody2D>();
            myCol = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!iveDropped) {
            float step = 20 * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, myShadow.transform.position, step);
            if (Vector3.Distance(transform.position, myShadow.transform.position) < 0.001f)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                iveDropped = true;

            }
        }

    }
}
