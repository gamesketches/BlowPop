using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidueBehavior : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Toothbrush") {
            gameObject.SetActive(false);
           // Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
