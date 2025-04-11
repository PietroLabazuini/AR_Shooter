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
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ARPlaneHandler : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager _planeManager;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;
    [SerializeField]
    Material orange;
    [SerializeField]
    Material gray;
    [SerializeField]
    NavMeshHandler navMeshHandler;
    [SerializeField]
    GameObject navMeshPlane;

    ARPlane floor;

    Vector3 offset = new(0f, 0.05f, 0f);
    enum PlaneClassification
    {
        Floor,
        Wall,
        Obstacle,
        Unknown
    };

    void Start()
    {
        //_planeManager.planesChanged += OnPlanesUpdated;
    }

    void OnEnable()
    {
        _planeManager.planesChanged += OnPlanesUpdated;
    }

    void OnDisable()
    {
        _planeManager.planesChanged -= OnPlanesUpdated;
    }


    private void OnPlanesUpdated(ARPlanesChangedEventArgs args)
    {
        if (args != null)
        {
            foreach (ARPlane plane in args.added)
            {
                ProcessPlane(plane);
            }

            foreach (ARPlane plane in args.updated)
            {
                ProcessPlane(plane);
            }
        }
    }


    private void ProcessPlane(ARPlane plane)
    {
        if(plane.extents.x * plane.extents.y > 1f)
        AddPlaneInfo(plane);

        switch(ClassifyPlane(plane))
        {
            case PlaneClassification.Floor:
                floor = plane;
                if(plane.TryGetComponent<NavMeshObstacle>(out NavMeshObstacle obs))
                {
                    Destroy(obs);
                }
                floor.gameObject.layer = LayerMask.NameToLayer("Floor");
                break;
            case PlaneClassification.Obstacle:
                navMeshHandler.AddObstacle(plane.gameObject);
                break;
        }
    }

    void AddPlaneInfo(ARPlane plane)
    {
        Canvas planeDisplay = plane.GetComponentInChildren<Canvas>();
        Text planeInfo;

        if (planeDisplay == null)
        {
            GameObject canvasObject = new("Canvas");
            canvasObject.transform.SetParent(plane.transform);
            planeDisplay = canvasObject.AddComponent<Canvas>();
            planeDisplay.renderMode = RenderMode.WorldSpace;
            CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 10;

            GameObject textObject = new("Text");
            textObject.transform.SetParent(planeDisplay.transform);
            planeInfo = textObject.AddComponent<Text>();
            planeInfo.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            planeInfo.alignment = TextAnchor.MiddleCenter;
            planeInfo.color = Color.black;
            planeInfo.fontSize = 10;
            planeInfo.transform.localScale = new Vector3(0.01f, 0.01f, 1f);
            textObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            planeInfo = planeDisplay.GetComponentInChildren<Text>();
        }
        planeDisplay.gameObject.transform.position = plane.center + offset;

        if (plane.subsumedBy != null)
        {
            planeInfo.text = "";
        }
        else
        {
            planeInfo.text = String.Format("{0:0.00}mx{1:0.00}m", plane.extents.x, plane.extents.y);
        }
        
    }

    /* This function is to be improved for better spatial understading. The data of the ARPlane should be studied to determine different criterias for plane classification*/
    private PlaneClassification ClassifyPlane(ARPlane plane)
    {
        MeshRenderer rend = plane.gameObject.GetComponent<MeshRenderer>();
        PlaneClassification planeClass = PlaneClassification.Unknown;

        

        switch (plane.alignment)
        {
            case PlaneAlignment.HorizontalDown:
            case PlaneAlignment.HorizontalUp:
                //if(floor == null || Mathf.Abs(plane.transform.position.y - floor.transform.position.y) < 0.05f)
                if (plane.extents.x * plane.extents.y >= 2f)
                 {
                    planeClass = PlaneClassification.Floor;
                    rend.material = green;
                }
                else
                {
                    planeClass = PlaneClassification.Obstacle;
                    rend.material = red;
                }
                break;
            case PlaneAlignment.Vertical:
                //Should try to differentiate walls and vertical obstacles like chairs
                planeClass = PlaneClassification.Wall;
                rend.material = orange;
                break;
            default:
                rend.material = gray;
                break;
        }
        return planeClass;
    }

    
}
