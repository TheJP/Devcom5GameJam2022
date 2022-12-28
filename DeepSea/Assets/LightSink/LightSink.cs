using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSink : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer lightBlob;

    [SerializeField]
    private LightKind lightKind;

    public bool HasLight => lightBlob.gameObject.activeSelf;
    public LightKind LightKind => lightKind;

    private Dictionary<LightKind, Color> colourMap = new()
    {
        { LightKind.Red, Color.red },
        { LightKind.Green, Color.green },
        { LightKind.Blue, Color.blue },
    };

    public void AddLight(LightKind lightKind)
    {
        Debug.Assert(!HasLight);
        lightBlob.gameObject.SetActive(true);
        lightBlob.color = colourMap[lightKind];
        lightBlob.color = new Color(lightBlob.color.r, lightBlob.color.g, lightBlob.color.b, 0.6666667f);
        this.lightKind = lightKind;
    }
}
