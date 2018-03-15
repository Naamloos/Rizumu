using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Rizumu.Engine.Entities
{
	// A list that loops back to it's first value when index > item count
	internal class LoopList<T> : IList<T>
	{
		internal List<T> _items;

		internal int _index_loopback(int index)
		{
			int i = index;
			while (i > _items.Count - 1)
				i -= _items.Count;

			return i;
		}

		public T this[int index] { get => this._items[_index_loopback(index)]; set => this._items[_index_loopback(index)] = value; }

		public int Count => this._items.Count();

		public bool IsReadOnly => false;

		public LoopList()
		{
			this._items = new List<T>();
		}

		public void Add(T item)
		{
			this._items.Add(item);
		}

		public void Clear()
		{
			this._items.Clear();
		}

		public bool Contains(T item)
		{
			return this._items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this._items.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return this._items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			this._items.Insert(index, item);
		}

		public bool Remove(T item)
		{
			return this._items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			this._items.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._items.GetEnumerator();
		}
	}
}
