using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LightManagerColour
{
    [field: SerializeField]
    public LightColour Colour { get; private set; }

    [field: SerializeField]
    public Camera Camera { get; private set; }
}

public class LightsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject renderTarget;

    [SerializeField]
    private LightManagerColour[] colours;

    private TilemapManager tilemapManager;

    private LightColour lights = LightColour.Black;

    private void Start()
    {
        tilemapManager = FindObjectOfType<TilemapManager>();
        Debug.Assert(tilemapManager != null);
    }

    internal void AddLight(LightKind kind)
    {
        var newLights = lights | (LightColour)kind;
        if (newLights != lights)
        {
            tilemapManager.ActivateColour(newLights);
            var oldColour = colours.FirstOrDefault(c => c.Colour == lights);
            if (oldColour != null)
            {
                oldColour.Camera.transform.gameObject.SetActive(false);
            }

            var newColour = colours.FirstOrDefault(s => s.Colour == newLights);
            if (newColour != null)
            {
                newColour.Camera.transform.gameObject.SetActive(true);
            }

            renderTarget.SetActive(newLights != LightColour.Black);
        }
        lights = newLights;
    }
}
