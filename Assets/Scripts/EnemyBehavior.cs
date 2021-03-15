﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public Transform myShadow;
    Rigidbody2D rb;
    bool iveDropped;
    CircleCollider2D myCol;
    public LayerMask blowerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Toothbrush" && iveDropped)
        {
            rb.isKinematic = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Blower" && !iveDropped)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
            gameObject.transform.localScale = new Vector3(Random.Range(1, 2), Random.Range(1, 2), Random.Range(1, 2));
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
                if (Check4Kill())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else {
                    transform.GetChild(0).gameObject.SetActive(true);
                    rb.isKinematic = true;
                    iveDropped = true;

                }

            }
        }

    }

    bool Check4Kill() {
        if (Physics2D.OverlapCircle(transform.position, 0.5f, blowerLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
