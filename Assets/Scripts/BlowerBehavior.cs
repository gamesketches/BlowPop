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
	public KeyCode TurnLeft;
	public KeyCode TurnRight;
	public KeyCode BubbleButton;
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
		BubbleBlowingLogic();
		HandleRotation();
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

	void CreateBubble() {
		//curBubble = Instantiate<GameObject>(bubblePrefab, transform.position + (transform.forward * 1f), Quaternion.identity); 
		curBubble = Instantiate <GameObject>(bubblePrefab, transform.forward * .25f + transform.position, transform.rotation);

		curBubble.transform.parent = transform;
		//curBubble.transform.localPosition = Vector3.up * 0.2f;
		lastX = Input.mousePosition.x;
		curBubble.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
	}

	void BlowUpBubble() {
		float curScalar = breath.Evaluate(breathTimer);
		curBubble.transform.localScale = new Vector3(curScalar, curScalar, curScalar);
		
		//curBubble.transform.position = transform.position + (transform.forward * 1f); //+ (transform.up * .6f)
		
	}

	void ReleaseBubble() {
		curBubble.transform.parent = null;
		Vector3 movementVector = curBubble.transform.position - transform.position;
		movementVector = movementVector.normalized * breath.Evaluate(breathTimer);
		curBubble.GetComponent<Rigidbody2D>().AddForce(movementVector, ForceMode2D.Impulse);
		rigidbody.AddForce(-movementVector * 2, ForceMode2D.Impulse);
		curBubble = null;
		breathTimer = 0;
		releaseTimer = 0;
		
	}
	
	void HandleRotation() {
		if(Input.GetKey(TurnRight)) {
			transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
		} else if(Input.GetKey(TurnLeft)) {
			transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
		} else {
			if(Input.mousePosition.x > lastX) transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
			else if(Input.mousePosition.x < lastX) transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
			lastX = Input.mousePosition.x;
		}
	}

	public void UpdateDrag(float newDrag) {
		rigidbody.drag = newDrag;
	}

	bool ButtonActive() {
		return Input.GetMouseButton(0) || Input.GetKey(BubbleButton);
	}

	bool ButtonPressed() {
		return Input.GetMouseButtonDown(0) || Input.GetKeyDown(BubbleButton);
	}

	bool ButtonReleased() {
		return Input.GetMouseButtonUp(0) || Input.GetKeyUp(BubbleButton);
	}
}
