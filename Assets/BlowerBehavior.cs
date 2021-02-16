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
				}
			} else {
				curBubble = Instantiate<GameObject>(bubblePrefab);
				curBubble.transform.parent = transform;
				curBubble.transform.localPosition = Vector3.zero + (Vector3.up * 0.2f);
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
		float curScalar = curBubble.transform.localScale.x;
		curScalar = breath.Evaluate(breathTimer);
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
