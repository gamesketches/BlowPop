using System.Collections;
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
     
            GameObject newShadow = Instantiate(toothEnemy, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
            newShadow.GetComponent<EnemyBehavior>().myShadow = gameObject.transform;
    }
}
