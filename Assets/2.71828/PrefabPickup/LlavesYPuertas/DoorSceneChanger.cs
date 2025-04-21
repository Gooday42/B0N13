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
            Debug.LogError("No se encontr� el componente Door en el objeto.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(triggeringTag))
        {
            print("me choqu�");
            if (door != null && !door.isLocked) {
                if (debugMode) Debug.Log("Objeto con tag '" + triggeringTag + "' entr� en el collider y la puerta est� abierta.");
                Invoke("LoadNextScene", delayBeforeLoad);
            } else if (debugMode) {
                Debug.Log("Objeto con tag '" + triggeringTag + "' entr� en el collider pero la puerta est� cerrada.");
            }
        }
    }

    private void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            if (debugMode) Debug.Log("Cargando escena con �ndice: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            if (debugMode) Debug.LogWarning("No hay m�s escenas en la lista. Volviendo a la escena 0.");
            SceneManager.LoadScene(0);
        }
    }
}