using UnityEngine;

public class DoorColliderManager : MonoBehaviour {
    public bool makeTriggerWhenUnlocked = true;
    public bool debugMode = false;

    private Door door;
    private Collider2D doorCollider;

    private void Awake() {
        door = GetComponent<Door>();
        doorCollider = GetComponent<Collider2D>();

        if (doorCollider == null && debugMode) {
            Debug.LogError("No se encontr� un componente Collider en el objeto.");
        }
    }

    private void Update() {
        if (doorCollider != null) {
            bool shouldBeTrigger = makeTriggerWhenUnlocked ? !door.isLocked : door.isLocked;

            if (doorCollider.isTrigger != shouldBeTrigger) {
                doorCollider.isTrigger = shouldBeTrigger;

                if (debugMode) {
                    Debug.Log($"Collider actualizado. isTrigger: {doorCollider.isTrigger} " +
                             $"(Puerta {(door.isLocked ? "bloqueada" : "desbloqueada")})");
                }
            }
        }
    }
}