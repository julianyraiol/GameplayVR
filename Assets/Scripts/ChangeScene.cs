using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public VRCameraFade fade;
    void Start()
    {
        fade = GameObject.FindObjectOfType<VRCameraFade>();
    }

    public void NewScene(string scene)
    {
        StartCoroutine(NewSceneCoroutine(scene));
    }

    IEnumerator NewSceneCoroutine(string scene)
    {
        yield return StartCoroutine(fade.BeginFadeOut(0.5f, true));
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

    }
    
}
