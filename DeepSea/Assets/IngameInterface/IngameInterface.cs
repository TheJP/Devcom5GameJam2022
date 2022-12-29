using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class IngameInterface : MonoBehaviour
{
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var restartLevel = uiDocument.rootVisualElement.Q<Button>("restart-level");
        restartLevel.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
