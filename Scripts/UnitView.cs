using UnityEngine;

namespace StateIO
{
    [RequireComponent(typeof(Unit))]
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
		[SerializeField] private Factions _factions;

		private void Start()
        {
            _sprite.color = _factions.GetFactionInfo(GetComponent<Unit>().Sender.Faction).PrimaryColor;
        }
    }
}