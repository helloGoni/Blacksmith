using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorizonScrollScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public Transform contentTransform;

    const int tabSize = 5;
    int targetIndex;
    float[] tabPos = new float[tabSize];
    float distance, curPos, targetPos;
    bool isDrag;

    void Start() {
        distance = 1f / (tabSize-1);
        for(int i = 0; i<tabSize;i++) {
            tabPos[i] = distance * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bottomTabSlider.value = scrollbar.value;
        if(!isDrag) {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

            for(int i=0;i<tabSize;i++) {
                tabBtnTranform[i].sizeDelta = new Vector2(i== targetIndex?360:180,tabBtnTranform[i].sizeDelta.y);
            }
        }
        if(Time.time<0.1f) return;

        for(int i = 0; i <tabSize; i++) {
            Vector3 BtnTargetPos = tabBtnTranform[i].anchoredPosition3D;
            Vector3 BtnTargetScale = Vector3.one;
            bool isTextActive = false;
            if(i == targetIndex) {
                BtnTargetPos.y =-23f;
                BtnTargetScale = new Vector3(1.2f,1.2f,1);
                isTextActive = true;
            }
            tabBtnImage[i].anchoredPosition3D = Vector3.Lerp(tabBtnImage[i].anchoredPosition3D, BtnTargetPos,0.25f);
            tabBtnImage[i].localScale = Vector3.Lerp(tabBtnImage[i].localScale, BtnTargetScale, 0.25f);
            tabBtnImage[i].transform.GetChild(0).gameObject.SetActive(isTextActive);
        }
    }

    float SetPos() {
        for(int i = 0; i<tabSize;i++) {
            if(scrollbar.value < tabPos[i] + distance * 0.5f && scrollbar.value > tabPos[i]-distance*0.5f) {
                targetIndex = i;
                return tabPos[i];
            }
        }
        return 0;
    }

    #region HorizonScroll
    public void OnBeginDrag(PointerEventData eventData) {
        curPos = SetPos();
    }
    public void OnDrag(PointerEventData eventData) {
        isDrag = true;
    }
    public void OnEndDrag(PointerEventData eventData) {
        isDrag = false;
        targetPos = SetPos();

        if(curPos == targetPos) {
            if(eventData.delta.x > 18 && curPos - distance >= 0) {
                --targetIndex;
                targetPos = curPos - distance;
            } else if (eventData.delta.x < -18 && curPos + distance <= 1.01f) {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
        for(int i = 0; i< tabSize ; i++) {
            if(contentTransform.GetChild(i).GetComponent<VerticalScrollScript>() && curPos != tabPos[i] && targetPos == tabPos[i]) {
                contentTransform.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
            }
        }
    }
    #endregion
    
    #region BottomTab

    public RectTransform[] tabBtnTranform, tabBtnImage;
    public Slider bottomTabSlider;

    public void ClickTab(int i) {
        targetIndex = i;
        targetPos = tabPos[i];
    }

    #endregion


}
