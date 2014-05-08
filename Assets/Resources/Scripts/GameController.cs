using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private bool showMenu = true;

	public float catSpawnRate = 2;
	public int score;
	public int scoreCatSuck;
	public int scoreCatLive;

	// ----------------- Instantiation ------------------

	private void CreateDog(){

	}

	private void CreateCat(){
		GameObject el = (GameObject)Instantiate( Resources.Load( "Prefabs/Cat" ) , new Vector3( -12 , 0 , 0 ) , Quaternion.identity );
		int y = Random.Range( -4 , 4 );
		int targetX = Random.Range( -10 , 5 );
		el.transform.position += Vector3.up * y;

		el.GetComponent<CatMover>().targetPosition = new Vector3( targetX , y , -2 );
		catSpawnRate -= 0.01f;
		Invoke( "CreateCat" , catSpawnRate );
	}

	private void CreateFloor(){
		int xcount = 22;
		int ycount = 16;

		for( int x = -xcount / 2 ; x < xcount / 2 ; x ++ ){
			for( int y = -ycount/2; y < ycount / 2 ; y ++ ){
				GameObject el = (GameObject)Instantiate( Resources.Load( "Prefabs/BackElement" ) , new Vector3( x * 1.69f , y * 1.69f , 0 ) , Quaternion.identity );
			}
		}
	}

	// ------------------ GAME LOGIC -------------------

	public void StartGame(){
		ClearScene();
		ev.Trigger( "StartGame" , null );
		Invoke( "CreateCat" , catSpawnRate );
	}
	public void StopGame(){
		ev.Trigger( "StopGame" , null );
	}
	public void ClearScene(){}

	public void CatSucked(){
		score += scoreCatSuck;
		PrintScore();
	}

	public void CatLive(){
		score += scoreCatLive;
		PrintScore();
	}

	private void PrintScore(){
		GetComponent<TextMesh>().text = score.ToString();
	}
	// ------------------- Unity stuff ------------------------

	private EventManager ev;

	void Awake(){
		ev = GameObject.FindGameObjectWithTag( "EventManager" ).GetComponent<EventManager>();
	}

	void Start(){
		CreateFloor();
	}

	void OnGUI(){
		if( GUILayout.Button( "StartGame" ) ){
			StartGame();
		}
	}
}
