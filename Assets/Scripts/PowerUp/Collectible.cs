using System;
using Interface;
using UnityEngine;

namespace PowerUp
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectible : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Collect(other);
            }
        }

        public virtual void Collect(Collision other)
        {
            gameObject.SetActive(false);
        }
    }
}
