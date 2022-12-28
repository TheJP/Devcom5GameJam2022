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
        move = value.Get<Vector2>().normalized;
        body.velocity = move * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var lightHolder = collision.GetComponent<LightHolder>();
        if (lightHolder != null)
        {
            LightHolderFound(lightHolder);
        }
    }

    private void LightHolderFound(LightHolder lightHolder)
    {
        if (!lightHolder.HasLight) { return; }
        var kind = lightHolder.RemoveLight();
        lightsManager.AddLight(kind);
    }
}
