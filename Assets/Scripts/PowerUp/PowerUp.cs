using Character.Player;
using UnityEngine;

namespace PowerUp
{
	public enum Type
	{
		DoubleJump,
		Sprint,
	}
	//Alternative I can set Type in Unity inspector,
	//but then I can't make different effect for each power up,
	//or all put here what is a quick but messy solution
	public class PowerUp : Collectible
	{
		public override void Collect(Collision other)
		{
			var player = other.gameObject.GetComponent<Player>();
			player.CollectPowerUp(Type);
			base.Collect(other);
		}

		public virtual Type Type { get; }
	}
}
