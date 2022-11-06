using System.Linq;
using UnityEngine;


namespace StateIO
{
	[CreateAssetMenu]
	public class Factions : ScriptableObject
	{
		public static Factions Instance;

		[SerializeField] private Faction[] _factions;

		private void Awake()
		{
			Instance = this;
		}

		public FactionId GetRandom() => _factions[Random.Range(0, _factions.Length)].Id;

		public Faction GetFactionInfo(FactionId id) => _factions.FirstOrDefault(x => x.Id == id) ?? throw new System.Exception($"There is no {id} faction data in {nameof(Factions)}");
	}
}
