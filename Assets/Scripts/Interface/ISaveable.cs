using System;
using Core;

namespace Interface
{
	public interface ISaveable
	{
		string Register(SaveManager.UnregisterAction unregister, SaveManager.UpdateAction update);
		string GetSaveName();
	}
}