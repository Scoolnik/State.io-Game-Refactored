using TMPro;
using UnityEngine;

namespace StateIO
{
	public class UnitsNumberView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;
		[SerializeField] private UnitsBase _model;

		private void OnEnable() => _model.UnitsCountChanged += OnValueChanged;

		private void OnDisable() => _model.UnitsCountChanged -= OnValueChanged;

		private void OnValueChanged(int number) => _text.text = number.ToString();
	}
}
