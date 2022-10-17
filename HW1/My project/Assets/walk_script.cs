using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class walk_script : MonoBehaviour
{

    public GameObject vbBtndummy;
    public Animator walkanimation;

    // Use this for initialization
    void Start()
    {
        vbBtndummy = GameObject.Find("VirtualButton");
        vbBtndummy.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtndummy.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        walkanimation.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        walkanimation.Play("Walk");
        Debug.Log("Button pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        walkanimation.Play("None");
        Debug.Log("Button released");
    }
}