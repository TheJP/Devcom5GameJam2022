using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class ColourTilemap
{
    [field: SerializeField]
    public LightColour Colour { get; private set; }

    [field: SerializeField]
    public TilemapCollider2D TilemapCollider { get; private set; }
}

public class TilemapManager : MonoBehaviour
{
    [SerializeField]
    private ColourTilemap[] tilemaps;

    public void ActivateColour(LightColour colour)
    {
        foreach (var tilemap in tilemaps)
        {
            tilemap.TilemapCollider.enabled = tilemap.Colour == colour;
        }
    }
}
