using UnityEngine;
using System.Collections;

public class DogMover : MonoBehaviour {

	public int speed;

	private bool gameIsOn = false;
	private float horizontal = 0;
	private float vertical   = 0;

	private EventManager ev;
	
	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
	}

	void Start(){
		ev.Subscribe( transform , "StartGame" );
		ev.Subscribe( transform , "StopGame" );
	}

	// Update is called once per frame
	void Update () {
		if( !gameIsOn ) return;
		horizontal 	= Input.GetKey( KeyCode.RightArrow ) 	? 1 : Input.GetKey( KeyCode.LeftArrow ) ? -1 : 0 ;
		vertical 	= Input.GetKey( KeyCode.UpArrow ) 		? 1 : Input.GetKey( KeyCode.DownArrow ) ? -1 : 0 ;

		if( horizontal == 0 && vertical == 0 ) return;

		transform.position += new Vector3 ( horizontal , vertical , 0 ) * speed * Time.deltaTime;
	}

	public void OnStartGame(){
		gameIsOn = true;
	}

	public void OnStopGame(){
		gameIsOn = false;
	}
}
