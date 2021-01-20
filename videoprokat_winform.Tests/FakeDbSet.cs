using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace videoprokat_winform.Tests
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        readonly IQueryable _query;

        public FakeDbSet()
        {
            Local = new ObservableCollection<T>();
            _query = Local.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            Local.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            Local.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            Local.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            Local.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local { get; }

        Type IQueryable.ElementType => _query.ElementType;

        System.Linq.Expressions.Expression IQueryable.Expression => _query.Expression;

        IQueryProvider IQueryable.Provider => _query.Provider;

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Local.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Local.GetEnumerator();
        }
    }
}