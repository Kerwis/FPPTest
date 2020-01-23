using System;
using Character.Player;
using Core;
using Interface;
using UnityEngine;

namespace Upgrades
{
	public enum Type
	{
		None,
		DoubleJump,
		Sprint,
	}
	//Alternative I can set Type in Unity inspector,
	//but then I can't make different effect for each power up,
	//or all put here what is a quick but messy solution
	public class PowerUp : Collectible, ISaveable
	{
		public override void Collect(Collision other)
		{
			var player = other.gameObject.GetComponent<Player>();
			player.CollectPowerUp(PowerType);
			base.Collect(other);
			unregisterAction.Invoke(this);
		}

		public virtual Type PowerType => Type.None;
		private SaveManager.UnregisterAction unregisterAction;
		protected virtual string SaveName => "PowerUp" + PowerType;
		public string Register(SaveManager.UnregisterAction unregister, SaveManager.UpdateAction update)
		{
			unregisterAction = unregister;
			return SaveName;
		}

		public string GetSaveName()
		{
			return SaveName;
		}

		public void SetUp(Transform powerUpSpawnPoint)
		{
			 transform.parent = powerUpSpawnPoint;
			 transform.position = powerUpSpawnPoint.position;
			 transform.rotation = powerUpSpawnPoint.rotation;
		}
	}
}
