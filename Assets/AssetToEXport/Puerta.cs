using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    private bool isDoorOpen = false;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public Sprite openSprite; // Sprite to use when the door is open
    public Sprite closedSprite; // Sprite to use when the door is closed

    public void OpenDoor()
    {
        isDoorOpen = true;
        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite; // Change the sprite to the open door sprite
        }
    }

    public void CloseDoor()
    {
        isDoorOpen = false;
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite; // Change the sprite to the closed door sprite
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDoorOpen && other.CompareTag("Player"))
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            int totalScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

            if (currentSceneIndex + 1 < totalScenes)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.LogError("No more scenes to load. Ensure the next scene is added to the Build Settings.");
            }
        }
    }
}
