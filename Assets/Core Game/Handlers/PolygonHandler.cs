#region Copyright Notice
// ******************************************************************************************************************
// 
// Assembly-CSharp.Player.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-09-20
// 
// ******************************************************************************************************************
#endregion
using System.Collections.Generic;
using Core_Game.Handlers;
using UnityEngine;
using UnityEngine.Difficulty;
using Random = UnityEngine.Random;
namespace PolygonCrosser
{
	public class PolygonHandler : MonoBehaviour
	{
		public SpriteRenderer playerGraphic;
		private static SpriteSheet spriteSheet;
		[SerializeField] private LayerMask layerMask;
		public Polygon playerPolygon;
		public static int DirectionAmount = 4;
		public List<Polygon> polygons = new List<Polygon>(DirectionAmount);
		public List<Polygon> polys = new List<Polygon>(DirectionAmount);
		[SerializeField] private SpriteRenderer[] polyGraphics;
		[SerializeField] private GameHandler gameHandler;
		private const float maxColorValue = 1f;
		private const float minColorValue = 0f;

		private void Awake()
		{
			if (spriteSheet == null) spriteSheet = FindObjectOfType<SpriteSheet>();
		}

		private void Start()
		{
			ClearStats();
			GameHandler.ShouldSpawn = true;
		}
		public static void ClearStats()
		{
			PointHandler.ClearPoints();
			DifficultyHandler.ClearLevels();
			GameHandler.ClearDiff();
		}

		public void NewRandomPolys(Color color = new Color())
		{
			int activePolygons = 0;
			foreach (GameObject polyObject in gameHandler.GetPolyObjects)
				if (polyObject.activeInHierarchy) activePolygons++;
			Polygon poly1 = RandomizePolygon(color);
			ChangePlayerPolygon(poly1);
			polygons.Clear();
			polygons.Add(poly1);
			for (int i = 0; i < activePolygons; i++) if (i != 0) polygons.Add(RandomizePolygon(color));
			polygons = polygons.Shuffle();
			polys.Clear();
			foreach (Polygon polygon in polygons) polys.Add(polygon);
			for (int i = 0; i < polygons.Count; i++)
			{
				polyGraphics[i].color = polys[i].Color;
				ChangePolygonSprite(polyGraphics[i], polys[i]);
			}
		}

		public static Polygon RandomizePolygon(Color color = new Color())
		{
			PolygonType polygonType = (PolygonType)Random.Range(1, 5);
			if (color == new Color()) color = new Color(Random.Range(minColorValue, maxColorValue), Random.Range(minColorValue, maxColorValue), Random.Range(minColorValue, maxColorValue), 1.0f);
			return new Polygon(polygonType, color, default);
		}

		private void ChangePlayerPolygon(Polygon newPolygon)
		{
			playerPolygon = newPolygon;
			playerGraphic.color = playerPolygon.Color;
			ChangePolygonSprite(playerGraphic, playerPolygon);
		}
		public static void ChangePolygonSprite(SpriteRenderer graphic, Polygon polygonType)
		{
			if (polygonType.Type == PolygonType.Circle) graphic.sprite = spriteSheet.Circle;
			if (polygonType.Type == PolygonType.Triangle) graphic.sprite = spriteSheet.Triangle;
			if (polygonType.Type == PolygonType.Square) graphic.sprite = spriteSheet.Square;
			if (polygonType.Type == PolygonType.Hexagon) graphic.sprite = spriteSheet.Hexagon;
			if (polygonType.Type == PolygonType.Octogon) graphic.sprite = spriteSheet.Octogon;
		}
	}
}
