using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class RoomManager : MonoBehaviour
{
    private List<Renderer> meshesToToggle = new List<Renderer>();
    public bool active = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateRoom(true);
            
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateRoom(false);
        }
    }
    private void Start()
    {
        foreach (Transform child in transform)
        {
            meshesToToggle.Add(child.gameObject.GetComponent<Renderer>());
        }
        UpdateRoom(active);
    }
    /* private void ToggleHallwayMasks(bool isVisible)
     {
         HallwayMask[] masks = GetComponentsInChildren<HallwayMask>();
         foreach (HallwayMask mask in masks)
         {
             mask.ToggleVisibility(isVisible);
         }
     }*/
    private void UpdateRoom(bool active)
    {
        foreach(Renderer obj in meshesToToggle)
        {
            obj.enabled = active;
        }
        this.active = active;
    }
  
}

