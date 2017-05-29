using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using VRStandardAssets.Utils;

public class CameraController : MonoBehaviour {
    Vector2 StartMouseDown;
    Vector2 ActualMousePos;
    bool IsMoving;
    public float velocity;
    public static bool isVr;
    public GameObject Button;
	// Use this for initialization
	void Start () {
        isVr = false;
        if (!VRSettings.enabled)
        {
            //GetComponent<Reticle>().enabled = false;
            //GetComponent<VREyeRaycaster>().enabled = false;
            //GetComponent<VRCameraUI>().enabled = false;
            //transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //Button.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!VRSettings.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsMoving = true;
                StartMouseDown = Input.mousePosition;
                SendRaycast(StartMouseDown);
            }
            if (Input.GetMouseButton(0))
            {
                ActualMousePos = StartMouseDown - (Vector2)Input.mousePosition;
                StartMouseDown = Input.mousePosition;
                transform.Rotate(new Vector3(ActualMousePos.y * velocity, -ActualMousePos.x * velocity, 0));
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }
            if (Input.GetMouseButtonUp(0))
            {
                IsMoving = false;
            }
        }
        
        if (Input.GetButtonUp("Cancel") && !VRSettings.enabled)
        {
            //StartCoroutine(LoadDevice("Oculus"));
            //OpenVR();
            StartCoroutine(Exit());
        }
        
	}

    IEnumerator Exit()
    {
        yield return GetComponent<VRCameraFade>().BeginFadeOut(true);
        Application.Quit();
    }

    void SendRaycast(Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            try
            {
                hit.collider.GetComponent<VRInteractiveButton>().OnSelectionEnd.Invoke();
            }catch
            {

            }
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        VRSettings.LoadDeviceByName(newDevice);
        yield return null;
        VRSettings.enabled = true;
        Debug.Log(VRSettings.loadedDeviceName);
    }

    public void OpenVR()
    {
        bool fail = false;
        string bundleId = "com.Ocean.CMM360VR"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            Application.OpenURL("https://google.com");
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }
}
