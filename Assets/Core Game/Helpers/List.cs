#region Copyright Notice
// ******************************************************************************************************************
// 
// Miscellaneous Files.List.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
namespace UnityEngine
{
	public static class List
	{
		public static List<T> Shuffle<T>(this List<T> _list)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				T temp = _list[i];
				int randomIndex = Random.Range(i, _list.Count);
				_list[i] = _list[randomIndex];
				_list[randomIndex] = temp;
			}

			return _list;
		}
	}
}
