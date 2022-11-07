using UnityEngine;

namespace Character
{
    public class Player : Character
    {



        public void Restart()
        {
            transform.position = InitialPosition;
        }
        // Update is called once per frame
        void Update()
        {
            float x = 0;
            float z = 0;
            if (Input.GetKey(KeyCode.Z))
            {
                x++;
            }

            if (Input.GetKey(KeyCode.S))
            {
                x--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                z++;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                z--;
            }
            _rb.velocity = new Vector3(-x ,0,z).normalized * _speed;
        
        }
    }
}
