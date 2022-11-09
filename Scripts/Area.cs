using System;
using System.Linq;
using UnityEngine;

namespace StateIO
{
    public class Area : MonoBehaviour
    {
        [field: SerializeField] public UnitsBase[] Bases { get; protected set; }
        [field: SerializeField] public FactionId Faction { get; protected set; }

        [SerializeField] private bool _autoCaptureBasesInside = true;

        public event Action<FactionId> Captured;

        private void OnValidate()
        {
            FindBases();
            Start();
            Captured?.Invoke(Faction);
        }

        private void Awake() => FindBases();

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
            if (_autoCaptureBasesInside)
                SetBasesFaction();
            else
                Captured?.Invoke(Faction);
        }

        private void SetBasesFaction()
        {
            foreach (var b in Bases)
                b.SetFaction(Faction);
        }

        private void FindBases() => Bases = GetComponentsInChildren<UnitsBase>();

        private void OnBaseCaptured(FactionId faction)
        {
            if (Bases.All(x => x.Faction == faction)) //todo optimize
                OnCaptured(faction);
        }

        private void OnCaptured(FactionId faction)
        {
            Faction = faction;
            Captured?.Invoke(Faction);
        }
    }
}