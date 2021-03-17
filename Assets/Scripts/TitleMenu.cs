using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
	public string[] options;
	int cursorPosition;
	Transform cursor;
	AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
        cursorPosition = 0;
		cursor = transform.GetChild(options.Length);
		if(options.Length != transform.childCount - 1) Debug.LogError("Number of options is insufficient!");
		for(int i = 0; i < options.Length; i++) {
			transform.GetChild(i).gameObject.GetComponent<Text>().text = options[i];
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			cursorPosition = (cursorPosition + 1) % options.Length;
			audio.Play();
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			cursorPosition--;
			if(cursorPosition < 0) cursorPosition = options.Length - 1;
			audio.Play();
		}
		UpdateCursorPosition();
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
			ProcessMenuSelection();
		}
    }

	void UpdateCursorPosition() {
		Vector3 curCursorPosition = cursor.position;
		curCursorPosition.y = transform.GetChild(cursorPosition).position.y;
		cursor.position = curCursorPosition;
	}

	IEnumerator BeginGame() {
		audio.clip = Resources.Load<AudioClip>("Sound/Confirm");
		audio.Play();
		while(audio.isPlaying) yield return null;
		SceneManager.LoadScene(0);
	}

	void ProcessMenuSelection() {
		switch(cursorPosition) {
			case 0: 
				StartCoroutine(BeginGame());
				break;
			case 1:
				Debug.Log("print life");
				break;
		}
	}
}
