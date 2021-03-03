﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothbrushScript : MonoBehaviour
{
    float randomizer;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        randomizer = Random.Range(5.0f, 15.0f);
        anim = gameObject.GetComponent<Animator>();
        anim.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        randomizer -= Time.deltaTime;

        if (randomizer <= 0) {
            anim.enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ResetAnim() {
        randomizer = Random.Range(5.0f, 15.0f);
        anim.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
