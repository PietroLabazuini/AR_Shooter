using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
=======
>>>>>>> 8a785dc6aaf4b9bbe9bd7a4454322d6d9a38df94
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
<<<<<<< HEAD

    ARPlane floor;
=======
>>>>>>> 8a785dc6aaf4b9bbe9bd7a4454322d6d9a38df94
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
<<<<<<< HEAD
                if (floor == null || plane.gameObject.transform.position.y < floor.gameObject.transform.position.y)
                {
                    floor = plane;
=======
                if (plane.classification == PlaneClassification.Floor)
                {
                    
>>>>>>> 8a785dc6aaf4b9bbe9bd7a4454322d6d9a38df94
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
