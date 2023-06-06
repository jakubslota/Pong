using UnityEngine;

public class PlayerPaddle : Paddle
{
    public Vector2 direction { get; private set; }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPosition = rigidbody.position;

        myPosition.y = Mathf.Lerp(myPosition.y, mousePosition.y, 10);
        myPosition.y = Mathf.Clamp(myPosition.y, -20.7f, 20.7f);
        rigidbody.position = myPosition;
    }

    private void FixedUpdate()
    {
        if (direction.sqrMagnitude != 0) {
            GetComponent<Rigidbody>().AddForce(direction * speed);
        }
    }

}
