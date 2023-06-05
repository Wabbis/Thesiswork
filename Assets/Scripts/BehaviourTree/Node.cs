using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _sharedData = new Dictionary<string, object>();
        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach(Node child in children)
            {
                AttachNode(child);
            }
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        private void AttachNode(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public void SetData(string key, object value)
        {
            _sharedData[key] = value;
            // UnityEngine.Debug.Log("Object added to dictionary with key: " + key + " and value: " + value);
        }

        public object GetData(string key)
        {
            object value = null;
            if (_sharedData.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if(value != null)
                    return value;
                node = node.parent;
            }
            return null;
            }

        public bool ClearData(string key)
        {
            if (_sharedData.ContainsKey(key))
            {
                _sharedData.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) return true;
                node = node.parent;

            }
            return false;
        }
    }
}

