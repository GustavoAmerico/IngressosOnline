using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Ticket.Collections;

namespace Ticket.DB.EntityFramework
{
    public class EfTable<T, TBaseCollection> : BaseEnumerable<T>, ICollection<T>, IQueryable<T>
        where T : class
        where TBaseCollection : BaseCollection<T>

    {
        /// <summary>Provider que compoem a cole��o</summary>
        protected internal readonly DbSet<T> Provider;

        /// <summary>Obt�m o total de itens na lista</summary>
        public int Count => Provider.Count();

        Type IQueryable.ElementType => ((IDbSet<T>)Provider).ElementType;

        Expression IQueryable.Expression => ((IDbSet<T>)Provider).Expression;

        IQueryProvider IQueryable.Provider => ((IDbSet<T>)Provider).Provider;

        /// <summary>Obt�m e envia um valor indicando se a cole��o � somente leitura ou n�o</summary>
        public bool IsReadOnly => false;

        public TBaseCollection Base;

        internal EfTable(DbSet<T> list, Func<ICollection<T>, TBaseCollection> constructor) : base(list)
        {
            Provider = list;
            Base = constructor(this);
        }


        public void Add(T item)
        {
            Provider.Add(item);
        }

        public IQueryable<T> AsNoTrack()
        {
            return Provider.AsNoTracking();
        }

        public T Attach(T entity)
        {
            return Provider.Attach(entity);
        }

        public void Clear()
        {
            throw new InvalidOperationException("Opera��o n�o suportada");
        }

        public bool Contains(T item)
        {
            return Provider.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Provider.ToArray().CopyTo(array, arrayIndex);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] express)
        {
            if (express == null) return Provider;
            IQueryable<T> query = Provider;
            foreach (var item in express)
                query = query.Include(item);
            return query;
        }

        public bool Remove(T item) => Provider.Remove(item) != null;
    }
}