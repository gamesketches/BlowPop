using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public KeyCode moveUp;
	public KeyCode moveDown;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	static float walkSpeed = 3;
	public float blowBackMultiplier = 1;
	float bubbleLimit = 1;
	List<BubbleBehavior> blownBubbles;
	TrailRenderer bubbleSmear;
	float maxForce;
	Rigidbody2D rigidbody;
	float stickyness;
	public float stickynessMultiplier;
	float timeSinceLastHit;
	AudioSource audio;

	// Start is called before the first frame update
	void Start()
	{
		audio = GetComponent<AudioSource>();
		breathTimer = 0;
		maxForce = breath.keys[breath.length - 1].value;
		rigidbody = GetComponent<Rigidbody2D>();
		stickyness = 0;
		bubbleSmear = GetComponent<TrailRenderer>();
		bubbleSmear.widthMultiplier = 0.1f;
		timeSinceLastHit = -1;
		blownBubbles = new List<BubbleBehavior>();
	}

	// Update is called once per frame
	void Update()
	{
		HandleMovement();
		BubbleBlowingLogic();
		CheckBlownBubbles();
		//HandleRotation();
		UpdateStickyness();
		if(timeSinceLastHit >= 0) timeSinceLastHit += Time.deltaTime;
	}

	void HandleMovement() {
		if(Input.GetKey(moveUp)) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		} else if(Input.GetKey(moveDown)) {
			transform.rotation = Quaternion.Euler(0, 0, 180);
		} else if(Input.GetKey(moveLeft)) {
			transform.rotation = Quaternion.Euler(0, 0, 90);
		} else if(Input.GetKey(moveRight)) {
			transform.rotation = Quaternion.Euler(0, 0, 270);
		} else {
			return;
		}
		transform.Translate(new Vector3(0, walkSpeed * Time.deltaTime, 0));
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
			} else if(ButtonPressed() && blownBubbles.Count < bubbleLimit){
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
		//bubbleSmear.startWidth = stickyness;
	}

	void CreateBubble() {
		//curBubble = Instantiate<GameObject>(bubblePrefab, transform.position + (transform.forward * 1f), Quaternion.identity); 
		curBubble = Instantiate <GameObject>(bubblePrefab, transform.forward * 1.3f + transform.position, transform.rotation);

		curBubble.transform.parent = transform;
		//curBubble.transform.localPosition = Vector3.up * 0.2f;
		lastX = Input.mousePosition.x;
		curBubble.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
		blownBubbles.Add(curBubble.GetComponent<BubbleBehavior>());
	}

	void BlowUpBubble() {
		float curScalar = breath.Evaluate(breathTimer);
		curBubble.transform.localScale = new Vector3(curScalar, curScalar, curScalar);
		//curBubble.transform.position = transform.position + (transform.forward * 1f); //+ (transform.up * .6f)
		curBubble.transform.localPosition = new Vector3(0, .075f, 0);
	}

	void ReleaseBubble() {
		curBubble.transform.parent = null;
		curBubble.GetComponent<Animator>().SetTrigger("Blown");
		Vector3 movementVector = curBubble.transform.position - transform.position;
		Vector3 bubbleVector = movementVector.normalized * (maxForce - breath.Evaluate(breathTimer)) * blowBackMultiplier;
		curBubble.GetComponent<Rigidbody2D>().AddForce(bubbleVector, ForceMode2D.Impulse);
		curBubble.GetComponent<BubbleBehavior>().beenBlown = true;
		Vector3 playerVector = -movementVector.normalized * (breath.Evaluate(breathTimer)) * blowBackMultiplier;
		//rigidbody.AddForce(playerVector, ForceMode2D.Impulse);
		curBubble = null;
		breathTimer = 0;
		releaseTimer = 0;
		audio.Play();
	}

	void CheckBlownBubbles() {
		for(int i = 0; i < blownBubbles.Count; i++) {
			if(blownBubbles[i].beenPopped) {
				blownBubbles.Remove(blownBubbles[i]);
				i--;
			}
		}
	}
	
	void HandleRotation() {
		if(Input.GetKey(turnRight)) {
			transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
			//rigidbody.AddTorque(-1, ForceMode2D.Force);
		} else if(Input.GetKey(turnLeft)) {
			transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
			//rigidbody.AddTorque(1, ForceMode2D.Force);
		} else {
			//if(Input.mousePosition.x > lastX) transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
			//else if(Input.mousePosition.x < lastX) transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
			//lastX = Input.mousePosition.x;
		}
	}

	public void UpdateDrag(float newDrag) {
		rigidbody.drag = newDrag;
	}

	public void AdjustDrag(float adjustBy) {
		rigidbody.drag += adjustBy;
	}

	public void AttachResidue(float size) {
		stickyness += size * stickynessMultiplier;
		/*bubbleSmear.AddPosition(transform.position);
		AnimationCurve curBubbleSmear = bubbleSmear.widthCurve;
		if(curBubbleSmear.keys.Length == 1) {
			timeSinceLastHit = 0;
			Keyframe[] newFrames = new Keyframe[2] {new Keyframe(stickyness, 0f), new Keyframe(0, Time.deltaTime)};
			curBubbleSmear.keys = newFrames;
		} else {
			Keyframe[] newFrames = new Keyframe[curBubbleSmear.keys.Length + 1];
			newFrames[0] = new Keyframe(stickyness, 0f);
			for(int i = 0; i < curBubbleSmear.keys.Length; i++) {
				curBubbleSmear.keys[i].time += timeSinceLastHit;
				newFrames[i+1] = curBubbleSmear.keys[i];
			}
			bubbleSmear.widthCurve.keys = newFrames;
			timeSinceLastHit = 0;
		}*/
	}

	public float GetStickyness() {
		return stickyness;
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

	public void IncreaseBubbleLimit(int increaseBy) {
		bubbleLimit += increaseBy;
	}

	public void EarlyPopBubble() {
		ReleaseBubble();
		//curBubble = null;
		//breathTimer = 0;
		//releaseTimer = 0;
	}
}
