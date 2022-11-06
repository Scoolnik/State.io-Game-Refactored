using System;
using System.Collections;
using UnityEngine;

namespace StateIO
{
	public class UnitsBase : MonoBehaviour
	{
		public Player Player => _player;

		[SerializeField] private Player _player;
		[SerializeField] private float _unitProductionTime = 1f;
		[SerializeField] private float _unitSendPeriod = 0.5f;
		[SerializeField] private int _startUnitCount = 0;
		[SerializeField] private int _unitCapacity = 100;
		[SerializeField] private int _unitCount = 0;
		[SerializeField] private GameObject _unitPrefab;

		public event Action<int> UnitsCountChanged;
		public event Action<FactionId> Captured;

		private void Start()
		{
			ChangeUnitsCount(_startUnitCount);
			Captured?.Invoke(_player.Faction);
		}

		private void OnEnable() => StartCoroutine(ProduceUnits());

		private void OnDisable() => StopAllCoroutines();

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag(Tags.UnitTag)) //todo get rid of this using layers
				return;
			var unit = other.GetComponent<Unit>();
			if (unit.Sender == this)
				return;
			if (unit.Sender.Player.Faction != _player.Faction)
			{
				ChangeUnitsCount(-1);
				if (_unitCount == 0)
				{
					_player = unit.Sender.Player;
					Captured?.Invoke(_player.Faction);
				}
			}
			else
			{
				ChangeUnitsCount(1);
			}
			Destroy(unit.gameObject); //todo use pool
		}

		public void SendAllUnits(UnitsBase target) => StartCoroutine(SendUnits(target));

		private IEnumerator SendUnits(UnitsBase target)
		{
			do //todo constrain by current number of units
			{
				ChangeUnitsCount(-1);
				var unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity).GetComponent<Unit>();
				unit.Init(this, target);
				yield return new WaitForSeconds(_unitSendPeriod);
			} 
			while (_unitCount > 0);
		}

		private IEnumerator ProduceUnits()
		{
			while (true)
			{
				ChangeUnitsCount(1);
				yield return new WaitForSeconds(_unitProductionTime);
			}
		}

		private void ChangeUnitsCount(int delta)
		{
			_unitCount = Math.Clamp(_unitCount + delta, 0, _unitCapacity);
			UnitsCountChanged?.Invoke(_unitCount);
		}
	}
}
