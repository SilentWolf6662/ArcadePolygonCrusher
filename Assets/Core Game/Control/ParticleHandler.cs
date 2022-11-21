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
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
namespace PolygonCrosser
{
	public class ParticleHandler : MonoBehaviour
	{
		[SerializeField] private GameObject effect;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private Camera cam;
		[SerializeField] private float shakeDuration;
		[SerializeField] private AnimationCurve shakeStrenghtCurve;
		private bool startShake;
		private void OnTriggerEnter2D(Collider2D col)
		{
			ParticleSystem.MainModule mainModule = effect.GetComponent<ParticleSystem>().main;
			mainModule.startColor = spriteRenderer.color;
			startShake = true;
			if (startShake)
			{
				startShake = false;
				StartCoroutine(Shaking());
			}
			Transform transform1 = transform;
			GameObject spawnedEffect = Instantiate(effect, transform1.position, transform1.rotation);
			Destroy(spawnedEffect, 0.5f);
		}
		private IEnumerator Shaking()
		{
			Vector3 startPosition = cam.transform.position;
			float elapsedTime = 0;
			while (elapsedTime < shakeDuration)
			{
				elapsedTime += Time.deltaTime;
				float strenght = shakeStrenghtCurve.Evaluate(elapsedTime / shakeDuration);
				cam.transform.position = startPosition + Random.insideUnitSphere * strenght;
				yield return null;
			}
			cam.transform.position = startPosition;
		}
	}
}
