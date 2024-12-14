using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Gun[] guns;

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var gun in guns)
        {
            if (gun != null && gun.isActiveAndEnabled) 
            { 
                gun.Reload();
            }
        }
    }
}
