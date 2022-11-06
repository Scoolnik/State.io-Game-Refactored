using UnityEngine;

namespace StateIO
{
    [RequireComponent(typeof(UnitsBase))]
    public class UnitsBaseView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        private void OnEnable() => GetComponent<UnitsBase>().Captured += OnCaptured;

		private void OnDisable() => GetComponent<UnitsBase>().Captured -= OnCaptured;

		private void OnCaptured(FactionId faction)
        {
            _sprite.color = Factions.Instance.GetFactionInfo(faction).PrimaryColor;
        }
    }
}