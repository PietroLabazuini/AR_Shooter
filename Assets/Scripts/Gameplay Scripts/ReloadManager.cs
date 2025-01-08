using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    Gun[] guns;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer click = "+ Time.frameCount);
        foreach (var gun in guns)
        {
            if (gun != null && gun.isActiveAndEnabled) 
            { 
                gun.TriggerReload();
            }
        }
    }
}
