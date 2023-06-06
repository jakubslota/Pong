using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScoringZone : MonoBehaviour
{
    public UnityEvent scoreTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball2 ball = collision.gameObject.GetComponent<Ball2>();

        if (ball != null) {
            scoreTrigger.Invoke();
        }
    }

}
