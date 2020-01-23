using System;
using Core;

namespace Interface
{
	public interface ISaveable
	{
		Tuple<string, string> Register(SaveManager.UnregisterAction unregister, SaveManager.UpdateAction update);
		string GetSaveName();
	}
}