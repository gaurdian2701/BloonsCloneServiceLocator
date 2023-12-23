using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform monkeyImageRectTransform;
        private Vector3 monkeyImageOriginalPos;
        private Vector3 monkeyImageOriginalAnchorPos;
        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }

        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            monkeyImageRectTransform = GetComponent<RectTransform>();
            monkeyImageOriginalPos = monkeyImageRectTransform.position;
            monkeyImageOriginalAnchorPos = monkeyImageRectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            monkeyImageRectTransform.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(monkeyImageRectTransform.position); //Calls the function to validate and give red/blue color to cells below
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SnapMonkeyImageToOriginalPos();
            owner.MonkeyDroppedAt(eventData.position);
        }

        private void SnapMonkeyImageToOriginalPos()
        {
            monkeyImageRectTransform.position = monkeyImageOriginalPos;
            monkeyImageRectTransform.anchoredPosition = monkeyImageOriginalAnchorPos;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
            monkeyImage.color = new Color(1, 1, 1, 1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
