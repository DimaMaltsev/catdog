using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
	private Dictionary<string, List<Transform>> 	events 		= new Dictionary<string, List<Transform>>();
	
	private List<string> eventDisplay = new List<string>();
	
	private string eventsHistory;
	
	private void RemoveSubscriber( string eventName , Transform subscriber ){
		events[ eventName ].Remove( subscriber );
		Log( "Subscriber removed (" + subscriber.name + ") from event (" + eventName + ")" );
		if( events[ eventName ].Count == 0 ){ events.Remove( eventName ); Log( "Event removed (" + eventName + ")" ); }
	}
	
	private void RemoveSubscriberOnly( string eventName , Transform subscriber ){
		events[ eventName ].Remove( subscriber );
		Log( "Subscriber removed (" + subscriber.name + ") from event (" + eventName + ")" );
	}
	
	private void RemoveEmptyEvents(){
		List<string> eventsToRemove = new List<string>();
		foreach( KeyValuePair<string, List<Transform>> ev in events) { 
			if( ev.Value.Count == 0 )
				eventsToRemove.Add( ev.Key );
		}
		
		for( int i = 0 ; i < eventsToRemove.Count ; i++ ){
			events.Remove( eventsToRemove[ i ] );
			Log( "Event removed (" + eventsToRemove[ i ] + ")" );
		}
	}
	
	private void Log( string logData ){
		eventsHistory += logData + "\n";
	}
	
	public void ShowLog(){
		Debug.Log( eventsHistory );
	}
	
	public void Subscribe( Transform transform , string eventName ){
		if( !events.ContainsKey( eventName ) ){
			events.Add( eventName , new List<Transform>() );
			eventDisplay.Add( eventName );
			Log( "Created event (" + eventName + ")" );
		}
		
		if( !events[ eventName ].Contains( transform ) ){
			events[ eventName ].Add( transform );
			Log( "New subscriber (" + transform.name + ") on event (" + eventName + ")" );
		}else
			Debug.Log("SUBSCRIBER '" + transform.name  + "' TRIES TO SUBSCRIBE ON '" + eventName + "' TWICE" );
	}
	
	public void Unsubscribe( Transform transform , string eventName = "" ){
		if( eventName != "" ){
			if( !events.ContainsKey( eventName ) ) {
				Debug.Log( "SUBSCRIBER '" + transform.name + "' WANTS TO UNSUBSCRIBE UNEXISTING EVENT '" + eventName + "'" );
				return;
			}else{
				RemoveSubscriber( eventName , transform );
			}
		}else{
			foreach( KeyValuePair<string, List<Transform>> ev in events) {
				if( ev.Value.Contains( transform ) )
					RemoveSubscriberOnly( ev.Key , transform );
			}
			RemoveEmptyEvents();
		}
	}
	
	public void Trigger( string eventName , object[] pars ){
		if( !events.ContainsKey( eventName ) ){
			string p = "";
			for( int i = 0 ; i < pars.Length ; i++ ) p += pars[ i ] + " ";
			//Debug.Log( "YOU ARE TRYING TO TRIGGER NOT EXISTING EVENT '" + eventName + "' " + p);
			return;
		}
		
		
		Log( "Triggered event (" + eventName + ")" );
		
		List<Transform> listeners = events[ eventName ];
		for( int i = 0 ; i < listeners.Count ; i++ ){
			if( listeners[ i ] != null )
				listeners[ i ].SendMessage( "On" + eventName , pars , SendMessageOptions.DontRequireReceiver );
			else{
				listeners.RemoveAt( i );
				i--;
			}
		}
	}
}