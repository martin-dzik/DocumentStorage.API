using DocumentStorage.API.Models;
using System.Text.Json;

namespace DocumentStorage.API.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<Tag> ConvertStringCollectionToTagIEnumerable(this ICollection<string> strings)
        {
            return strings.Select(s => new Tag { Name = s });
        }

        public static IEnumerable<string> ConvertTagCollectionToStringIEnumerable(this ICollection<Tag> tags)
        {
            return tags.Select(s => s.Name);
        }

        public static string ConvertDictionaryToJSONString(this Dictionary<string, object> dictionary)
        {
            return JsonSerializer.Serialize(dictionary);
        }

        public static Dictionary<string, object> ConvertJSONStringToDictionary(this string jsonString)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
        }
    }
}
