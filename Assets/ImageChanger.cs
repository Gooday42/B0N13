using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlitchImageChanger : MonoBehaviour
{
    public Image targetImage; // The UI Image component
    public Sprite[] glitchSprites; // Array of sprites to flash through
    public float glitchDuration = 0.5f; // How long the glitch effect lasts
    public float glitchInterval = 0.05f; // Time between sprite changes

    private Sprite originalSprite;

    void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        originalSprite = targetImage.sprite;
    }

    public void StartGlitch()
    {
        StartCoroutine(GlitchEffect());
    }

    IEnumerator GlitchEffect()
    {
        float endTime = Time.time + glitchDuration;

        while (Time.time < endTime)
        {
            targetImage.sprite = glitchSprites[Random.Range(0, glitchSprites.Length)];
            yield return new WaitForSeconds(glitchInterval);
        }

        targetImage.sprite = originalSprite;
    }
}
