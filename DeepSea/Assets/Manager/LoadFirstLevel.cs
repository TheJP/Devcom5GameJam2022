using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstLevel : MonoBehaviour
{
    [SerializeField]
    private float animationStepSeconds = 0.2f;

    [SerializeField]
    private float scaleAnimationSeconds = 1f;

    [SerializeField]
    private float fadeAnimationSeconds = 1f;

    [SerializeField]
    private Camera animationCamera;

    [SerializeField]
    private SpriteRenderer foreground;

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private Sprite[] animationSprites;

    [SerializeField]
    private Vector2 targetScale = new Vector2(0.2f, 0.2f);

    private IEnumerator Start()
    {
        // Disable player
        var player = FindObjectOfType<Player>();
        player.Started = false;

        foreground.sprite = animationSprites[0];

        // Animate Fish Blinking
        var startTime = Time.time;
        var foregroundStart = new Color(1, 1, 1, 0);
        var foregroundTarget = Color.white;
        background.color = Color.white;
        for (int i = 1; i < animationSprites.Length; ++i)
        {
            startTime = Time.time;
            background.sprite = foreground.sprite;
            foreground.sprite = animationSprites[i];
            while (Time.time - startTime < animationStepSeconds)
            {
                var t = (Time.time - startTime) / animationStepSeconds;
                foreground.color = Color.Lerp(foregroundStart, foregroundTarget, t);
                yield return null;
            }
        }
        foreground.color = foregroundTarget;

        // Scale Fish to Proper Size
        startTime = Time.time;
        background.color = Color.black;
        var startScale = foreground.transform.localScale;
        var targetScale = new Vector3(this.targetScale.x, this.targetScale.y, startScale.z);
        while (Time.time - startTime < scaleAnimationSeconds)
        {
            var t = (Time.time - startTime) / scaleAnimationSeconds;
            foreground.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        foreground.transform.localScale = targetScale;

        // Fade From Animation to Game
        startTime = Time.time;
        foregroundStart = foreground.color;
        var backgroundStart = background.color;
        var targetColour = new Color(0, 0, 0, 0);
        while (Time.time - startTime < fadeAnimationSeconds)
        {
            var t = (Time.time - startTime) / fadeAnimationSeconds;
            background.color = Color.Lerp(backgroundStart, targetColour, t);
            foreground.color = Color.Lerp(foregroundStart, targetColour, t);
            yield return null;
        }
        foreground.color = targetColour;
        background.color = targetColour;

        player.Started = true;
    }
}
