using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KMMobile.LibTess
{
    public class PriorityQueue<TValue> where TValue : class
    {
        private PriorityHeap<TValue>.LessOrEqual _leq;
        private PriorityHeap<TValue> _heap;
        private TValue[] _keys;
        private int[] _order;

        private int _size, _max;
        private bool _initialized;

        public bool Empty { get { return _size == 0 && _heap.Empty; } }

        public PriorityQueue(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
        {
            _leq = leq;
            _heap = new PriorityHeap<TValue>(initialSize, leq);

            _keys = new TValue[initialSize];

            _size = 0;
            _max = initialSize;
            _initialized = false;
        }

        class StackItem
        {
            internal int p, r;
        };

        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        public void Init()
        {
            var stack = new Stack<StackItem>();
            int p, r, i, j, piv;
            uint seed = 2016473283;

            p = 0;
            r = _size - 1;
            _order = new int[_size + 1];
            for (piv = 0, i = p; i <= r; ++piv, ++i)
            {
                _order[i] = piv;
            }

            stack.Push(new StackItem { p = p, r = r });
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                p = top.p;
                r = top.r;

                while (r > p + 10)
                {
                    seed = seed * 1539415821 + 1;
                    i = p + (int)(seed % (r - p + 1));
                    piv = _order[i];
                    _order[i] = _order[p];
                    _order[p] = piv;
                    i = p - 1;
                    j = r + 1;
                    do
                    {
                        do { ++i; } while (!_leq(_keys[_order[i]], _keys[piv]));
                        do { --j; } while (!_leq(_keys[piv], _keys[_order[j]]));
                        Swap(ref _order[i], ref _order[j]);
                    } while (i < j);
                    Swap(ref _order[i], ref _order[j]);
                    if (i - p < r - j)
                    {
                        stack.Push(new StackItem { p = j + 1, r = r });
                        r = i - 1;
                    }
                    else
                    {
                        stack.Push(new StackItem { p = p, r = i - 1 });
                        p = j + 1;
                    }
                }
                for (i = p + 1; i <= r; ++i)
                {
                    piv = _order[i];
                    for (j = i; j > p && !_leq(_keys[piv], _keys[_order[j - 1]]); --j)
                    {
                        _order[j] = _order[j - 1];
                    }
                    _order[j] = piv;
                }
            }

#if DEBUG
            p = 0;
            r = _size - 1;
            for (i = p; i < r; ++i)
            {
                Debug.Assert(_leq(_keys[_order[i + 1]], _keys[_order[i]]), "Wrong sort");
            }
#endif

            _max = _size;
            _initialized = true;
            _heap.Init();
        }

        public PQHandle Insert(TValue value)
        {
            if (_initialized)
            {
                return _heap.Insert(value);
            }

            int curr = _size;
            if (++_size >= _max)
            {
                _max <<= 1;
                Array.Resize(ref _keys, _max);
            }

            _keys[curr] = value;
            return new PQHandle { _handle = -(curr + 1) };
        }

        public TValue ExtractMin()
        {
            Debug.Assert(_initialized);

            if (_size == 0)
            {
                return _heap.ExtractMin();
            }
            TValue sortMin = _keys[_order[_size - 1]];
            if (!_heap.Empty)
            {
                TValue heapMin = _heap.Minimum();
                if (_leq(heapMin, sortMin))
                    return _heap.ExtractMin();
            }
            do
            {
                --_size;
            } while (_size > 0 && _keys[_order[_size - 1]] == null);

            return sortMin;
        }

        public TValue Minimum()
        {
            Debug.Assert(_initialized);

            if (_size == 0)
            {
                return _heap.Minimum();
            }
            TValue sortMin = _keys[_order[_size - 1]];
            if (!_heap.Empty)
            {
                TValue heapMin = _heap.Minimum();
                if (_leq(heapMin, sortMin))
                    return heapMin;
            }
            return sortMin;
        }

        public void Remove(PQHandle handle)
        {
            Debug.Assert(_initialized);

            int curr = handle._handle;
            if (curr >= 0)
            {
                _heap.Remove(handle);
                return;
            }
            curr = -(curr + 1);
            Debug.Assert(curr < _max && _keys[curr] != null);

            _keys[curr] = null;
            while (_size > 0 && _keys[_order[_size - 1]] == null)
            {
                --_size;
            }
        }
    }
}
