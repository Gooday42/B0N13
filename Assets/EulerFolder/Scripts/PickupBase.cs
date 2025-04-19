using UnityEngine;

public abstract class PickupBase : MonoBehaviour {
    public bool isActive = true;

    [Header("Visual")]
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.2f;
    private Vector3 startPosition;
    public bool wasCollected = false;

    private SpriteRenderer[] renderers;
    private Collider2D pickupCollider;

    protected virtual void Awake() {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();
    }

    protected virtual void Start() {
        startPosition = transform.position;
        SetVisibility(isActive);
    }

    protected virtual void Update() {
        if (!isActive || wasCollected) return;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        print("tomado");
        if (other.CompareTag("Player") && isActive && !wasCollected) {
            Collect();
        }
    }
    public virtual void Collect() {
        if (!isActive || wasCollected) return;

        wasCollected = true;
        ApplyEffect();
        SetVisibility(false);
    }

    public void ChangeEnvironment(int currentEnvironmentId) {
        bool shouldBeActive = !wasCollected;

        if (isActive != shouldBeActive) {
            isActive = shouldBeActive;
            SetVisibility(isActive);
        }
    }

    protected virtual void SetVisibility(bool visible) {
        if (renderers != null) {
            foreach (var renderer in renderers) {
                renderer.enabled = visible;
            }
        }

        if (pickupCollider != null) {
            pickupCollider.enabled = visible;
        }
    }
    public abstract void ApplyEffect();
}