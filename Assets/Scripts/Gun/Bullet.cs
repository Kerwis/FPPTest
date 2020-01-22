using System;
using Character.Al;
using UnityEngine;

namespace Gun
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float Speed;
        private Rigidbody rg;

        public void Fire(Vector3 direction)
        {
            rg.velocity = direction * Speed;
        }
        
        private void Awake()
        {
            rg = GetComponent<Rigidbody>();
        }
        

        void OnCollisionEnter(Collision collision)
        {
            rg.useGravity = true;
            rg.constraints = RigidbodyConstraints.None;
            rg.velocity /= 0.25f;
        }
    }
}
