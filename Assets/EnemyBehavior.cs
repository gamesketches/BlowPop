using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform myShadow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position != myShadow)
        //{
        //    Vector3.MoveTowards(transform.position, myShadow, 2 * Time.deltaTime);
        //}
        //else {
        //    transform.GetChild(0).gameObject.SetActive(true);
        //}

        float step = 20 * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, myShadow.transform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, myShadow.transform.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            transform.GetChild(0).gameObject.SetActive(true);
            
        }

    }
}
