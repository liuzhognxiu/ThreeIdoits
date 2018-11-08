// iOS AOT cannot use LitJson.JsonMaper.ToJson.
//
// The classes need to be serialized to JSON must implement IJsonSerializable

namespace LitJsonExt
{
	public interface ISerializable
	{
		void ToJson(Serializer s);
	}
}
