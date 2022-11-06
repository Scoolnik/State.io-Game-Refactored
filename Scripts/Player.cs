using UnityEngine;

namespace StateIO
{
    public class Player : MonoBehaviour
    {
        public FactionId Faction => _faction;

        [SerializeField] private FactionId _faction;
    }
}