#region Copyright Notice

// ******************************************************************************************************************
// 
// Assembly-CSharp.PlayerScore.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-10-24
// 
// ******************************************************************************************************************

#endregion
using System;
namespace PolygonCrosser.Scoreboard
{
	#pragma warning disable CS0618
	[Serializable]
	public struct PlayerScore //Creates place to store the variables for the name and score of each player
	{
		public string username;
		public int score;

		public PlayerScore(string _username, int _score)
		{
			username = _username;
			score = _score;
		}
	}
}
