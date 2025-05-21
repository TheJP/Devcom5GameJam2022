using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class IngameInterface : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    private UIDocument uiDocument;

    private InputAction menuAction;
    private InputAction submitAction;

    private Button home;
    private Button restartLevel;

    public bool MenuOpen { get; private set; } = false;

    private void Awake()
    {
        menuAction = inputActions.FindActionMap("UI").FindAction("Menu");
        submitAction = inputActions.FindActionMap("UI").FindAction("Submit");
    }

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        restartLevel = uiDocument.rootVisualElement.Q<Button>("restart-level");
        restartLevel.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));

        home = uiDocument.rootVisualElement.Q<Button>("home");
        home.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene(0));
    }

    private void OnEnable()
    {
        menuAction.Enable();
        submitAction.Enable();

        menuAction.performed += OnMenu;
        submitAction.performed += OnSubmit;
    }

    private void OnDisable()
    {
        menuAction.Disable();
        submitAction.Disable();

        menuAction.performed -= OnMenu;
        submitAction.performed -= OnSubmit;
    }

    private void CloseMenu()
    {
        MenuOpen = false;

        var root = uiDocument.rootVisualElement.Q<VisualElement>("root");
        root.RemoveFromClassList("open");
        root.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);

        home.focusable = false;
        restartLevel.focusable = false;
        root.Focus();
    }

    private void OnMenu(InputAction.CallbackContext context)
    {
        MenuOpen = !MenuOpen;

        if (MenuOpen)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>("root");
            root.AddToClassList("open");
            root.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);

            restartLevel.focusable = true;
            home.focusable = true;
            restartLevel.Focus();

        }
        else
        {
            CloseMenu();
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        var root = uiDocument.rootVisualElement.Q<VisualElement>("root");
        var focused = root.focusController.focusedElement;

        using var clickEvent = new ClickEvent();
        clickEvent.target = focused;
        root.SendEvent(clickEvent);
    }
}
