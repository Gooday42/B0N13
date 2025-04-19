using UnityEngine;

public class KeyPickup : PickupBase {
    public int keyId;
    public Door targetDoor;

    private bool wasRegistered = false;

    protected override void Start() {
        base.Start();
        if (!wasCollected && !wasRegistered) {
            PickupManager.Instance?.RegisterKeyPickup(this);
            wasRegistered = true;
        }
    }

    public override void ApplyEffect() {
        if (targetDoor != null) {
            targetDoor.Unlock();
        } else {
            PickupManager.Instance?.KeyCollected(keyId);
        }
        PlayCollectionEffects();
    }

    private void PlayCollectionEffects() {
        Debug.Log("llave coleccionada");
        base.SetVisibility(false && !wasCollected);
    }
}