using System.Collections;

namespace CombinedCollections;

public interface ICombinedEnumerable: IEnumerable
{
    IList<IEnumerable> Enumerables { get; set; }
}

public interface ICombinedEnumerable<T> : IEnumerable<T>
{
    public IList<IEnumerable<T>> Enumerables { get; set; }
}