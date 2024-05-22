using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core;

public class DateTimeConverter : JsonConverter<DateTime>
{
     private const string DateFormat = "yyyy-MM-ddTHH:mm:ss zzz";
     public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
     {
          string dateTimeString = reader.GetString();
          return DateTime.ParseExact(dateTimeString, DateFormat, CultureInfo.InvariantCulture);
     }

     public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
     {
          writer.WriteStringValue(value.ToString(DateFormat));
     }
}