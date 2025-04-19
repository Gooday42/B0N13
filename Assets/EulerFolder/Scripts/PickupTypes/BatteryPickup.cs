using UnityEngine;

public class BatteryPickup : PickupBase {
    public override void ApplyEffect() {
        EnergyTracker energyTracker = GameObject.FindObjectOfType<EnergyTracker>();
        if (energyTracker != null) {
            energyTracker.AddEnergy();
            Debug.Log("Se a�ade {energyAmount} de enegia XD");
        } else {
            Debug.Log("No hay energy");
        }
    }
}