using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //private void FixedUpdate()
    //{
    //    Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

    //    rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetDir.z, 2 * Time.deltaTime));
    //}
}
