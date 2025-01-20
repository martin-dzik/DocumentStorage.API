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

        public static Document GetDocumentWithMergedTags(this Document document, IList<Tag> tags)
        {
            if (document.Tags != null)
            {
                var newTags = MergeDocumentTagsWithNewTags(document.Tags, tags);

                document.Tags = newTags;
            }

            return document;
        }

        public static List<Tag> MergeDocumentTagsWithNewTags(ICollection<Tag> incomingTags, IList<Tag> dbTags)
        {
            return dbTags.UnionBy(incomingTags, tag => tag.Name).ToList();
        }
    }
}
