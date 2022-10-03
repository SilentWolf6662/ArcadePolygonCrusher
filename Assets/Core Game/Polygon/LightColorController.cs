using UnityEngine;

namespace PolygonCrosser
{
	public class LightColorController : MonoBehaviour
	{
		private SpriteRenderer parentGraphic;
		private Light curLight;

		private void Awake() => curLight = GetComponent<Light>();
		private void Start() => parentGraphic = GetComponentInParent<SpriteRenderer>();
		private void Update() => UpdateLightColor();
		private void UpdateLightColor()
		{
			if (curLight.color != parentGraphic.color) curLight.color = parentGraphic.color;
		}
	}
}
