using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BouncySurface : MonoBehaviour
{
    public float bounceStrength = 1f;
    public AudioSource coll;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball2 ball = collision.gameObject.GetComponent<Ball2>();
        coll.Play();
        if (ball != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            ball.rigidbody.AddForce(-normal * bounceStrength, ForceMode2D.Impulse);
        }
    }

}
