using UnityEngine;
using System.Collections;

public class DogKicker : MonoBehaviour {

	public float kickCoolDown;

	private EventManager ev;
	private bool gameIsOn = false;
	private bool canKick = false;

	private GameController gameController;

	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
		gameController = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
	}

	// Use this for initialization
	void Start () {
		ev.Subscribe( transform , "StartGame" );
		ev.Subscribe( transform , "StopGame" );
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<Animator>().SetInteger( "kick" , 0 );
		if( !gameIsOn || !canKick ) return;
		if( Input.GetButtonDown( "Jump" ) ) {
			Kick();
			DisableKick();
			Invoke( "EnableKick" , kickCoolDown );
		}
	}

	private void Kick(){
		Collider2D[] points = Physics2D.OverlapPointAll( transform.position - Vector3.right * 1.7f );
		for( int i = 0 ; i < points.Length ; i ++ ){
			if( points[ i ].GetComponent<CatMover>() == null ) continue;
			points[ i ].GetComponent<CatSuck>().Suck();
			gameController.CatSucked();
		}

		GetComponent<Animator>().SetInteger( "kick" , 1 );
	}

	private void EnableKick(){
		canKick = true;
	}

	private void DisableKick(){
		canKick = false;
	}


	public void OnStartGame(){
		gameIsOn = true;
		EnableKick();
	}
	
	public void OnStopGame(){
		gameIsOn = false;
		DisableKick();
	}
}
