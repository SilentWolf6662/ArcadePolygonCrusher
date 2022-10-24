using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolygonCrosser
{
	public class MenuFunctions : MonoBehaviour
	{
		public void MainMenu() => SceneManager.LoadScene((int)Scenes.MainMenu);
		public void StartGame() => SceneManager.LoadScene((int)Scenes.Gameplay);
		public void Quit()
		{
			#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
			#else
			Application.Quit();
			#endif
		}
	}
}
