﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public GameObject toothEnemy;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Shadow")
    //    {
    //        Destroy(gameObject);
    //    }

    //    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnToothEnemy() {
     
            GameObject newTooth = Instantiate(toothEnemy, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity) as GameObject;
            float tempSizer = Random.Range(1.0f, 1.75f);
           // newTooth.transform.localScale = new Vector3(Random.Range(1.0f, 1.5f), Random.Range(1.0f, 1.5f), Random.Range(1.0f, 1.5f));
            newTooth.transform.localScale = new Vector3(tempSizer, tempSizer, tempSizer);
            newTooth.GetComponent<EnemyBehavior>().myShadow = gameObject.transform;
    }
}
