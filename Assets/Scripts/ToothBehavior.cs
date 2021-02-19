using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBehavior : MonoBehaviour
{
	public float timeToCenter;
	Rigidbody2D rigidbody;
	Vector3 myTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Bubble") {
			//transform.rotation = Quaternion.identity;
			rigidbody.isKinematic = true;
			StopAllCoroutines();
		}

		if (collision.gameObject.tag == "Tooth")
		{
			gameObject.SetActive(false);
		}
	}
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
		timeToCenter = Random.Range(4.0f, 10.0f);
		myTarget = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);

	}

    // Update is called once per frame
    void Update()
    {
		//Wobble();
    }

	public void MoveTowardsCenter() {
		StartCoroutine(MoveTowardsPoint(myTarget));
	}

	IEnumerator MoveTowardsPoint(Vector3 targetPoint) {
		Vector3 startPos = transform.position;
		for(float t = 0; t < timeToCenter; t+= Time.deltaTime) {
			Vector3 newPos = Vector3.Lerp(startPos, targetPoint, t / timeToCenter);
			rigidbody.MovePosition(new Vector2(newPos.x, newPos.y));
			yield return null;
		}
	}

	void Wobble()
	{
		rigidbody.AddForce(Vector3.right * Random.Range(-0.5f, 0.5f));
		rigidbody.AddForce(Vector3.up * Random.Range(-1, 1));
		transform.Rotate(0, 0, 1);
	}
}
