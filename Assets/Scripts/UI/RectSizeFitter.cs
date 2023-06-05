using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SideScroll.UI
{
    // 스크린 사이즈에 맞춰 UI가 화면 안에서만 표시되도록 Rect를 맞춥니다.
    
    [RequireComponent(typeof(RectTransform))]
    public class RectSizeFitter : MonoBehaviour
    {
        [SerializeField] private RectTransform rectToFit = null;

        private void Reset()
        {
            rectToFit = gameObject.GetComponent<RectTransform>();
        }

        private void Awake()
        {
            ChangeRectSize(Screen.safeArea);
        }

        private void OnRectTransformDimensionsChange()
        {
            ChangeRectSize(Screen.safeArea);
        }

        private void ChangeRectSize(Rect toChange)
        {
            Vector2 minAnchor = toChange.position;
            Vector2 maxAnchor = minAnchor + toChange.size;

            Vector2 curScreenSize = new Vector2(Screen.width, Screen.height);
            
            minAnchor /= curScreenSize;
            maxAnchor /= curScreenSize;

            rectToFit.anchorMin = minAnchor;
            rectToFit.anchorMax = maxAnchor;
        }
    }
}
