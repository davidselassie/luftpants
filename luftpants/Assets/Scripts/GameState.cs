using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public Phases CurrentPhase;

	public float LogoTime = 5f;

	// Use this for initialization
	void Start () {
		CurrentPhase = Phases.LOGO;
		GameObject.Find ("Logo").SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		switch (CurrentPhase) {
			case Phases.LOGO:
			Debug.Log ("CurrentPhase:" + CurrentPhase);
			Debug.Log ("Time.time:" + Time.time);
			if(Time.time > LogoTime && Input.anyKeyDown){
				Debug.Log ("Switching to " + Phases.INSTRUCTIONS.ToString());
				GameObject.Find ("Logo").SetActive (false);
				GameObject.Find ("Instructions").SetActive (true);
				CurrentPhase = Phases.INSTRUCTIONS;
			}	
			break;
		}

	}

	
	public enum Phases{
		LOGO,INSTRUCTIONS,PLAY,CREDITS
	}
}
