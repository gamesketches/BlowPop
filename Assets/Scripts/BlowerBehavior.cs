using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerBehavior : MonoBehaviour
{
	public GameObject bubblePrefab;
	GameObject curBubble;
	public AnimationCurve breath;
	float breathTimer;
	public float releaseThreshold;
	float releaseTimer;
	float lastX;
	Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        breathTimer = 0;
		rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		releaseTimer -= Time.deltaTime;
        if(Input.GetMouseButton(0)) {
			if(curBubble) {
				if(releaseTimer > 0) {
					ReleaseBubble();
				} else {
					breathTimer += Time.deltaTime;
					BlowUpBubble();
					if(Input.mousePosition.x > lastX) transform.Rotate(0, 0, 1);
					else if(Input.mousePosition.x < lastX) transform.Rotate(0, 0, -1);
					lastX = Input.mousePosition.x;
				}
			} else if(Input.GetMouseButtonDown(0)){
				curBubble = Instantiate<GameObject>(bubblePrefab);
				curBubble.transform.parent = transform;
				curBubble.transform.localPosition = Vector3.up * 0.2f;
				lastX = Input.mousePosition.x;
				curBubble.transform.position += new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
				curBubble.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
			}
		} else if(Input.GetMouseButtonUp(0) && curBubble) {
			releaseTimer = releaseThreshold;
		} else {
			if(curBubble) {
				breathTimer -= Time.deltaTime;
				BlowUpBubble();
			}
		}
    }
	
	void BlowUpBubble() {
		float curScalar = breath.Evaluate(breathTimer);
		curBubble.transform.localScale = new Vector3(curScalar, curScalar, curScalar);
	}

	void ReleaseBubble() {
		curBubble.transform.parent = null;
		Vector3 movementVector = curBubble.transform.position - transform.position;
		curBubble.GetComponent<Rigidbody2D>().AddForce(movementVector, ForceMode2D.Impulse);
		rigidbody.AddForce(-movementVector, ForceMode2D.Impulse);
		curBubble = null;
		releaseTimer = 0;
	}
}
