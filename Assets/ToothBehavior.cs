using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBehavior : MonoBehaviour
{
	public float timeToCenter;
	Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void MoveTowardsCenter() {
		StartCoroutine(MoveTowardsPoint(Vector3.zero));
	}

	IEnumerator MoveTowardsPoint(Vector3 targetPoint) {
		Vector3 startPos = transform.position;
		for(float t = 0; t < timeToCenter; t+= Time.deltaTime) {
			Vector3 newPos = Vector3.Lerp(startPos, targetPoint, t / timeToCenter);
			rigidbody.MovePosition(new Vector2(newPos.x, newPos.y));
			yield return null;
		}
	}
}
