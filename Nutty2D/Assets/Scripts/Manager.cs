using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager> 
{
	public List<Transform> spawnPoints;
	public  List<GameObject> bottles;
	public GameObject activeBottle;

	private void Start() {
		SpawnBottles();
	}

	public void SpawnBottles() {
		if (activeBottle == null)
			Destroy(activeBottle);

		int spawnIndex = Random.Range(0, spawnPoints.Count);
		int bottleIndex = Random.Range(0, bottles.Count);

		activeBottle = Instantiate(bottles[bottleIndex], spawnPoints[spawnIndex]);
	}
}
