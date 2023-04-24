using UnityEngine;

namespace Nutty2D {

	public class Nutty : MonoBehaviour {

		private void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.GetComponent<Bottle>()) {
				Manager.Instance.Score(col.gameObject.GetComponent<Bottle>().bottleIndex);
				Destroy(col.gameObject);
				Manager.Instance.SpawnBottles();
			}
		}
	}

}