using System.Collections.Generic;
using UnityEngine;

namespace StateIO
{
	[RequireComponent(typeof(LineRenderer))]
	[RequireComponent(typeof(UnitsBase))]
	public class UnitsBaseTouchHandler : MonoBehaviour
	{
		private static FactionId? _selectedFaction;
		private static HashSet<UnitsBaseTouchHandler> _activeHandlers = new();

		private UnitsBase _base;
		private LineRenderer _renderer;

		private void Awake()
		{
			_base = GetComponent<UnitsBase>();
			_renderer = GetComponent<LineRenderer>();
			_renderer.enabled = false;
			_renderer.SetPosition(0, transform.position + Camera.main.transform.position);
		}

		private void Update()
		{
			if (_renderer.enabled)
				_renderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		private void OnMouseDown()
		{
			if (_selectedFaction != null)
				return;
			_selectedFaction = _base.Player.Faction;
			OnSelected();
		}

		private void OnMouseEnter()
		{
			if (_selectedFaction == _base.Player.Faction)
				OnSelected();
		}

		private void OnMouseUp()
		{
			var collider = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, 1 << gameObject.layer).collider;
			if (collider)
			{
				var selectedBase = collider.GetComponent<UnitsBase>();
				if (selectedBase)
					SetTargetForActiveHandlers(selectedBase);
			}
			DeselectActiveHandlers();
		}

		private static void SetTargetForActiveHandlers(UnitsBase selectedBase)
		{
			foreach (var handler in _activeHandlers)
				handler.OnTargetSelected(selectedBase);
		}

		private void DeselectActiveHandlers()
		{
			foreach (var handler in _activeHandlers)
				handler.OnDeselected();
			_activeHandlers.Clear();
			_selectedFaction = null;
		}

		private void OnSelected()
		{
			_activeHandlers.Add(this);
			_renderer.enabled = true;
		}

		private void OnDeselected() => _renderer.enabled = false;

		private void OnTargetSelected(UnitsBase target)
		{
			if (target != _base)
				_base.SendAllUnits(target);
		}
	}
}
