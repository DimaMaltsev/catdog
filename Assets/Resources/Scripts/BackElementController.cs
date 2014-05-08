using UnityEngine;
using System.Collections;

public class BackElementController : MonoBehaviour {

	private EventManager ev;
	private bool gameIsOn = false;
	private SpriteRenderer sprite;

	public int speed;

	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void Start () {
		Animator a = GetComponent<Animator>();
		int type = Random.Range( 1 , 6 );

		a.SetInteger( "randomer" , type );

		ev.Subscribe( transform , "StartGame" 	);
		ev.Subscribe( transform , "StopGame" 	);
	}

	void Update(){
		if( !gameIsOn ){
			return;
		}

		transform.position -= Vector3.right * Time.deltaTime * speed;
		if( transform.position.x <= -11 * 1.69f )
			MoveMeToRight();
	}

	// ------------------ Routine ------------------------

	private void MoveMeToRight(){
		transform.position = new Vector3( 11 * 1.69f , transform.position.y , 0 );
	}

	// ------------------ Events -------------------------

	public void OnStartGame(){
		gameIsOn = true;
	}

	public void OnStopGame(){
		gameIsOn = false;
	}
}
