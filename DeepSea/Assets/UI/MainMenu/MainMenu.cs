using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float antiBurnInInterval = 10.0f;

    private UIDocument uiDocument;
    private VisualElement antiBurnIn;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        antiBurnIn = uiDocument.rootVisualElement.Q("anti-burn-in");
        StartCoroutine(AntiBurnInFlicker());
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator AntiBurnInFlicker()
    {
        while (true)
        {
            antiBurnIn.style.backgroundColor = Random.ColorHSV();
            antiBurnIn.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            yield return new WaitForSeconds(0.5f);
            antiBurnIn.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            yield return new WaitForSeconds(antiBurnInInterval);
        }
    }
}
