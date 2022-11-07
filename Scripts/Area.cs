using System;
using System.Linq;
using UnityEngine;

namespace StateIO
{
    public class Area : MonoBehaviour
    {
        [field: SerializeField] public UnitsBase[] Bases { get; protected set; }
        [field: SerializeField] public FactionId Faction { get; protected set; }

        public event Action<FactionId> Captured;

        private void Awake()
        {
            if (Bases.Length == 0) 
                Bases = GetComponentsInChildren<UnitsBase>();
        }

        private void OnEnable()
        {
			foreach (var b in Bases)
				b.Captured += OnBaseCaptured;
		}

		private void OnDisable()
		{
			foreach (var b in Bases)
				b.Captured -= OnBaseCaptured;
		}

		private void Start()
        {
            foreach (var b in Bases)
				b.SetFaction(Faction);
        }

        private void OnBaseCaptured(FactionId faction)
        {
            if (Bases.All(x => x.Faction == faction)) //todo optimize
                Captured?.Invoke(faction);
        }
    }
}