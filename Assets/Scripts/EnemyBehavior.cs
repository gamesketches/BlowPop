using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public Transform myShadow;
    Rigidbody2D rb;
    bool iveDropped;
    CircleCollider2D myCol;
    public LayerMask blowerLayer;
	static float deathAnimationTime = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Toothbrush" && iveDropped)
        {
            rb.isKinematic = false;
            //transform.rotation = Quaternion.identity;
            var magnitude = 100;
            var force = transform.position - collision.transform.position;
            force.Normalize();
            gameObject.GetComponent<Rigidbody2D>().AddForce(force * magnitude);
            transform.GetChild(0).gameObject.SetActive(false);
            myCol.enabled = false;
            rb.gravityScale = 1f;
            Destroy(myShadow.gameObject);
        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Blower" && !iveDropped)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
            rb = gameObject.GetComponent<Rigidbody2D>();
            myCol = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!iveDropped) {
            float step = 20 * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, myShadow.transform.position, step);
            if (Vector3.Distance(transform.position, myShadow.transform.position) < 0.001f)
            {
                if (Check4Kill())
                {
					StartCoroutine(DeathAnimation());
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else {
                    transform.GetChild(0).gameObject.SetActive(true);
                    rb.isKinematic = true;
                    iveDropped = true;

                    //sorting order issues with sprite renderer order
                   // GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
                   // transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }

            }
        }

    }

    bool Check4Kill() {
        if (Physics2D.OverlapCircle(transform.position, transform.localScale.z/2, blowerLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	IEnumerator DeathAnimation() {
		float deltaTime = Time.deltaTime;
		Time.timeScale = 0;
		Vector3 startPos = transform.position;
		Vector3 endPos = myShadow.transform.position;
		for(float t = 0; t < deathAnimationTime; t += deltaTime) {
			transform.position = Vector3.Lerp(startPos, endPos, t / deathAnimationTime);
			yield return null;
		}
		GameObject screenFlash = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
		screenFlash.SetActive(true);
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.Play();
		yield return new WaitForSecondsRealtime(0.2f);
		screenFlash.SetActive(false);
		yield return new WaitForSecondsRealtime(0.45f);
		audioSource.clip = Resources.Load<AudioClip>("Sound/Wail");
		audioSource.Play();
		while(audioSource.isPlaying) yield return null;
		Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
