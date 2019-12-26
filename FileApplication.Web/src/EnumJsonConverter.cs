using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FileApplication.BL.Entities;

namespace FileApplication
{
    public class ItemTypeJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(ItemType);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new ItemTypeJsonConverter();
        }
    }

    public class ItemTypeJsonConverter : JsonConverter<ItemType>
    {
        public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ItemType value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ItemType.File:
                    writer.WriteStringValue("file");
                    break;
                
                default:
                    writer.WriteStringValue("dir");
                    break;
            }
        }
    }
}