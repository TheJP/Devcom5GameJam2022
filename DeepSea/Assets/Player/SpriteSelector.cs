using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LightsManager))]
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

    private Rigidbody2D body;
    private LightsManager lightsManager;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lightsManager = GetComponent<LightsManager>();
        Debug.Assert(front.Length == 8 && side.Length == 8 && back.Length == 8, "Invalid sprite settings");
    }

    private void Update()
    {
        var direction = body.velocity.normalized;
        var spriteIndex = (int)lightsManager.LightColour;
        Debug.Assert(spriteIndex >= 0 && spriteIndex < 8);

        if (Mathf.Abs(direction.x) > 0.1f)
        {
            spriteRenderer.sprite = side[spriteIndex];
            spriteRenderer.flipX = direction.x < 0f;
        }
        else if (direction.y > 0.1f)
        {
            spriteRenderer.sprite = back[spriteIndex];
        }
        else
        {
            spriteRenderer.sprite = front[spriteIndex];
        }
    }
}
