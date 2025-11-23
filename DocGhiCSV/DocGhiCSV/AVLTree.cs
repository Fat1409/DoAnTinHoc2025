using System;
using System.Collections.Generic;
using System.Text;

namespace DocGhiCSV
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Key;
            public string[] RowData;
            public Node Left, Right;
            public int Height;

            public Node(T key, string[] data)
            {
                Key = key;
                RowData = data;
                Height = 1;
            }
        }

        private Node root;

        public void Clear() => root = null;

        public void Insert(T key, string[] row)
        {
            root = Insert(root, key, row);
        }

        private Node Insert(Node node, T key, string[] row)
        {
            if (node == null)
                return new Node(key, row);

            int cmp = key.CompareTo(node.Key);
            if (cmp < 0)
                node.Left = Insert(node.Left, key, row);
            else if (cmp > 0)
                node.Right = Insert(node.Right, key, row);
            else
                node.RowData = row;

            UpdateHeight(node);
            return BalanceNode(node);
        }

        private int Height(Node n) => n?.Height ?? 0;
        private void UpdateHeight(Node n) => n.Height = 1 + Math.Max(Height(n.Left), Height(n.Right));
        private int Balance(Node n) => n == null ? 0 : Height(n.Left) - Height(n.Right);

        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node RotateLeft(Node x)
        {   
            Node y = x.Right;
            Node T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        private Node BalanceNode(Node node)
        {
            int bf = Balance(node);
            if (bf > 1)
            {
                if (Balance(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (bf < -1)
            {
                if (Balance(node.Right) > 0)
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
        }

        public int GetTreeHeight() => Height(root);
        public int GetRootBalance() => Balance(root);

        // Cập nhật lại chiều cao của toàn bộ cây
        public void RecalculateHeights()
        {
            RecalculateHeights(root);
        }

        private void RecalculateHeights(Node node)
        {
            if (node == null) return;
            RecalculateHeights(node.Left);
            RecalculateHeights(node.Right);
            UpdateHeight(node);
        }

        // In ra cấu trúc cây dưới dạng text
        public string PrintTree()
        {
            if (root == null)
                return "Cây rỗng!";

            StringBuilder sb = new StringBuilder();
            PrintTreeHelper(root, "", true, sb);
            return sb.ToString();
        }

        private void PrintTreeHelper(Node node, string prefix, bool isTail, StringBuilder sb)
        {
            if (node == null) return;

            sb.AppendLine(prefix + (isTail ? "└── " : "├── ") +
                         $"[{node.Key}] (h={node.Height}, bf={Balance(node)})");

            var children = new List<Node>();
            if (node.Left != null) children.Add(node.Left);
            if (node.Right != null) children.Add(node.Right);

            for (int i = 0; i < children.Count; i++)
            {
                bool isLast = (i == children.Count - 1);
                string newPrefix = prefix + (isTail ? "    " : "│   ");
                PrintTreeHelper(children[i], newPrefix, isLast, sb);
            }
        }

        // NodeInfo dùng để vẽ cây
        public class NodeInfo
        {
            public T Key;
            public int Index;
            public int Depth;
            public int Height;
            public int ParentIndex;

            public NodeInfo(T key, int idx, int depth, int height, int parentIdx)
            {
                Key = key;
                Index = idx;
                Depth = depth;
                Height = height;
                ParentIndex = parentIdx;
            }
        }

        public List<NodeInfo> GetNodeInfos()
        {
            var nodes = new List<Node>();
            var depthMap = new Dictionary<Node, int>();
            var parentMap = new Dictionary<Node, Node>();

            void Traverse(Node n, int depth, Node parent)
            {
                if (n == null) return;
                Traverse(n.Left, depth + 1, n);
                depthMap[n] = depth;
                parentMap[n] = parent;
                nodes.Add(n);
                Traverse(n.Right, depth + 1, n);
            }

            Traverse(root, 0, null);

            var indexMap = new Dictionary<Node, int>();
            for (int i = 0; i < nodes.Count; i++)
                indexMap[nodes[i]] = i;

            var infos = new List<NodeInfo>();
            foreach (var n in nodes)
            {
                int idx = indexMap[n];
                int parentIdx = -1;
                if (parentMap[n] != null && indexMap.ContainsKey(parentMap[n]))
                    parentIdx = indexMap[parentMap[n]];
                infos.Add(new NodeInfo(n.Key, idx, depthMap[n], n.Height, parentIdx));
            }
            return infos;
        }
    }
}