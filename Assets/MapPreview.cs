using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPreview : MonoBehaviour
{
    private List<GameObject> maps = new List<GameObject>();
    private int currentMapIndex = 0;

    [SerializeField] private float switchInterval = 2f;
    [SerializeField] private float fadeDuration = 1f; // Duration for fade-in and fade-out
    [SerializeField] private float chaosFactor = 0.5f; // Factor for random fluctuations
    [SerializeField] private float peakTransparency = 1f; // Peak transparency value at the end of the fade

    private void Start()
    {
        SwitchMap switchMapComponent = FindObjectOfType<SwitchMap>();
        if (switchMapComponent == null)
        {
            Debug.LogError("No SwitchMap component found on the GameObject.");
            return;
        }

        foreach (GameObject map in switchMapComponent.maps)
        {
            if (map != null)
            {
                GameObject instantiatedMap = Instantiate(map, transform); // Set parent to preserve hierarchy
                //En caso de usar shaders se debe cambiar el material de la instancia
                //Pd: Euler alejate de mi paloma xd

                instantiatedMap.transform.position = Vector3.zero;   // Prevent compression issue
                instantiatedMap.SetActive(false);
                maps.Add(instantiatedMap);
            }
            else
            {
                Debug.LogWarning("Null map found in SwitchMap component.");
            }
        }

        foreach (GameObject map in maps)
        {
            Collider2D collider = map.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        if (maps.Count > 0)
        {
            maps[0].SetActive(true);
            AdjustSortingLayers();
            StartCoroutine(SwitchMapsCoroutine());
        }
        else
        {
            Debug.LogError("No maps were added to the MapPreview.");
        }
    }

    private IEnumerator SwitchMapsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            // Fade out the current map
            StartCoroutine(FadeOut(maps[currentMapIndex]));

            // Switch to the next map
            currentMapIndex = (currentMapIndex + 1) % maps.Count;
            maps[currentMapIndex].SetActive(true);

            // Fade in the new map
            StartCoroutine(FadeIn(maps[currentMapIndex]));
        }
    }

    private IEnumerator FadeIn(GameObject map)
    {
        TilemapRenderer renderer = map.GetComponent<TilemapRenderer>();
        if (renderer == null)
        {
            Debug.LogError("The map does not have a TilemapRenderer attached.");
            yield break;
        }

        Material material = renderer.material;
        Color originalColor = material.color;
        originalColor.a = 0f; // Start with fully transparent
        material.color = originalColor;

        float timeElapsed = 0f;
        float alpha = 0f;

        // Chaotic fade-in (from 0 to peakTransparency)
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;

            // Add random fluctuation to alpha for chaos
            alpha = Mathf.Lerp(0f, peakTransparency, timeElapsed / fadeDuration) + Random.Range(-chaosFactor, chaosFactor);
            alpha = Mathf.Clamp01(alpha); // Ensure the alpha stays between 0 and peakTransparency

            originalColor.a = alpha;
            material.color = originalColor;
            yield return null;
        }

        // Ensure the map reaches the peakTransparency value at the end
        originalColor.a = peakTransparency;
        material.color = originalColor;
    }

    private IEnumerator FadeOut(GameObject map)
    {
        TilemapRenderer renderer = map.GetComponent<TilemapRenderer>();
        if (renderer == null)
        {
            Debug.LogError("The map does not have a TilemapRenderer attached.");
            yield break;
        }

        Material material = renderer.material;
        Color originalColor = material.color;
        originalColor.a = peakTransparency; // Start from peakTransparency
        material.color = originalColor;

        float timeElapsed = 0f;
        float alpha = peakTransparency;

        // Chaotic fade-out (from peakTransparency to 0)
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;

            // Add random fluctuation to alpha for chaos
            alpha = Mathf.Lerp(peakTransparency, 0f, timeElapsed / fadeDuration) + Random.Range(-chaosFactor, chaosFactor);
            alpha = Mathf.Clamp01(alpha); // Ensure the alpha stays between 0 and 1

            originalColor.a = alpha;
            material.color = originalColor;
            yield return null;
        }

        // Ensure the map is fully transparent at the end
        originalColor.a = 0f;
        material.color = originalColor;
    }

    private void AdjustSortingLayers()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            TilemapRenderer renderer = maps[i].GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = i + 1; // Set sorting layer to +1 of the index
            }
            else
            {
                Debug.LogWarning($"Map at index {i} does not have a TilemapRenderer component.");
            }
        }
    }
}
