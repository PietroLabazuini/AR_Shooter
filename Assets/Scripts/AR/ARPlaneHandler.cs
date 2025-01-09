using System;
using System.Collections;
using System.Collections.Generic;
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
                if (plane.classification == PlaneClassification.Floor)
                {
                    
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
