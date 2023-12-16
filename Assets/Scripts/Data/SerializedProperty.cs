using System;
using UnityEngine;

public class SerializedProperty<T> : ISerializable where T : struct
{
	protected SaveType SaveType { get; private set; }
	protected T DefaultValue { get; private set; }
	public T Value
	{
		get
		{
			Deserialize();
			return _value;
		}
		set
		{
			_value = value;
			Serialize();
		}
	}

	private T _value;

	public SerializedProperty(SaveType identifier, T defaultValue, bool clearOnLoad)
	{
		SaveType = identifier;
		DefaultValue = defaultValue;

		if (clearOnLoad)
		{
			Value = DefaultValue;
		}

		Deserialize();
	}

	protected void Serialize()
	{
		Type propertyType = typeof(T);

		if (propertyType == typeof(int))
		{
			PlayerPrefs.SetInt(SaveType.ToString(), (int)(object)_value);
			PlayerPrefs.Save();
			return;
		}

		if (propertyType == typeof(float))
		{
			PlayerPrefs.SetFloat(SaveType.ToString(), (float)(object)_value);
			PlayerPrefs.Save();
			return;
		}

		if (propertyType == typeof(bool))
		{
			bool value = (bool)(object)_value;
			int result = value == true ? 1 : 0;

			PlayerPrefs.SetInt(SaveType.ToString(), result);
			PlayerPrefs.Save();
			return;
		}
	}

	protected void Deserialize()
	{
		Type propertyType = typeof(T);

		if (propertyType == typeof(int))
		{
			_value = (T)(object)PlayerPrefs.GetInt(SaveType.ToString(), (int)(object)DefaultValue);
			return;
		}

		if (propertyType == typeof(float))
		{
			_value = (T)(object)PlayerPrefs.GetFloat(SaveType.ToString(), (float)(object)DefaultValue);
			return;
		}

		if (propertyType == typeof(bool))
		{
			var defaultValue = (bool)(object)DefaultValue;

			int defaultValueInt = defaultValue == true ? 1 : 0;
			var value = PlayerPrefs.GetInt(SaveType.ToString(), defaultValueInt);
			bool valueResult = value == 1 ? true : false;
			_value = (T)(object)valueResult;
			return;
		}
	}
}

public interface ISerializable
{

}
