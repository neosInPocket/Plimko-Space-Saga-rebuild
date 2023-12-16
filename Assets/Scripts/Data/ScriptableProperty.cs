using System;
using UnityEngine;

[Serializable]
public class ScriptableProperty
{
	[SerializeField] private PropertyType propertyType;
	[SerializeField] private SaveType identifier;
	[SerializeField] private float defaultValue;
	
	public PropertyType PropertyType => propertyType;
	public SaveType SaveType => identifier;
	public float DefaultValue => defaultValue;
}

public enum PropertyType
{
	Int,
	Float,
	Bool
}
