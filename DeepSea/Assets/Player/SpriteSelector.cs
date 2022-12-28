using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(LightsManager))]
public class SpriteSelector : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] front;

    [SerializeField]
    private Sprite[] side;

    [SerializeField]
    private Sprite[] back;

    private Rigidbody2D rigidbody;
    private LightsManager lightsManager;

    private void Update()
    {

    }
}
