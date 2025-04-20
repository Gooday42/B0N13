using UnityEngine;

public class Door : MonoBehaviour {
    public int doorId;
    public bool startsLocked = true;
    public bool isLocked;
    public Sprite spriteOpen;

    private SpriteRenderer spriteRenderer;
    private Sprite spriteClosed;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteClosed = spriteRenderer.sprite;

        isLocked = startsLocked;
        UpdateDoorState();
        PickupManager.Instance?.RegisterDoor(this);
    }

    public void Unlock() {
        if (isLocked) {
            isLocked = false;
            UpdateDoorState();
            Debug.Log($"Door {doorId} unlocked!");
        }
    }

    private void UpdateDoorState() {
        spriteRenderer.sprite = isLocked ? spriteClosed : spriteOpen;
    }
}