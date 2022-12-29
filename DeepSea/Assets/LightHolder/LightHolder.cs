using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightHolder : MonoBehaviour
{
    [SerializeField]
    private LightKind lightKind;

    [SerializeField]
    private bool hasLight;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] spritesEmpty;

    [SerializeField]
    private Sprite[] spritesLight;

    public bool HasLight => hasLight;
    public LightKind LightKind => lightKind;

    private int LightIndex => LightKind switch
    {
        LightKind.Red => 0,
        LightKind.Green => 1,
        LightKind.Blue => 2,
        _ => throw new InvalidOperationException(),
    };

    private void Start()
    {
        Debug.Assert(spritesEmpty.Length == 3 && spritesLight.Length == 3);
    }

    private void Update()
    {
        spriteRenderer.sprite = hasLight ? spritesLight[LightIndex] : spritesEmpty[LightIndex];
    }

    public LightKind RemoveLight()
    {
        Debug.Assert(HasLight);
        hasLight = false;
        return lightKind;
    }

    public void AddLight()
    {
        Debug.Assert(!HasLight);
        hasLight = true;
    }
}
