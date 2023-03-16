using UnityEngine;

public class Nutty : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.GetComponent<Bottle>()) {
			Destroy(col.gameObject);
			Manager.Instance.SpawnBottles();
		}
	}
}
