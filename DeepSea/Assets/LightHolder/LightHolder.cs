using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject lightBlob;

    [SerializeField]
    private LightKind lightKind;

    public bool HasLight => lightBlob.activeSelf;
    public LightKind LightKind => lightKind;

    public LightKind RemoveLight()
    {
        Debug.Assert(HasLight);
        lightBlob.SetActive(false);
        return lightKind;
    }
}
