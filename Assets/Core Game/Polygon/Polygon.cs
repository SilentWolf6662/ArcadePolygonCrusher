#region Copyright Notice
// ******************************************************************************************************************
// 
// Miscellaneous Files.Form.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-09-19
// 
// ******************************************************************************************************************
#endregion
using System;
using UnityEngine;
namespace PolygonCrosser
{
	[Serializable]
	public struct Polygon
	{
		public string Name;
		public PolygonType Type;
		public Color32 Color;
		public Polygon(PolygonType polygonType, Color color, string name)
		{
			Name = name;
			Type = polygonType;
			Color = color;
		}
	}
}
