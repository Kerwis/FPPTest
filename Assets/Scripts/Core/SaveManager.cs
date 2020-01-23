using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

namespace Core
{
	public class SaveManager
	{
		private const string SaveNameKey = "saveName";
		private const string LastGameNameKey = "lastGameName";
		private Dictionary<string, string> save = new Dictionary<string, string>();
		private List<ISaveable> saveables = new List<ISaveable>();
		[SerializeField]
	 	private List<string> list = new List<string>();

		public delegate void UpdateAction(Tuple<string, string> entry);
		public delegate void UnregisterAction(ISaveable unregister);

		public static bool ContinuationAvailable()
		{
			return PlayerPrefs.GetString(LastGameNameKey, "None") != "None";
		}
		
		public void InitSaveManager(string name)
		{
			save.Add(SaveNameKey, name);
		}

		public void RegisterObject(ISaveable saveable, string value)
		{
			string key = saveable.Register(Unregister, UpdateEntry);
			save[key] = value;
			saveables.Add(saveable);
		}

		public void SaveAll()
		{
			list.Clear();
			foreach (var pair in save)
			{
				list.Add(pair.Key);
				list.Add(pair.Value);
			}
			string json = JsonUtility.ToJson(this);
			//the easiest way
			PlayerPrefs.SetString(LastGameNameKey, save[SaveNameKey]);
			PlayerPrefs.SetString(save[SaveNameKey], json);
			PlayerPrefs.Save();
		}
		
		public string LoadLastGame()
		{
			string name = PlayerPrefs.GetString(LastGameNameKey, "None");

			if (name != "None")
			{
				LoadSave(name);
				save.Clear();
				for (int i = 0; i < list.Count; i += 2)
				{
					save.Add(list[i], list[i + 1]);
				}
			}

			return name;
		}

		private void LoadSave(string saveName)
		{
			string json = PlayerPrefs.GetString(saveName);
			JsonUtility.FromJsonOverwrite(json, this);
		}

		private void UpdateEntry(Tuple<string, string> entry)
		{
			save[entry.Item1] = entry.Item2;
		}
		private void Unregister(ISaveable saveable)
		{
			string saveName = saveable.GetSaveName();
			save.Remove(saveName);
			saveables.Remove(saveable);
		}

		public string ResumeObject(ISaveable saveable)
		{
			return save[saveable.GetSaveName()];
		}
	}
}