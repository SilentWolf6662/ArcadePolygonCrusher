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
			DiffUp();
			if (!ShouldSpawn) return;
			wfc = false;
			int activePolygons = 0;
			foreach (GameObject polyObject in polyObjects)
				if (polyObject.activeInHierarchy) activePolygons++;
			if (diff == 0)
			{
				color = Color.white;
				difficultyHandler.DespawnPolygons(activePolygons, polyObjects);
				difficultyHandler.SpawnPolygons(1, polyObjects);
				wfc = true;
			}
			else if (diff == 1)
			{
				color = new Color(255, 124, 124, 255);
				difficultyHandler.DespawnPolygons(activePolygons, polyObjects);
				difficultyHandler.SpawnPolygons(2, polyObjects);
				wfc = true;
			}
			else if (diff == 2)
			{
				color = new Color(124, 124, 255, 255);
				wfc = true;
				difficultyHandler.DespawnPolygons(activePolygons, polyObjects);
				difficultyHandler.SpawnPolygons(3, polyObjects);
			}
			else if (diff == 3)
			{
				color = new Color(124, 255, 124, 255);
				wfc = true;
				difficultyHandler.DespawnPolygons(activePolygons, polyObjects);
				difficultyHandler.SpawnPolygons(4, polyObjects);
			}
			if (wfc)
			{
				color = new Color(); // new Color(Random.Range(0.50f, 1.0f), Random.Range(0.50f, 1.0f), Random.Range(0.50f, 1.0f), 1.0f);
				polygonHandler.NewRandomPolys(color);
				ShouldSpawn = false;
			}
		}
		private static void DiffUp()
		{

			if (PointHandler.GetPoints() == 3)
			{
				DifficultyHandler.Up1();
				diff = 1;
			}
			else if (PointHandler.GetPoints() == 13)
			{
				DifficultyHandler.Up1();
				diff = 2;
			}
			else if (PointHandler.GetPoints() == 23)
			{
				DifficultyHandler.Up1();
				diff = 3;
			}
		}

		public GameObject[] GetPolyObjects => polyObjects;
		public static void ClearDiff() => diff = 0;
	}
}
