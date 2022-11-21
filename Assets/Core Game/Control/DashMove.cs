#region Copyright Notice

// ******************************************************************************************************************
// 
// Assembly-CSharp.PolyColor.cs © Shadow Wolf Development (SilentWolf6662 & Bambinidk) - All Rights Reserved
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
using System;
using System.Collections;
using Core_Game.Handlers;
using UnityEngine;
using UnityEngine.Difficulty;
using UnityEngine.SceneManagement;
namespace PolygonCrosser
{
	public class DashMove : MonoBehaviour
	{
		private enum DashDirection
		{
			Left,
			Right,
			Up,
			Down,
			NoDirection
		}

		private Rigidbody2D rb;
		[SerializeField] private float dashSpeed;
		private DashDirection dashDirection;
		[SerializeField] private float dashDuration;
		[SerializeField] private float dashTimer;
		[SerializeField] private PolygonHandler polygonHandler;

		private void Awake() => rb = GetComponent<Rigidbody2D>();
		private void Start() 
		{
			dashDirection = DashDirection.NoDirection;
			PointHandler.ClearPoints();
		}

		private void Update()
		{
			rb.velocity = Vector2.zero;
			InputHandler();
			if (dashDirection != DashDirection.NoDirection) MoveOverTime();
		}
		private void MoveOverTime()
		{

			if (dashTimer >= dashDuration)
			{
				dashDirection = DashDirection.NoDirection;
				dashTimer = 0;
				rb.velocity = Vector2.zero;
			}
			else
			{
				dashTimer += Time.deltaTime;
				MovementHandler();
			}
		}
		private void MovementHandler() => rb.velocity = dashDirection switch { DashDirection.Left => Vector2.left * dashSpeed, DashDirection.Right => Vector2.right * dashSpeed, DashDirection.Up => Vector2.up * dashSpeed, DashDirection.Down => Vector2.down * dashSpeed, _ => rb.velocity };
		private void InputHandler()
		{
			Func<KeyCode,bool> keyDown = Input.GetKeyDown;
			if (keyDown(KeyCode.DownArrow) && !keyDown(KeyCode.UpArrow) && !keyDown(KeyCode.LeftArrow) && !keyDown(KeyCode.RightArrow)) dashDirection = DashDirection.Down;
			if (keyDown(KeyCode.UpArrow) && !keyDown(KeyCode.DownArrow) && !keyDown(KeyCode.LeftArrow) && !keyDown(KeyCode.RightArrow)) dashDirection = DashDirection.Up;
			if (keyDown(KeyCode.LeftArrow) && !keyDown(KeyCode.DownArrow) && !keyDown(KeyCode.UpArrow) && !keyDown(KeyCode.RightArrow)) dashDirection = DashDirection.Left;
			if (keyDown(KeyCode.RightArrow) && !keyDown(KeyCode.DownArrow) && !keyDown(KeyCode.UpArrow) && !keyDown(KeyCode.LeftArrow)) dashDirection = DashDirection.Right;
			if (keyDown(KeyCode.Space)) PolygonHandler.ClearStats();
		}
		IEnumerator DelaySpawning(float timeInSeconds, Collider2D other)
		{
			yield return new WaitForSeconds(timeInSeconds * 0.3f);
			InitTriggerVariables(other, out Color playerColor, out Sprite playerPolygonType, out Color collidedPolygonColor, out Sprite collidedPolygonPolygonType);
			if (playerColor == collidedPolygonColor && playerPolygonType == collidedPolygonPolygonType)
			{
				PointHandler.AddPoints(1);
				print($"Points: {PointHandler.GetPoints().ToString()} | Level: {DifficultyHandler.GetLevel().ToString()}");
				GameHandler.ShouldSpawn = true;
				PlayerPrefs.SetInt("score", PointHandler.GetPoints());
			} else Die();
			yield return new WaitForSeconds(timeInSeconds * 0.1f);
			other.GetComponent<SpriteRenderer>().enabled = true;
			foreach (SpriteRenderer spriteRenderer in FindObjectsOfType<SpriteRenderer>()) spriteRenderer.enabled = true;
			ResetPlayerPosition();
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Wall")) ResetPlayerPosition();
			float counter = 0;
			while (counter < 1)
			{
				counter += Time.deltaTime;
				ResetPlayerPosition();
			}
			if (other.CompareTag("Polygon"))
			{
				other.GetComponent<SpriteRenderer>().enabled = false;
				foreach (SpriteRenderer spriteRenderer in FindObjectsOfType<SpriteRenderer>()) spriteRenderer.enabled = false;
				IEnumerator delaySpawning = DelaySpawning(1, other);
				StartCoroutine(delaySpawning);
			}
		}
		internal static void Die()
		{
			PlayerPrefs.SetInt("score", PointHandler.GetPoints());
			PointHandler.ClearPoints();
			print("You failed to choose the correct color/shape! (Died)");
			Time.timeScale = 0;
			SceneManager.LoadScene((int)Scenes.Gameplay);
			Time.timeScale = 0;
			//SceneManager.LoadScene((int)Scenes.Gameover);
		}

		private void ResetPlayerPosition()
		{
			dashDirection = DashDirection.NoDirection;
			dashTimer = 0;
			rb.velocity = Vector2.zero;
			transform.position = Vector3.zero;
		}
		private void InitTriggerVariables(Component other, out Color playerColor, out Sprite playerPolygonType, out Color collidedPolygonColor, out Sprite collidedPolygonPolygonType)
		{
			SpriteRenderer playerRender = polygonHandler.playerGraphic;
			playerPolygonType = playerRender.sprite;
			playerColor = playerRender.color;

			SpriteRenderer collidedPolygon = other.GetComponent<SpriteRenderer>();
			collidedPolygonColor = collidedPolygon.color;
			collidedPolygonPolygonType = collidedPolygon.sprite;
		}
	}
}
