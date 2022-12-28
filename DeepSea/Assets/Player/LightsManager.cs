using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    const int LayerOffset = 16;

    [SerializeField]
    private Camera lightmapCamera;

    [SerializeField]
    private GameObject renderTarget;

    private TilemapManager tilemapManager;

    private LightColour lights = LightColour.Black;
    private Stack<LightKind> lightOrder = new();
    private Dictionary<LightColour, Color> colourMap = new()
    {
        { LightColour.Black, Color.black },
        { LightColour.Red, Color.red },
        { LightColour.Green, Color.green },
        { LightColour.Blue, Color.blue },
        { LightColour.Yellow, Color.yellow },
        { LightColour.Pink, new Color(1, 0, 1) },
        { LightColour.Cyan, new Color(0, 1, 1) },
        { LightColour.White, Color.white },
    };

    private void Start()
    {
        tilemapManager = FindObjectOfType<TilemapManager>();
        Debug.Assert(tilemapManager != null);
    }

    public bool IsLayerVisible(int layer) => layer == 0 || layer == LayerOffset + (int)lights;

    private void ChangeLight(LightColour newLights)
    {
        if (newLights != lights)
        {
            tilemapManager.ActivateColour(newLights);

            lightmapCamera.cullingMask = 1 << (LayerOffset + (int)newLights);
            lightmapCamera.backgroundColor = colourMap[newLights];

            renderTarget.SetActive(newLights != LightColour.Black);
            lightmapCamera.gameObject.SetActive(newLights != LightColour.Black);
        }
        lights = newLights;
    }

    public void PushLight(LightKind kind)
    {
        lightOrder.Push(kind);
        ChangeLight(lights | (LightColour)kind);
    }

    public LightKind PopLight()
    {
        var removedLight = lightOrder.Pop();
        var newLights = lights & (~(LightColour)removedLight);
        ChangeLight(newLights);
        return removedLight;
    }
}
