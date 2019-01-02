using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;
    GameObject tempImage;
    RectTransform tempRT;
    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        tempImage = new GameObject("icon");

        tempImage.transform.SetParent(canvas.transform, false);
        tempImage.transform.SetAsLastSibling();

        var image = tempImage.AddComponent<Image>();

        var group = tempImage.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            tempRT = transform as RectTransform;
        else
            tempRT = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (tempImage != null)
            SetDraggedPosition(eventData);
        //GetComponent<Image>().sprite = null;
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        //if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        //	tempRT = eventData.pointerEnter.transform as RectTransform;

        var rt = tempImage.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(tempRT, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
             rt.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject icon = GameObject.Find("icon");
        if (!eventData.pointerCurrentRaycast.gameObject || eventData.pointerCurrentRaycast.gameObject == eventData.pointerPressRaycast.gameObject)
        {
            //GetComponent<Image>().sprite = tempImage.gameObject.GetComponent<Image>().sprite;
            Destroy(tempImage.gameObject);
            return;
        }
        Sprite tempSprite = null;

        //把目标图像给一个临时Sprite
        //把tempImage的图像给目标图像
        //再把这个临时Sprite图像给刚开始的图像
        IsDrag isd = eventData.pointerCurrentRaycast.gameObject.GetComponent<IsDrag>();
        if (isd != null)
        {
            tempSprite = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;
            eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite = tempImage.gameObject.GetComponent<Image>().sprite;
            //GetComponent<Image>().sprite = tempSprite;
            if (tempImage)
            {
                Destroy(tempImage.gameObject);
            }
        }
        else
        {
            Destroy(tempImage.gameObject);
        }
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        var t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
