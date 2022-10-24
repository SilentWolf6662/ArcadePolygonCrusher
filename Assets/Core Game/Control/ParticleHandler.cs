#region Copyright Notice

// ******************************************************************************************************************
// 
// Assembly-CSharp.ParticleHandler.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// 
// This work is licensed under the Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
// To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-nd/3.0/
// 
// Created & Copyrighted @ 2022-10-03
// 
// ******************************************************************************************************************

#endregion
using UnityEngine;
namespace PolygonCrosser
{
	public class ParticleHandler : MonoBehaviour
	{
		[SerializeField] private GameObject effect;
		[SerializeField] private SpriteRenderer spriteRenderer;

		private void OnTriggerEnter2D(Collider2D col)
		{
			GameObject spawnedEffect = Instantiate(effect, transform.position, transform.rotation);
			Destroy(spawnedEffect, 0.5f);
		}
	}
}
