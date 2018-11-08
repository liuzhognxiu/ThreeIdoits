using System.Collections.Generic;

namespace LitJsonExt
{
	// Unity uses Mono AOT for iOS, in which LitJson.JsonMapper.ToJson cannot
	// work. So the POCO cannot be serialized to Json using reflection, and the
	// fields must be listed manually.
	//
	// @see ISerializable
	public class Serializer
	{
		private LitJson.JsonWriter _writer;

		public Serializer(LitJson.JsonWriter w)
		{
			_writer = w;
		}

		public void Write(string name, decimal value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, bool value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, double value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, int value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, long value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, string value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, ulong value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, ISerializable value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, IList<int> value)
		{
			_writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, IList<string> value)
		{
			_writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write<T>(string name, IList<T> value) where T : ISerializable {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write<T>(string name, IDictionary<string, T> value) where T : ISerializable {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, LitJson.JsonData value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, IList<LitJson.JsonData> value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
		public void Write(string name, IDictionary<string, LitJson.JsonData> value) {
		  _writer.WritePropertyName(name);
			JsonMapper.ToJson(value, _writer);
		}
	}
}

