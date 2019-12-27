using System;
using FileApplication.BL.Entities;
using Newtonsoft.Json;

namespace FileApplication
{
    public class ItemTypeJsonConverter : JsonConverter<ItemType>
    {
        public override void WriteJson(JsonWriter writer, ItemType value, JsonSerializer serializer)
        {
            switch (value)
            {
                case ItemType.File:
                    writer.WriteValue("file");
                    break;
    
                default:
                    writer.WriteValue("dir");
                    break;
            }
        }

        public override ItemType ReadJson(JsonReader reader, Type objectType, ItemType existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}