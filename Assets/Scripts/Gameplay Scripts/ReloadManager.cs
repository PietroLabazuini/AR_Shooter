using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    WeaponsManager _weaponsManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Reloading...");
        _weaponsManager.GetCurrentGun().TriggerReload();
    }
}
