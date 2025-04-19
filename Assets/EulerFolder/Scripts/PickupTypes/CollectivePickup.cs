using UnityEngine;

public class CollectivePickup : PickupBase {
    public int pickupType = 0; // 1 para desbloqueable

    private bool isUnlocked = false;

    protected override void Start() {
        base.Start();
        if (PickupManager.Instance != null) {
            if (pickupType == 1) {
                UpdateUnlockState(false);
            }
        }
    }

    public override void ApplyEffect() {
        if (PickupManager.Instance != null) {
            PickupManager.Instance.RegisterPickupCollected(this);
        }
        Debug.Log("colectado");
    }

    public void UpdateUnlockState(bool unlocked) {
        isUnlocked = unlocked;
        if (pickupType == 1) {
            bool shouldBeVisible = isUnlocked && !wasCollected;
            SetVisibility(shouldBeVisible);
            isActive = shouldBeVisible;
        }
    }
}