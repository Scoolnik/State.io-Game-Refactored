using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateIO
{
	[CreateAssetMenu]
	public class Factions : ScriptableObject
	{
		public static Factions Instance;

		[SerializeField] private Faction[] _factions;

		private Dictionary<FactionId, Faction> _factionsDict;

		private void OnEnable() => _factionsDict = _factions.ToDictionary(x => x.Id);

		public FactionId GetRandom() => _factions[Random.Range(0, _factions.Length)].Id;

		public Faction GetFactionInfo(FactionId id) => _factionsDict[id];
	}
}
