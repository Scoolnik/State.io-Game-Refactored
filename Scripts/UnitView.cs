using UnityEngine;

namespace StateIO
{
    [RequireComponent(typeof(Unit))]
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        private void Start()
        {
            _sprite.color = Factions.Instance.GetFactionInfo(GetComponent<Unit>().Sender.Player.Faction).PrimaryColor;
        }
    }
}