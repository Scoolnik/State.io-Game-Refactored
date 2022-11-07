using System;
using System.Linq;
using UnityEngine;

namespace StateIO
{
    [ExecuteAlways]
    public class Area : MonoBehaviour
    {
        [field: SerializeField] public UnitsBase[] Bases { get; protected set; }
        [field: SerializeField, OnChanged(nameof(Start))] public FactionId Faction { get; protected set; }

        [SerializeField] private bool _autoCaptureBasesInside = true;

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
            if (_autoCaptureBasesInside)
            {
                foreach (var b in Bases)
                    b.SetFaction(Faction);
            }
        }

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