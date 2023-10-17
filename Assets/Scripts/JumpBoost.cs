using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] private float boostForce = 1000f;  // The force applied when something jumps on the jump pad

    private void OnCollisionEnter(Collision collision)  // We use OnCollisionEnter to detect when something lands on the jump pad
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null && collision.gameObject.CompareTag("Player"))  // Check if the colliding object has a Rigidbody and is tagged "Player"
        {
            rb.AddForce(Vector3.up * boostForce, ForceMode.Acceleration);
        }
    }
}
