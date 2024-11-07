using System.Collections;

namespace CombinedCollections;

public class CombinedEnumerable<T>(params IEnumerable<T>[] enumerables) : ICombinedEnumerable<T>
{
    public IList<IEnumerable<T>> Enumerables { get; set; } = enumerables;
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var enumerable in Enumerables)
        {
            foreach (var item in enumerable)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}