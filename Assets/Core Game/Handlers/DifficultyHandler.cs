#region Copyright Notice
// ******************************************************************************************************************
// 
// Assembly-CSharp.DifficultyLevel.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-09-21
// 
// ******************************************************************************************************************
#endregion
using Core_Game.Handlers;
using PolygonCrosser;
namespace UnityEngine.Difficulty
{
	public class DifficultyHandler
	{
		public static int Level;
		private int points;
		private int tempPoints;
		public static int GetLevel() => Level;
		private static void AddLevel(int levelsToAdd) => Level += levelsToAdd;
		private static void SetLevel(int level) => Level = level;
		public static void ClearLevels() => SetLevel(1);
		public static void Up1() => AddLevel(1);
		public static void Up10() => AddLevel(10);
		public static void Up100() => AddLevel(100);
		public void IncreaseLvlPer10Points()
		{
			if (CanLevelBeDevideByTen()) Up1();
		}
		public bool CanLevelBeDevideByTen()
		{
			points = PointHandler.GetPoints();
			if (points > 0) tempPoints = points / Level;
			return tempPoints >= 10;
		}
		public bool CanLevelBeDevideByX(int numberToCheck)
		{
			points = PointHandler.GetPoints();
			tempPoints = points / Level;
			return tempPoints >= numberToCheck;
		}
		public void SpawnPolygons(int amount, GameObject[] polygons)
		{
			if (polygons.Length < amount) return;
			for (int i = 0; i < amount; i++)
				if (polygons.Length >= i)
				{
					Polygon tempPolygon = PolygonHandler.RandomizePolygon();
					GameHandler tempGameHandler = Object.FindObjectOfType<GameHandler>();
					SpriteRenderer tempPoly = tempGameHandler.GetPolyObjects[i].GetComponent<SpriteRenderer>();
					tempPoly.color = tempPolygon.Color;
					PolygonHandler.ChangePolygonSprite(tempPoly, tempPolygon);
					SpriteRenderer firstPoly = polygons[i].GetComponent<SpriteRenderer>();
					firstPoly.color = tempPoly.color;
					firstPoly.sprite = tempPoly.sprite;
					polygons[i].SetActive(true);
				}
		}
		public void DespawnPolygons(int amount, GameObject[] polygons)
		{
			if (polygons.Length < amount) return;
			for (int i = 0; i < amount; i++)
				if (polygons.Length >= i) polygons[i].SetActive(false);
		}
	}
}
