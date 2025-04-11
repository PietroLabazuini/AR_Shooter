using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.AI.Navigation;
using UnityEngine.UI;
using System;

public class AddTextToPlane : MonoBehaviour
{
    [SerializeField]
    GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        AddText();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void AddText()
    {
        Canvas planeDisplay = plane.GetComponentInChildren<Canvas>();
        Text planeInfo;

        if (planeDisplay == null)
        {
            GameObject canvasObject = new GameObject("Canvas");
            canvasObject.transform.SetParent(plane.transform);
            planeDisplay = canvasObject.AddComponent<Canvas>();
            planeDisplay.renderMode = RenderMode.WorldSpace;
            CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 10;

            GameObject textObject = new GameObject("Text");
            textObject.transform.SetParent(planeDisplay.transform);
            planeInfo = textObject.AddComponent<Text>();
            planeInfo.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            planeInfo.alignment = TextAnchor.MiddleCenter;
            planeInfo.color = Color.black;
            planeInfo.fontSize = 4;
            planeInfo.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            textObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else 
        {
            planeInfo = planeDisplay.GetComponentInChildren<Text>();
        }
        planeDisplay.gameObject.transform.localPosition = new Vector3(0f,0.1f,0f);
        planeInfo.text = String.Format("{0}mx{1}m", 2, 3);
        
    }
}
