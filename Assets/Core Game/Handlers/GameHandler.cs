#region Copyright Notice
// ******************************************************************************************************************
// 
// Assembly-CSharp.GameHandler.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-09-22
// 
// ******************************************************************************************************************
#endregion
using System;
using PolygonCrosser;
using UnityEngine;
using UnityEngine.Difficulty;
namespace Core_Game.Handlers
{
	public class GameHandler : MonoBehaviour
	{
		public static bool ShouldSpawn;
		[SerializeField] private GameObject[] polyObjects;
		private PolygonHandler polygonHandler;
		private readonly DifficultyHandler difficultyHandler = new DifficultyHandler();
		public static int diff;
		public bool wfc;
		[SerializeField] private Color color;
		private void Awake() => polygonHandler = GetComponent<PolygonHandler>();
		private void Start()
		{
			difficultyHandler.DespawnPolygons(4, polyObjects);
			difficultyHandler.SpawnPolygons(1, polyObjects);
		}
		private void Update()
		{
			difficultyHandler.IncreaseLvlPer10Points();
			if (DifficultyHandler.GetLevel() == 3) diff = 1;
			else if (DifficultyHandler.GetLevel() == 4) diff = 2;
			else if (DifficultyHandler.GetLevel() == 5) diff = 3;
			if (!ShouldSpawn) return;
			wfc = false;

			if (diff == 0)
			{
				color = Color.white;
				wfc = true;
			}
			else if (diff == 1)
			{
				color = new Color(255, 124, 124, 255);
				wfc = true;
			}
			else if (diff == 2)
			{
				color = new Color(124, 124, 255, 255);
				wfc = true;
			}
			else if (diff == 3)
			{
				color = new Color(124, 255, 124, 255);
				wfc = true;
			}
			if (wfc)
			{
				polygonHandler.NewRandomPolys(color, color, color, color);
				ShouldSpawn = false;
			}
		}
		public static void ClearDiff() => diff = 0;
	}
}
