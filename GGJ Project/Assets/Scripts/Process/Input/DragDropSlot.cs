﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    EventBase targetEvent;
    TileManager tileMgr;

    void Awake()
    {
        targetEvent = GetComponent<EventBase>();
        tileMgr = GameObject.FindObjectOfType<TileManager>();
    }

    private GameObject GetItem()
    {
        if (transform.childCount > 0)
            return transform.GetChild(0).gameObject;
        else
            return null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GetItem() == null)
        {
            if (Tile.CompareID(DragAndDrop.selectedObject.GetComponent<Tile>(), this.GetComponent<EventBase>()))
            {
                targetEvent.TileAttatched(DragAndDrop.selectedObject.GetComponent<Tile>());
                tileMgr.TileAttatched(DragAndDrop.selectedObject.GetComponent<Tile>().ID);
                DragAndDrop.selectedObject.transform.SetParent(transform);
                DragAndDrop.selectedObject.transform.localPosition = new Vector3(0, 0, -1);
            }
        }
    }
}