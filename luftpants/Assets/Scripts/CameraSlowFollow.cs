using UnityEngine;
using System.Collections;

public class CameraSlowFollow : MonoBehaviour {

		private Vector3 targetPosition;
		public GameObject ship1;
		public GameObject ship2;
		public float smooth = 2f;
		private Vector3 target;

		
		
		// (in New York accent) "Hey I'm changing this script over here!"
		
		void Update () {
			
			target = (ship1.transform.position + ship2.transform.position)/2;
			targetPosition = new Vector3(target.x, transform.position.y, target.z);
			transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		}
	}
