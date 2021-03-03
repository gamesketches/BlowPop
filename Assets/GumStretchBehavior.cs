using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumStretchBehavior : MonoBehaviour
{

    Vector3 scaleChange;
    public Transform myTarget;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(+Random.Range(0.004f, 0.005f), +Random.Range(0.004f, 0.006f), +Random.Range(0.005f, 0.005f));
        //scaleChange = new Vector3(+0.001f, +0.001f, +0.001f);

        //transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.parent.position.z));
       
    }

    // Update is called once per frame
    void Update()
    {
       

        if (transform.localScale.z < 2f) {
            gameObject.transform.localScale += scaleChange;

            Vector3 difference = myTarget.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
        }
    }

    
}
