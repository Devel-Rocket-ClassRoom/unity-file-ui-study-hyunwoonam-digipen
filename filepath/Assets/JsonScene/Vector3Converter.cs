using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ItemDataConverter : JsonConverter<ItemData>
{
    public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = reader.Value as string;

        return DataTableManager.ItemTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}

public class CharacterDataConverter : JsonConverter<CharacterData>
{
    public override CharacterData ReadJson(JsonReader reader, Type objectType, CharacterData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = reader.Value as string;

        return DataTableManager.CharacterTable.Get(id);
    }

    public override void WriteJson(JsonWriter writer, CharacterData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}

    public class Vector3Converter : JsonConverter<Vector3>
{
    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Vector3 v = Vector3.zero;

        JObject jobj = JObject.Load(reader);
        v.x = (float)jobj["X"];
        v.y = (float)jobj["Y"];
        v.z = (float)jobj["Z"];
        return v;
    }

    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("X");
        writer.WriteValue(value.x);
        writer.WritePropertyName("Y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("Z");
        writer.WriteValue(value.z);

        writer.WriteEndObject();
    }
}
