namespace BreweriesFinder.Api.Extensions;

internal static class DictionaryExtensions
{
    internal static void AddIfNotNull<TKey, TValue>(
        this IDictionary<TKey, TValue> dic,
        TKey? key,
        TValue? value)
    {
        if (key is null || value is null)
        {
            return;
        }

        dic.Add(key, value);
    }
}
