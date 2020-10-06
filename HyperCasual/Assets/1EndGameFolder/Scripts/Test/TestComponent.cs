using UnityEngine;
public class TestComponent : MonoBehaviour
{
	public static string First = "First";
	public enum ComponentType { First = 1, Second = 2 };
	public ComponentType component;
	public string componentName;
	public int variableComponentFirst;
	public int variableComponentSecond;
}