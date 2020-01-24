using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class ExitPopup : MonoBehaviour
	{
		public void ExitYes()
		{
			SceneManager.LoadScene("Menu");
			SceneManager.UnloadSceneAsync("Game");
		}

		public void ExitNo()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Hide();
		}

		private void Start()
		{
			Hide();
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}