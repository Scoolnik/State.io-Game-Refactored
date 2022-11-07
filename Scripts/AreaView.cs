using UnityEngine;

namespace StateIO
{
	[ExecuteAlways]
	[RequireComponent(typeof(SpriteRenderer))]
	internal class AreaView : MonoBehaviour
	{
		[SerializeField] private Area _area;

		private SpriteRenderer _renderer;

		private void Awake() => _renderer = GetComponent<SpriteRenderer>();

		private void OnEnable() => _area.Captured += OnCaptured;

		private void OnDisable() => _area.Captured += OnCaptured;

		private void OnCaptured(FactionId faction) => _renderer.color = Factions.Instance.GetFactionInfo(faction).SecondaryColor;
	}
}
