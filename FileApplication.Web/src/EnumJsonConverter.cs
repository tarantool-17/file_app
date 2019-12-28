using System;
using FileApplication.BL.Models;
using Newtonsoft.Json;

namespace FileApplication
{
    public class ItemTypeJsonConverter : JsonConverter<ComponentType>
    {
        public override void WriteJson(JsonWriter writer, ComponentType value, JsonSerializer serializer)
        {
            switch (value)
            {
                case ComponentType.File:
                    writer.WriteValue("file");
                    break;
    
                default:
                    writer.WriteValue("dir");
                    break;
            }
        }

        public override ComponentType ReadJson(JsonReader reader, Type objectType, ComponentType existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}