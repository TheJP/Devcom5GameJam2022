using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class LevelCompleteBanner : MonoBehaviour
{
    [SerializeField]
    private float sizeChangeSpeed = 1f;

    [SerializeField]
    private float opacityChangeSpeed = 1f;

    private float initialHeight = 0f;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var colouredBox = uiDocument.rootVisualElement.Q<VisualElement>("colour-box");
        void onGeometryChanged(GeometryChangedEvent e)
        {
            initialHeight = e.newRect.height;
            colouredBox.UnregisterCallback<GeometryChangedEvent>(onGeometryChanged); // unregister self

            StartCoroutine(AnimateBanner(uiDocument, colouredBox));
        }
        colouredBox.RegisterCallback<GeometryChangedEvent>(onGeometryChanged);
    }

    private IEnumerator AnimateBanner(UIDocument uiDocument, VisualElement colouredBox)
    {
        var bannerText = uiDocument.rootVisualElement.Q<Label>("banner-text");
        bannerText.style.opacity = 0f;

        float height = 0;
        while (height < initialHeight)
        {
            height += sizeChangeSpeed * Time.deltaTime;
            colouredBox.style.height = Mathf.Min(height, initialHeight);
            yield return null;
        }

        float opacity = 0f;
        while (opacity < 1f)
        {
            opacity += opacityChangeSpeed * Time.deltaTime;
            bannerText.style.opacity = Mathf.Min(opacity, 1f);
            yield return null;
        }
    }
}
