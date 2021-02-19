using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    public Sprite[] mySprites;
    SpriteRenderer sr;
   
   
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = mySprites[Random.Range(0, mySprites.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
