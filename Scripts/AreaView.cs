using UnityEngine;

namespace StateIO
{
	[ExecuteAlways]
	[RequireComponent(typeof(SpriteRenderer))]
	internal class AreaView : MonoBehaviour
	{
		[SerializeField] private Area _area;
		[SerializeField] private Factions _factions;

		private SpriteRenderer _renderer;

		private void Awake() => _renderer = GetComponent<SpriteRenderer>();

		private void OnEnable() => _area.Captured += OnCaptured;

		private void OnDisable() => _area.Captured += OnCaptured;

		private void OnCaptured(FactionId faction) => _renderer.color = _factions.GetFactionInfo(faction).SecondaryColor;
	}
}
