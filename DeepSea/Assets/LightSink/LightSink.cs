using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightSink : MonoBehaviour
{
    [SerializeField]
    private LightKind lightKind;

    [SerializeField]
    private bool hasLight;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    public bool HasLight => hasLight;
    public LightKind LightKind => lightKind;
    private int SpriteIndex => (hasLight, lightKind) switch
    {
        (false, _) => 0,
        (true, LightKind.Red) => 1,
        (true, LightKind.Green) => 2,
        (true, LightKind.Blue) => 3,
        _ => throw new InvalidOperationException(),
    };

    private void Start()
    {
        Debug.Assert(sprites.Length == 4);
    }

    private void Update()
    {
        spriteRenderer.sprite = sprites[SpriteIndex];
    }

    public void AddLight(LightKind lightKind)
    {
        Debug.Assert(!HasLight);
        hasLight = true;
        this.lightKind = lightKind;
    }
}
