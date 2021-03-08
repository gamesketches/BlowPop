using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	public float spawnInterval;
	public float spawnDistanceScalar;
	float spawnTimer;
	public Transform background;


	//public GameObject ToothEnemy;
	public GameObject shadow;

	// Start is called before the first frame update
	void Start()
    {
        spawnTimer = 0;
		spawnInterval = Random.Range(7.5f, 12.5f);
    }

    // Update is called once per frame
    void Update()
    {
  //      spawnTimer += Time.deltaTime;
		//if(spawnTimer > spawnInterval) {
		//	CreateEnemy();
		//	spawnTimer = 0;
		//}

		spawnTimer += Time.deltaTime;
		if (spawnTimer > spawnInterval)
		{
			CreateShadow();
			spawnTimer = 0;
			spawnInterval = Random.Range(10.0f, 20.0f);
		}
	}

	void CreateEnemy() {
		GameObject newEnemy = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Tooth"), new Vector3(transform.position.x, transform.position.y + Random.Range(-5.0f, 5.0f), transform.position.z), Quaternion.identity);
		//newEnemy.transform.parent = background.transform;
		//newEnemy.transform.localPosition = Vector3.zero;
		newEnemy.transform.Rotate(0, 0, Random.value * 360);
		//newEnemy.transform.position = newEnemy.transform.up.normalized * spawnDistanceScalar;
		newEnemy.transform.parent = null;
		newEnemy.GetComponent<ToothBehavior>().MoveTowardsCenter();
	}

	void CreateShadow() {
		GameObject newShadow = Instantiate(shadow, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), transform.position.z), Quaternion.identity);

	}
}
