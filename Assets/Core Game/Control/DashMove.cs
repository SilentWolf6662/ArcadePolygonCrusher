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
using Core_Game.Handlers;
using UnityEngine;
using UnityEngine.Difficulty;
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

		[SerializeField] private AllIn1Shader playerShader;
		[SerializeField] private ParticleSystem effect;
		private Rigidbody2D rb;
		public float dashSpeed;
		private DashDirection dashDirection;
		public float dashDuration;
		public float dashTimer;
		[SerializeField] private PolygonHandler polygonHandler;

		private void Awake() => rb = GetComponent<Rigidbody2D>();
		private void Start() => dashDirection = DashDirection.NoDirection;

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
			if (keyDown(KeyCode.DownArrow)) dashDirection = DashDirection.Down;
			if (keyDown(KeyCode.UpArrow)) dashDirection = DashDirection.Up;
			if (keyDown(KeyCode.LeftArrow)) dashDirection = DashDirection.Left;
			if (keyDown(KeyCode.RightArrow)) dashDirection = DashDirection.Right;
			if (keyDown(KeyCode.Space)) PolygonHandler.ClearStats();
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Polygon")) return;
			InitTriggerVariables(other, out Color playerColor, out Sprite playerPolygonType, out Color collidedPolygonColor, out Sprite collidedPolygonPolygonType);
			if (playerColor == collidedPolygonColor && playerPolygonType == collidedPolygonPolygonType)
			{
				PointHandler.AddPoints(1);
				print($"Points: {PointHandler.GetPoints().ToString()} | Level: {DifficultyHandler.GetLevel().ToString()}");
				GameHandler.ShouldSpawn = true;
			}
			else
			{
				PointHandler.ClearPoints();
				print("You failed to choose the correct color/shape! (Died)");
				GameHandler.ShouldSpawn = true;
			}
			effect = other.GetComponent<ParticleHandler>().effect;
			effect.Play();
			ResetPlayerPosition();
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
