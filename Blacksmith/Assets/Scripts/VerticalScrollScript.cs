using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VerticalScrollScript : ScrollRect {

    bool isParent;
    HorizonScrollScript BT;
    ScrollRect parentScrollRect;

    protected override void Start() {
        BT = GameObject.FindWithTag("ScrollManager").GetComponent<HorizonScrollScript>();
        parentScrollRect = GameObject.FindWithTag("ScrollManager").GetComponent<ScrollRect>();
    }


    public override void OnBeginDrag(PointerEventData eventData) {
        isParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);
        if(isParent) {
            BT.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        } else 
            base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData) {
        if(isParent) {
            BT.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        } else 
            base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData) {
        if(isParent) {
            BT.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        } else 
            base.OnEndDrag(eventData);
    }
}

