using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponsManager : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        int nextIndex = (currIndex + 1) % guns.Length;
        Debug.Log(nextIndex +" "+ guns.Length);
        if (guns[nextIndex] != null)
        {
            guns[currIndex].gameObject.SetActive(false);
            guns[nextIndex].gameObject.SetActive(true);
            currIndex = nextIndex;
        }
    }
}
