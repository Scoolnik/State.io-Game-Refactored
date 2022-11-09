using UnityEngine;

namespace StateIO
{
	[ExecuteAlways]
    [RequireComponent(typeof(UnitsBase))]
    public class UnitsBaseView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
		[SerializeField] private Factions _factions;

		private void OnEnable() => GetComponent<UnitsBase>().Captured += OnCaptured;

		private void OnDisable() => GetComponent<UnitsBase>().Captured -= OnCaptured;

		private void OnCaptured(FactionId faction)
        {
            _sprite.color = _factions.GetFactionInfo(faction).PrimaryColor;
        }
    }
}