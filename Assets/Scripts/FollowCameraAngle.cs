using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraAngle : MonoBehaviour {
    public Transform MainCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(-MainCamera.rotation.eulerAngles.x, MainCamera.rotation.eulerAngles.y, 0);
	}
}
