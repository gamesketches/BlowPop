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

    // Start is called before the first frame update
    void Start()
    {
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
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			cursorPosition--;
			if(cursorPosition < 0) cursorPosition = options.Length - 1;
		}
		UpdateCursorPosition();
		if(Input.GetKeyDown(KeyCode.Space)) {
			ProcessMenuSelection();
		}
    }

	void UpdateCursorPosition() {
		Vector3 curCursorPosition = cursor.position;
		curCursorPosition.y = transform.GetChild(cursorPosition).position.y;
		cursor.position = curCursorPosition;
	}

	void ProcessMenuSelection() {
		switch(cursorPosition) {
			case 0: 
				SceneManager.LoadScene(0);
				break;
			case 1:
				Debug.Log("print life");
				break;
		}
	}
}
