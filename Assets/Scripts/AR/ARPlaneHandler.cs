using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneHandler : MonoBehaviour
{
    [SerializeField]
    ARPlaneManager _planeManager;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;

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
                if (floor == null || plane.gameObject.transform.position.y < floor.gameObject.transform.position.y)
                {
                    floor = plane;
                    rend.material = green;
                }
                else
                {
                    rend.material = red;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}
