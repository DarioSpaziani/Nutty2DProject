using UnityEngine;

namespace Nutty2D {

	public class Trash : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.GetComponent<Bottle>()) {
				AudioManager.Instance.PlayTrashSound();
				Manager.Instance.SpawnBottles();
			}
		}
	}

}