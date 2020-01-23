using System;

namespace Interface
{
	public interface ISaveable
	{
		Tuple<string, string> Register();
		void Unregister();
	}
}