using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LightsManager))]
[RequireComponent(typeof(UIDocument))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [field: SerializeField]
    public bool Started { get; set; } = true;

    private Rigidbody2D body;
    private LightsManager lightsManager;

    private Vector2 move = Vector2.zero;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lightsManager = GetComponent<LightsManager>();

        var uiDocument = GetComponent<UIDocument>();

        (string name, Vector2 movement)[] buttons = new[]
        {
            ("up", new Vector2(0, 1)),
            ("right", new Vector2(1, 0)),
            ("down", new Vector2(0, -1)),
            ("left", new Vector2(-1, 0))
        };

        foreach (var definition in buttons)
        {
            var button = uiDocument.rootVisualElement.Q<Button>(definition.name);
            button.clickable.activators.Clear();
            button.RegisterCallback<MouseDownEvent>(e => Move(definition.movement));
            button.RegisterCallback<MouseUpEvent>(e => Move(0, 0));
        }
    }

    private void FixedUpdate()
    {
        body.velocity = move * speed;
    }

    public void OnMove(InputValue value) => Move(value.Get<Vector2>());

    private void Move(float x, float y) => Move(new Vector2(x, y));

    private void Move(Vector2 move)
    {
        if (!Started)
        {
            return;
        }

        this.move = move.normalized;
        body.velocity = move * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!lightsManager.IsLayerVisible(collision.gameObject.layer)) { return; }

        var lightHolder = collision.GetComponent<LightHolder>();
        if (lightHolder != null) { LightHolderFound(lightHolder); }

        var lightSink = collision.GetComponent<LightSink>();
        if (lightSink != null) { LightSinkFound(lightSink); }

        var target = collision.GetComponent<Target>();
        if (target != null) { TargetFound(target); }
    }

    private void LightHolderFound(LightHolder lightHolder)
    {
        if (lightHolder.HasLight)
        {
            var kind = lightHolder.RemoveLight();
            lightsManager.PushLight(kind);
        }
        else if (lightsManager.TryRemove(lightHolder.LightKind))
        {
            lightHolder.AddLight();
        }
    }

    private void LightSinkFound(LightSink lightSink)
    {
        if (lightSink.HasLight) { return; }
        var kind = lightsManager.PopLight();
        lightSink.AddLight(kind);
    }

    private void TargetFound(Target target)
    {
        var levelComplete = FindObjectOfType<LevelCompleteBanner>(true);
        Debug.Assert(levelComplete != null);
        levelComplete.Done += () => FindObjectOfType<LevelManager>()?.NextLevel();
        levelComplete.gameObject.SetActive(true);
    }
}
