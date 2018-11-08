using System.Collections.Generic;

namespace LitJsonExt
{
	public class JsonMapper
	{
		public static T ToObject<T>(string json)
		{
			return LitJson.JsonMapper.ToObject<T>(json);
		}

		public static void ToJson(decimal val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(bool val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(double val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(int val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(long val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(string val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(ulong val, LitJson.JsonWriter writer)
		{
			writer.Write(val);
		}
		public static void ToJson(ISerializable val, LitJson.JsonWriter writer)
		{
			writer.WriteObjectStart();
			var s = new Serializer(writer);
			val.ToJson(s);
			writer.WriteObjectEnd();
		}

		public static void ToJson(IList<string> val, LitJson.JsonWriter writer)
		{
			writer.WriteArrayStart();
			foreach(var elem in val) {
				ToJson(elem, writer);
			}
			writer.WriteArrayEnd();
		}

		public static void ToJson(IList<int> val, LitJson.JsonWriter writer)
		{
			writer.WriteArrayStart();
			foreach(var elem in val) {
				ToJson(elem, writer);
			}
			writer.WriteArrayEnd();
		}

		public static void ToJson<T>(IList<T> val, LitJson.JsonWriter writer)
			where T : ISerializable
		{
			writer.WriteArrayStart();
			foreach(var elem in val) {
				ToJson(elem, writer);
			}
			writer.WriteArrayEnd();
		}

		public static void ToJson<T>(IDictionary<string, T> val, LitJson.JsonWriter writer)
			where T : ISerializable
		{
			writer.WriteObjectStart();
			foreach(var pair in val) {
				writer.WritePropertyName(pair.Key);
				ToJson(pair.Value, writer);
			}
			writer.WriteObjectEnd();
		}
		public static void ToJson(LitJson.JsonData val, LitJson.JsonWriter writer)
		{
			val.ToJson(writer);	
		}
		public static void ToJson(IList<LitJson.JsonData> val, LitJson.JsonWriter writer)
		{
			writer.WriteArrayStart();
			foreach(var elem in val) {
				ToJson(elem, writer);
			}
			writer.WriteArrayEnd();
		}
		public static void ToJson(IDictionary<string, LitJson.JsonData> val, LitJson.JsonWriter writer)
		{
			writer.WriteObjectStart();
			foreach(var pair in val) {
				writer.WritePropertyName(pair.Key);
				ToJson(pair.Value, writer);
			}
			writer.WriteObjectEnd();
		}
	}
}
