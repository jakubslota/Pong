using UnityEngine;

namespace Mirror.Examples.Pong
{
    public class Player : NetworkBehaviour
    {
        public float speed = 30;
        public Rigidbody2D rigidbody2d;

        void FixedUpdate()
        {
            if (isLocalPlayer)
                movement();
        }

        private void movement()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPosition = rigidbody2d.position;

            myPosition.y = Mathf.Lerp(myPosition.y, mousePosition.y, 10);
            myPosition.y = Mathf.Clamp(myPosition.y, -36.7f, 36.7f);
            rigidbody2d.position = myPosition;
        }
    }
}
