using UnityEngine;

namespace Nutty2D {

	public class Option : MonoBehaviour {
		private void OnMouseDown() {
			Manager.Instance.Option();
		}
	}

}