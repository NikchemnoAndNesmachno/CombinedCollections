using System.Collections;
using System.Collections.Specialized;

namespace CombinedCollections;

public class CombinedNotifyingList<T>: IList, IEnumerable<T>, INotifyCollectionChanged, IDisposable
{
    public IReadOnlyList<T>[] Lists { get; set; }
    private Action? _onDispose;

    public void SetForNotifying(params INotifyCollectionChanged[] lists)
    {
        _onDispose = () =>
        {
            foreach (var list in lists)
            {
                list.CollectionChanged -= OnSourceCollectionChanged;
            }
        };
    }
    
    public CombinedNotifyingList(params IReadOnlyList<T>[] lists)
    {
        Lists = lists;
        OnSourceCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
    

    private void OnSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);

    public IEnumerator<T> GetEnumerator() => Lists.SelectMany(list => list).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void CopyTo(Array array, int index) => throw new NotImplementedException();

    public int Count => Lists.Sum(list => list.Count);
    public bool IsSynchronized => false;
    public object SyncRoot { get; } = new();
    public int Add(object? value) => throw new NotImplementedException();

    public void Clear() => throw new NotImplementedException();

    public bool Contains(object? value) => 
        Lists.SelectMany(list => list).Any(item => Equals(value, item));

    public int IndexOf(object? value)
    {
        int k = 0;
        foreach (var list in Lists)
        {
            foreach (var item in list)
            {
                if (Equals(item, value)) return k;
                k++;
            }
        }
        return -1;
    }

    public void Insert(int index, object? value) => throw new NotImplementedException();

    public void Remove(object? value) => throw new NotImplementedException();

    public void RemoveAt(int index) => throw new NotImplementedException();

    public bool IsFixedSize => false;
    public bool IsReadOnly => true;

    public object? this[int index]
    {
        get
        {
            foreach (var list in Lists)
            {
                if (index < list.Count) return list[index];
                index -= list.Count;
            }

            return null;
        }
        set => throw new NotImplementedException();
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public void Dispose() => _onDispose?.Invoke();
}