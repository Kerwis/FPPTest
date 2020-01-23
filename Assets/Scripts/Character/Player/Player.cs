using PowerUp;
using UnityEngine;

namespace Character.Player
{
    public class Player : Control
    {
        public void CollectPowerUp(Type type)
        {
            switch (type)
            {
                case Type.DoubleJump:
                    CollectDoubleJump();
                    break;
                case Type.Sprint:
                    CollectSprint();
                    break;
                default:
                    Debug.LogError("Type not set");
                    break;
            }
        }

        private void CollectDoubleJump()
        {
            HaveDoubleJump = true;
            //TODO Save it
        }

        private void CollectSprint()
        {
            HaveSprint = true;
            //TODO Save it
        }
    }
}
