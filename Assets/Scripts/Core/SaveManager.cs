using System;
using System.Collections.Generic;
using Interface;
using UnityEngine.Experimental.PlayerLoop;
using Upgrades;

namespace Core
{
	public class SaveManager
	{
		private readonly string saveNameKey = "saveName";
		private Dictionary<string, string> save = new Dictionary<string, string>();
		private List<ISaveable> saveables = new List<ISaveable>();

		public delegate void UpdateAction(Tuple<string, string> entry);
		public delegate void UnregisterAction(ISaveable unregister);
		public void InitSaveManager(string name)
		{
			save.Add(saveNameKey, name);
		}

		public void RegisterObject(ISaveable saveable)
		{
			Tuple<string,string> entry = saveable.Register(Unregister, UpdateEntry);
			save.Add(entry.Item1, entry.Item2);
			saveables.Add(saveable);
		}

		public void SaveAll()
		{
			//TODO
		}

		private void UpdateEntry(Tuple<string, string> entry)
		{
			
		}
		private void Unregister(ISaveable saveable)
		{
			string saveName = saveable.GetSaveName();
			save.Remove(saveName);
			saveables.Remove(saveable);
		}
	}
}