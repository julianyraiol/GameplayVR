using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCanvas : MonoBehaviour {
    public float initPosY;
    public float angle;
    public Transform Target;
	// Use this for initialization
	void Start () {
        initPosY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localPosition;
        angle = Target.eulerAngles.x;
        if(angle > 180)
        {
            float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            //Debug.Log(cos);
            cos = cos/2 + 0.5f;
            transform.localPosition = new Vector3(pos.x, 50 * cos - 50, pos.z);
            pos = transform.localPosition;
            //transform.localPosition = new Vector3(pos.x, pos.y, initPosY);
        }
        Vector2 rot = transform.eulerAngles;
        //transform.eulerAngles = new Vector3(rot.x, rot.y, 0);
        //SphericalToCartesian(19, elevation, polar, out pos);
        //transform.position = pos;
	}

    public void CartesianToSpherical(Vector3 cartCoords, out float outRadius, out float outPolar, out float outElevation)
    {
        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;
        outRadius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
                        + (cartCoords.y * cartCoords.y)
                        + (cartCoords.z * cartCoords.z));
        outPolar = Mathf.Atan(cartCoords.z / cartCoords.x);
        if (cartCoords.x < 0)
            outPolar += Mathf.PI;
        outElevation = Mathf.Asin(cartCoords.y / outRadius);
    }

    public void SphericalToCartesian(float radius, float polar, float elevation, out Vector3 outCart)
    {
        float a = radius * Mathf.Cos(elevation);
        outCart.x = a * Mathf.Cos(polar);
        outCart.y = radius * Mathf.Sin(elevation);
        outCart.z = a * Mathf.Sin(polar);
    }
}
