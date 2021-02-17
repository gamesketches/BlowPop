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
	public bool releaseToFire;
	public float rotateSpeed;
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
					if(Input.mousePosition.x > lastX) transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
					else if(Input.mousePosition.x < lastX) transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
					lastX = Input.mousePosition.x;
				}
			} else if(Input.GetMouseButtonDown(0)){
				CreateBubble();
							}
		} else if(Input.GetMouseButtonUp(0) && curBubble) {
			if(releaseToFire) ReleaseBubble();
			else releaseTimer = releaseThreshold;
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
		movementVector = movementVector.normalized * breath.Evaluate(breathTimer);
		curBubble.GetComponent<Rigidbody2D>().AddForce(movementVector, ForceMode2D.Impulse);
		rigidbody.AddForce(-movementVector, ForceMode2D.Impulse);
		curBubble = null;
		breathTimer = 0;
		releaseTimer = 0;
	}
	
	void CreateBubble() {
		curBubble = Instantiate<GameObject>(bubblePrefab);
		curBubble.transform.parent = transform;
		curBubble.transform.localPosition = Vector3.up * 0.2f;
		lastX = Input.mousePosition.x;
		curBubble.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
	}

}
