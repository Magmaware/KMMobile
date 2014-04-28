namespace KMMobile.LibTess
{
    public class Dict<TValue> where TValue : class
    {
        public class Node
        {
            internal TValue _key;
            internal Node _prev, _next;

            public TValue Key { get { return _key; } }
            public Node Prev { get { return _prev; } }
            public Node Next { get { return _next; } }
        }

        public delegate bool LessOrEqual(TValue lhs, TValue rhs);

        private LessOrEqual _leq;
        Node _head;

        public Dict(LessOrEqual leq)
        {
            _leq = leq;

            _head = new Node { _key = null };
            _head._prev = _head;
            _head._next = _head;
        }

        public Node Insert(TValue key)
        {
            return InsertBefore(_head, key);
        }

        public Node InsertBefore(Node node, TValue key)
        {
            do
            {
                node = node._prev;
            } while (node._key != null && !_leq(node._key, key));

            var newNode = new Node { _key = key };
            newNode._next = node._next;
            node._next._prev = newNode;
            newNode._prev = node;
            node._next = newNode;

            return newNode;
        }

        public Node Find(TValue key)
        {
            var node = _head;
            do
            {
                node = node._next;
            } while (node._key != null && !_leq(key, node._key));
            return node;
        }

        public Node Min()
        {
            return _head._next;
        }

        public void Remove(Node node)
        {
            node._next._prev = node._prev;
            node._prev._next = node._next;
        }
    }
}
