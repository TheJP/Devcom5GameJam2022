using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LightsManager))]
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
    }

    private void FixedUpdate()
    {
        body.velocity = move * speed;
    }

    public void OnMove(InputValue value)
    {
        if (!Started)
        {
            return;
        }

        move = value.Get<Vector2>().normalized;
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
