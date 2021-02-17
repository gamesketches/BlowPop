using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	public float spawnInterval;
	public float spawnDistanceScalar;
	float spawnTimer;
	public Transform background;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
		if(spawnTimer > spawnInterval) {
			CreateEnemy();
			spawnTimer = 0;
		}
    }

	void CreateEnemy() {
		GameObject newEnemy = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Tooth"));
		newEnemy.transform.parent = background.transform;
		newEnemy.transform.localPosition = Vector3.zero;
		newEnemy.transform.Rotate(0, 0, Random.value * 360);
		newEnemy.transform.position = newEnemy.transform.up.normalized * spawnDistanceScalar;
		newEnemy.transform.parent = null;
		newEnemy.GetComponent<ToothBehavior>().MoveTowardsCenter();
	}
}
