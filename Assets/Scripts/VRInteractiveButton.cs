using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VRStandardAssets.Utils;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class VRInteractiveButton : VRInteractiveItem {    
    VRInteractiveItem interact;
    public SelectionRadial selection;
    public UnityEvent OnSelectionEnd;
    Button btn;
    // Use this for initialization
    void Awake()
    {
        interact = GetComponent<VRInteractiveItem>();
        btn = GetComponent<Button>();
        selection = Camera.main.GetComponent<SelectionRadial>();
    }

    public void OnEnable()
    {
        //interact.OnClick += NewScene;
        interact.OnOver += HandleOver;
        interact.OnOut += HandleOut;
    }

    public void OnDisable()
    {
        //interact.OnClick -= NewScene;
        interact.OnOver -= HandleOver;
        interact.OnOut -= HandleOut;
    }

    private void HandleOver()
    {
        // When the user looks at the rendering of the scene, show the radial.
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
        selection.OnSelectionComplete += OnSelectionComplete;
        selection.Show();
        selection.HandleDown();
        //m_GazeOver = true;
    }


    private void HandleOut()
    {
        // When the user looks away from the rendering of the scene, hide the radial.
        ExecuteEvents.Execute(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        selection.OnSelectionComplete -= OnSelectionComplete;
        selection.HandleUp();
        selection.ResetFill();
        //m_GazeOver = false;
    }

    private void OnSelectionComplete()
    {
        OnSelectionEnd.Invoke();
    }

}
