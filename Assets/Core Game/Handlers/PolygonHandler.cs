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
using System;
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
		[SerializeField] private SpriteSheet spriteSheet;
		[SerializeField] private LayerMask layerMask;
		public Polygon playerPolygon;
		public static int DirectionAmount = 4;
		public List<Polygon> polygons = new List<Polygon>(DirectionAmount);
		public List<Polygon> polys = new List<Polygon>(DirectionAmount);
		[SerializeField] private SpriteRenderer[] polyGraphics;

		private void Awake()
		{
			if (spriteSheet == null) spriteSheet = GetComponent<SpriteSheet>();
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

		public void NewRandomPolys(Color color = new Color(), Color color2 = new Color(), Color color3 = new Color(), Color color4 = new Color())
		{

			Polygon poly1 = RandomizePolygon(color);
			ChangePlayerPolygon(poly1);
			polygons.Clear();
			polygons.Add(poly1);
			polygons.Add(RandomizePolygon(color2));
			polygons.Add(RandomizePolygon(color3));
			polygons.Add(RandomizePolygon(color4));
			polygons = polygons.Shuffle();
			polys.Clear();
			foreach (Polygon polygon in polygons) polys.Add(polygon);
			for (int i = 0; i < polygons.Count; i++)
			{
				polyGraphics[i].color = polys[i].Color;
				ChangePolygonSprite(polyGraphics[i], polys[i]);
			}
		}
		private static Polygon RandomizePolygon(Color color = new Color())
		{
			PolygonType polygonType = (PolygonType)Random.Range(1, 5);
			if (color == new Color()) color = new Color(Random.Range(0.50f, 1.0f), Random.Range(0.50f, 1.0f), Random.Range(0.50f, 1.0f), 1.0f);
			return new Polygon(polygonType, color, default);
		}

		private void ChangePlayerPolygon(Polygon newPolygon)
		{
			playerPolygon = newPolygon;
			playerGraphic.color = playerPolygon.Color;
			ChangePolygonSprite(playerGraphic, playerPolygon);
		}
		private void ChangePolygonSprite(SpriteRenderer graphic, Polygon polygonType)
		{
			if (polygonType.Type == PolygonType.Circle) graphic.sprite = spriteSheet.Circle;
			if (polygonType.Type == PolygonType.Triangle) graphic.sprite = spriteSheet.Triangle;
			if (polygonType.Type == PolygonType.Square) graphic.sprite = spriteSheet.Square;
			if (polygonType.Type == PolygonType.Hexagon) graphic.sprite = spriteSheet.Hexagon;
		}
	}
}
