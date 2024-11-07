using System.Collections;

namespace CombinedCollections;

public class CombinedEnumerable(params IEnumerable[] enumerables) : ICombinedEnumerable
{
    public IList<IEnumerable> Enumerables { get; set; } = enumerables;

    public IEnumerator GetEnumerator()
    {
        foreach (var enumerable in Enumerables)
        {
            foreach (var item in enumerable)
            {
                yield return item;
            }
        }
    }
}