using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    WeaponsManager _weaponsManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Switching...");
        _weaponsManager.SwitchGun();
    }
}
