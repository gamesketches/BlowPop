using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidueBehavior : MonoBehaviour
{
    public bool onPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Toothbrush") {
            if (onPlayer) {
                transform.parent.GetComponent<BlowerBehavior>().AdjustDrag(-1f);
            }
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
