using System.Linq;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
#endif

namespace StateIO
{
	public class OnChangedAttribute : PropertyAttribute
	{
		public string MethodName;

		public OnChangedAttribute(string methodName) => MethodName = methodName;
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(OnChangedAttribute))]
	public class OnChangedAttributePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(position, property, label);
			if (!EditorGUI.EndChangeCheck()) return;

			var targetObject = property.serializedObject.targetObject;

			var callAttribute = attribute as OnChangedAttribute;
			var methodName = callAttribute?.MethodName;

			var classType = targetObject.GetType();
			var methodInfo = classType
				.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
				.FirstOrDefault(info => info.Name == methodName);

			property.serializedObject.ApplyModifiedProperties();

			if (methodInfo != null && !methodInfo.GetParameters().Any())
			{
				methodInfo.Invoke(targetObject, null);
			}
			else
			{
				// TODO: Create proper exception
				Debug.LogError($"{nameof(OnChangedAttribute)} error : No public function without arguments named {methodName} in class {classType.Name}");
			}
		}
	}
#endif
}