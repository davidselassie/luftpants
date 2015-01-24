//using UnityEngine;
//using System.Collections;
//
//public class SpinThumbstick : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//
//    int StickSpin(float oldX, float newX, float oldY, float newY){
//        //returns 1 for clockwise, -1 for counterclockwise, 0 for neither
//        if (newX > oldX){
//            if (newY > oldY)
//                return -1;
//            else if (newY < oldY)
//                return 1;
//        }else if (newX < oldX){
//            if (newY > oldY)
//                return -1;
//            else if (newY < oldY)
//                return 1;
//        }
//        else
//            return 0;
//    }
//	
//	// Update is called once per frame
//	void Update () {
//
//	
//	}
//}
