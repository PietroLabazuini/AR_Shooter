using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponsManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    Gun[] guns;
    int currIndex = 0;

    public void Start()
    {
        if (guns != null) 
        {
            guns[currIndex].gameObject.SetActive(true);
        }
        for (int i = 1; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int nextIndex = (currIndex + 1) % guns.Length;
        if (guns[nextIndex] != null)
        {
            guns[currIndex].Unequip();
            guns[nextIndex].gameObject.SetActive(true);
            guns[nextIndex].Equip();
            currIndex = nextIndex;
        }
    }
}
