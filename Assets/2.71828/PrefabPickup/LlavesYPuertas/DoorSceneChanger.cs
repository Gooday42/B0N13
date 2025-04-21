using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneChanger : MonoBehaviour {
    public bool debugMode = false;
    public float delayBeforeLoad = 0f;
    public string triggeringTag = "Player";

    private Door door;

    private void Start() {
        door = GetComponent<Door>();
        if (door == null && debugMode) {
            Debug.LogError("No se encontró el componente Door en el objeto.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(triggeringTag))
        {
            print("me choqué");
            if (door != null && !door.isLocked) {
                if (debugMode) Debug.Log("Objeto con tag '" + triggeringTag + "' entró en el collider y la puerta está abierta.");
                Invoke("LoadNextScene", delayBeforeLoad);
            } else if (debugMode) {
                Debug.Log("Objeto con tag '" + triggeringTag + "' entró en el collider pero la puerta está cerrada.");
            }
        }
    }

    private void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            if (debugMode) Debug.Log("Cargando escena con índice: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            if (debugMode) Debug.LogWarning("No hay más escenas en la lista. Volviendo a la escena 0.");
            SceneManager.LoadScene(0);
        }
    }
}