using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Credits : MonoBehaviour
{
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();

        var link = uiDocument.rootVisualElement.Q<Label>("link");
        link.RegisterCallback<ClickEvent>(e => Application.OpenURL("https://github.com/TheJP/Devcom5GameJam2022"));

        var home = uiDocument.rootVisualElement.Q<Button>("home");
        home.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene(0));
    }
}
