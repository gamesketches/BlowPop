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
	public KeyCode turnLeft;
	public KeyCode turnRight;
	public KeyCode bubbleButton;
	public float blowBackMultiplier = 1;
	float maxForce;
	Rigidbody2D rigidbody;
	float stickyness;
	public float stickynessMultiplier;

	// Start is called before the first frame update
	void Start()
	{
		breathTimer = 0;
		maxForce = breath.keys[breath.length - 1].value;
		rigidbody = GetComponent<Rigidbody2D>();
		stickyness = 0;
	}

	// Update is called once per frame
	void Update()
	{
		BubbleBlowingLogic();
		HandleRotation();
		UpdateStickyness();
	}

	void BubbleBlowingLogic() {
		releaseTimer -= Time.deltaTime;
		if (ButtonActive())
		{
			if (curBubble)
			{
				if (releaseTimer > 0)
				{
					ReleaseBubble();
				}
				else
				{
					breathTimer += Time.deltaTime;
					BlowUpBubble();
									}
			} else if(ButtonPressed()){
				CreateBubble();
							}
		} else if(ButtonReleased() && curBubble) {
			if(releaseToFire) ReleaseBubble();
			else releaseTimer = releaseThreshold;
		} else {
			if(curBubble) {
				breathTimer -= Time.deltaTime;
				BlowUpBubble();
			}
		}
	}

	void UpdateStickyness() {
		stickyness -= rigidbody.velocity.normalized.magnitude * Time.deltaTime;
		if(stickyness < 0) stickyness = 0;
		rigidbody.drag = 1.5f + stickyness;
	}

	void CreateBubble() {
		//curBubble = Instantiate<GameObject>(bubblePrefab, transform.position + (transform.forward * 1f), Quaternion.identity); 
		curBubble = Instantiate <GameObject>(bubblePrefab, transform.forward * 1f + transform.position, transform.rotation);

		curBubble.transform.parent = transform;
		//curBubble.transform.localPosition = Vector3.up * 0.2f;
		lastX = Input.mousePosition.x;
		curBubble.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
	}

	void BlowUpBubble() {
		float curScalar = breath.Evaluate(breathTimer);
		curBubble.transform.localScale = new Vector3(curScalar, curScalar, curScalar);
		//curBubble.transform.position = transform.position + (transform.forward * 1f); //+ (transform.up * .6f)
		curBubble.transform.localPosition = new Vector3(0, .075f, 0);
	}

	void ReleaseBubble() {
		curBubble.transform.parent = null;
		Vector3 movementVector = curBubble.transform.position - transform.position;
		Vector3 bubbleVector = movementVector.normalized * (maxForce - breath.Evaluate(breathTimer)) * blowBackMultiplier;
		curBubble.GetComponent<Rigidbody2D>().AddForce(bubbleVector, ForceMode2D.Impulse);
		curBubble.GetComponent<BubbleBehavior>().beenBlown = true;
		Vector3 playerVector = -movementVector.normalized * (breath.Evaluate(breathTimer)) * blowBackMultiplier;
		rigidbody.AddForce(playerVector, ForceMode2D.Impulse);
		curBubble = null;
		breathTimer = 0;
		releaseTimer = 0;
	}
	
	void HandleRotation() {
		if(Input.GetKey(turnRight)) {
			transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
		} else if(Input.GetKey(turnLeft)) {
			transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
		} else {
			//if(Input.mousePosition.x > lastX) transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
			//else if(Input.mousePosition.x < lastX) transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
			//lastX = Input.mousePosition.x;
		}
	}

	public void UpdateDrag(float newDrag) {
		rigidbody.drag = newDrag;
	}

	public void AttachResidue(float size) {
		stickyness += size * stickynessMultiplier;
	}

	bool ButtonActive() {
		return Input.GetMouseButton(0) || Input.GetKey(bubbleButton);
	}

	bool ButtonPressed() {
		return Input.GetMouseButtonDown(0) || Input.GetKeyDown(bubbleButton);
	}

	bool ButtonReleased() {
		return Input.GetMouseButtonUp(0) || Input.GetKeyUp(bubbleButton);
	}

	public void EarlyPopBubble() {
		curBubble = null;
		breathTimer = 0;
		releaseTimer = 0;
	}
}
