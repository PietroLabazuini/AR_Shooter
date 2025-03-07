using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.AI.Navigation;
using System;

public class ARPlaneHandler : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager _planeManager;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;

    NavMeshHandler navMeshHandler;

    ARPlane floor;
    // Start is called before the first frame update
    void Start()
    {
        _planeManager.planesChanged += OnPlanesUpdated;
    }

    private void OnPlanesUpdated(ARPlanesChangedEventArgs args)
    {
        if (args != null)
        {
            foreach (ARPlane plane in args.added) 
            {
                MeshRenderer rend = plane.gameObject.GetComponent<MeshRenderer>();

                if(ClassifyPlane(plane)== PlaneClassification.Floor)
                {
                    if (floor != null)
                    {
                        navMeshHandler.RemoveNavMesh(floor.GetComponent<NavMeshSurface>());
                        floor.GetComponent<MeshRenderer>().material = red;
                    }

                    floor = plane;

                    NavMeshSurface navMeshSurface = floor.AddComponent<NavMeshSurface>();
                    navMeshHandler.AddNavMesh(navMeshSurface);
                    rend.material = green;
                }
                else
                {
                    rend.material = red;
                }
            }
        }
    }

    /* This function is to be improved for better spatial understading. The data of the ARPlane should be studied to determine different criterias for plane classification*/
    private PlaneClassification ClassifyPlane(ARPlane plane)
    {
        PlaneClassification planeClass = PlaneClassification.Unknown;

        switch (plane.alignment)
        {
            case PlaneAlignment.HorizontalUp:
                if(floor == null || (plane.gameObject.transform.position.y < floor.gameObject.transform.position.y))
                {
                    planeClass = PlaneClassification.Floor;
                }
                else
                {
                    planeClass = PlaneClassification.Obstacle;
                }
                break;
            case PlaneAlignment.Vertical:
                //Should try to differentiate walls and vertical obstacles like chairs
                planeClass = PlaneClassification.Wall;
                break;
            default:
                break;
        }
        return planeClass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum PlaneClassification
    {
        Floor,
        Wall,
        Obstacle,
        Unknown
    };
}
