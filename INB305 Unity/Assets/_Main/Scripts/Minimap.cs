using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform center;
	public GameObject minimapObject;
	[System.Serializable]
	public class TrackedObjectPair{
		public Transform tracked, minimap;
		public TrackedObjectPair(Transform tracked, Transform minimap){
			this.tracked = tracked;
			this.minimap = minimap;
		}
	}
	public List<TrackedObjectPair> trackedObjects;
	public float transpose_scale;
		
	void Awake(){
		trackedObjects = new List<TrackedObjectPair>();
	}

	void Update(){
		Update_Minimap();
	}

	public void AddTrackedObject(Transform tracked){
		trackedObjects.Add(new TrackedObjectPair(tracked, Instantiate(minimapObject, this.transform).transform));
	}

	void Update_Minimap(){
		foreach(TrackedObjectPair objectpair in trackedObjects){
			objectpair.minimap.localPosition = (Vector3)(objectpair.tracked.position - center.position) * transpose_scale;
		}
	}
}
