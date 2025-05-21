using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ResetOnInactivity : MonoBehaviour
{
    [SerializeField]
    private float maxSecondsOfInactivity = 180f;

    [SerializeField]
    private float secondsOfWarning = 30f;

    private float timeSinceLastInput = 0f;
    private UIDocument uiDocument;
    private VisualElement root;
    private Label countdown;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement.Q("root");
        countdown = uiDocument.rootVisualElement.Q<Label>("countdown");
    }

    private void Update()
    {
        timeSinceLastInput += Time.deltaTime;
        if (Input.anyKey)
        {
            timeSinceLastInput = 0;
        }

        if (timeSinceLastInput >= maxSecondsOfInactivity)
        {
            SceneManager.LoadScene(0);
        }

        float secondsRemaining = maxSecondsOfInactivity - timeSinceLastInput;
        bool openWarning = secondsRemaining <= secondsOfWarning;
        root.style.display = new StyleEnum<DisplayStyle>(openWarning ? DisplayStyle.Flex : DisplayStyle.None);
        if (openWarning)
        {
            countdown.text = $"{secondsRemaining:F1}s";
        }
    }
}
