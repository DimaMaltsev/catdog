using UnityEngine;
using System.Collections;

public class CatSuck : MonoBehaviour {

	private EventManager ev;
	private bool sucked = false;

	
	private GameController gameController;

	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
		gameController = GameObject.FindGameObjectWithTag( "GameController" ).GetComponent<GameController>();
	}

	void Start () {
		ev.Subscribe( transform , "StopGame" );
	}

	void Update(){
		if( !IsInvoking( "SubMitCatScore" ) && !sucked ){
			Invoke ( "SubMitCatScore" , 0.5f );
		}
	}

	public void Suck(){
		if( sucked ) return;
		sucked = true;
		GetComponent<Animator>().SetInteger( "suck" , 1 );
		GetComponent<CatMover>().targetPosition = transform.position - Vector3.right * 20;// run animation
		GetComponent<CatMover>().speed = 20;
	}
	
	private void SubMitCatScore(){
		gameController.CatLive();
	}

	public void OnStopGame(){
		Destroy( gameObject );
	}
}
