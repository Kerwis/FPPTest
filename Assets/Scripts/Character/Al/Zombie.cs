using UnityEngine;

namespace Character.Al
{
    public class Zombie : Movement
    {
        [SerializeField] 
        private Transform player;

        private Transform myTransform;
        private Vector3 rotation;
        private bool dead;
        void Start()
        {
            myTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!dead && player != null)
            {
                MoveToPlayer(player);
            }
        }

        private void MoveToPlayer(Transform player)
        {
            
            myTransform.LookAt(player);
            rotation = myTransform.rotation.eulerAngles;
            rotation.x = 0;
            myTransform.rotation = Quaternion.Euler(rotation);
            Move(myTransform.forward);
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                Hit(collision.relativeVelocity * 10);
            }
        }

        public void Hit(Vector3 direction)
        {
            Die(direction);
        }

        private void Die(Vector3 direction)
        {            
            Ragdoll(direction);
            Die();
        }

        private void Die()
        {
            Ragdoll();
            dead = true;
            player = null;
        }

        public void Attack(Transform targer)
        {
            if(dead)
                return;
            player = targer;
        }

        public void LoseFocus()
        {
            player = null;
        }
    }
}
