using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerBehavior : MonoBehaviour
{
	public GameObject bubblePrefab;
	GameObject curBubble;
	public AnimationCurve breath;
	float breathTimer;
    // Start is called before the first frame update
    void Start()
    {
        breathTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
			if(curBubble) {
				breathTimer += Time.deltaTime;
				BlowUpBubble();
			} else {
				curBubble = Instantiate<GameObject>(bubblePrefab);
				curBubble.transform.parent = transform;
				curBubble.transform.localPosition = Vector3.zero + (Vector3.up * 0.34f);
			}
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
}
