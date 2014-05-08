using UnityEngine;
using System.Collections;

public class CatMover : MonoBehaviour {

	public Vector3 targetPosition;
	public int speed;
	private EventManager ev;

	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
	}

	// Use this for initialization
	void Start () {
		ev.Subscribe( transform , "StopGame" );
	}
	
	// Update is called once per frame
	void Update () {
		if( ( targetPosition - transform.position ).magnitude > 1 ){
			transform.position += ( targetPosition - transform.position ).normalized * Time.deltaTime * speed;
		}
	}

	public void OnStopGame(){
		Destroy( gameObject );
	}
}
