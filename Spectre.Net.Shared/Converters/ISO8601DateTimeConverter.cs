using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spectre.Converters
{
    internal sealed class ISO8601DateTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.ParseExact(reader.GetString()!, "yyyy'-'MM'-'ddTHH':'mm':'ss'Z'", null);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss'Z'"));
        }
    }
}
