using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class FixedDistance : MonoBehaviour {
    public float MinDistance;
    public Transform target;
	// Use this for initialization
	void Start () {
        //target = Camera.main.transform;
    }
	// Update is called once per frame
	void Update () {
        transform.LookAt(2 * transform.position - target.position);
    }
}
