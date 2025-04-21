using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
    public static PickupManager Instance { get; private set; }

    // para coleccionables
    private Dictionary<int, List<CollectivePickup>> pickupsByType = new Dictionary<int, List<CollectivePickup>>();
    // para las llaves
    private Dictionary<int, List<Door>> doors = new Dictionary<int, List<Door>>();
    private Dictionary<int, bool> collectedKeys = new Dictionary<int, bool>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            return;
        }
    }

    private void Start() {
        pickupsByType.Clear();
        CollectivePickup[] allPickups = FindObjectsOfType<CollectivePickup>();

        foreach (CollectivePickup pickup in allPickups) {
            int pickupType = pickup.pickupType;

            if (!pickupsByType.ContainsKey(pickupType)) {
                pickupsByType[pickupType] = new List<CollectivePickup>();
            }

            pickupsByType[pickupType].Add(pickup);
        }

        UpdateUnlockablePickups();
    }
    public void RegisterPickupCollected(CollectivePickup pickup) {
        if (pickupsByType.ContainsKey(pickup.pickupType)) {
            pickupsByType[pickup.pickupType].Remove(pickup);
            if (pickup.pickupType == 0) {
                UpdateUnlockablePickups();
            }
        }
    }

    public int GetPickupCount(int type) {
        if (pickupsByType.ContainsKey(type)) {
            return pickupsByType[type].Count;
        }
        return 0;
    }

    private void UpdateUnlockablePickups() {
        bool type0Remaining = GetPickupCount(0) > 0;

        if (!type0Remaining && pickupsByType.ContainsKey(1)) {
            Debug.Log("Todos los 0s recogidos");

            foreach (CollectivePickup unlockablePickup in pickupsByType[1]) {
                unlockablePickup.UpdateUnlockState(!type0Remaining);
            }
        }
    }
    public void RegisterDoor(Door door) {
        if (!doors.ContainsKey(door.doorId))
            doors[door.doorId] = new List<Door>();

        doors[door.doorId].Add(door);

        if (collectedKeys.ContainsKey(door.doorId) && collectedKeys[door.doorId])
            door.Unlock();
    }

    public void RegisterKeyPickup(KeyPickup key) {
        if (collectedKeys.ContainsKey(key.keyId) && collectedKeys[key.keyId]) {
            key.wasCollected = true;
            key.Collect();
        }
    }

    public void KeyCollected(int keyId) {
        collectedKeys[keyId] = true;

        if (doors.ContainsKey(keyId)) {
            foreach (var door in doors[keyId])
                door.Unlock();
        }
    }
}