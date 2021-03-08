using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitBehavior : MonoBehaviour
{
	public BlowerBehavior player;
	public static float maxStickyness = 20;
	int numStages;

    // Start is called before the first frame update
    void Start()
    {
		numStages = transform.childCount;
		for(int i = 1; i < numStages; i++) transform.GetChild(i).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePortrait(player.GetStickyness() / maxStickyness);
    }

	void UpdatePortrait(float curStickyness) {
		int numSticks = Mathf.FloorToInt(Mathf.Lerp(0, numStages, curStickyness));
		for(int i = 1; i < numStages; i++) {
			if(i < numSticks)
				transform.GetChild(i).gameObject.SetActive(true);
			else
				transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
