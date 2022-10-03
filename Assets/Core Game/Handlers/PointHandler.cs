#region Copyright Notice
// ******************************************************************************************************************
// 
// Miscellaneous Files.PointHandler.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
using static UnityEngine.Difficulty.DifficultyHandler;
namespace UnityEngine
{
	public static class PointHandler
	{
		private static int Points;
		public static int GetPoints() => Points;
		public static void AddPoints(int pointsToAdd) => Points += pointsToAdd;
		public static void SetPoints(int points) => Points = points;
		public static void ClearPoints() 
		{
			SetPoints(0);
			ClearLevels();
		}
	}
}
