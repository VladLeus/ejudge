using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ejudge
{

    class Tree
    {
        public Node root { get; private set; }

        internal class Node
        {
            public Node left;
            public Node right;
            public int data;
        }

        public bool Add(int value)
        {
            if (root == null)
            {
                Node newNode = new Node();
                newNode.data = value;
                root = newNode;
                return true;
            }
            else
            {
                Node after = root, before = null;
                while (after != null)
                {
                    before = after;
                    if (value > after.data)
                        after = after.right;
                    else if (value < after.data)
                        after = after.left;
                    else
                        return false;
                }
                Node newNode = new Node();
                newNode.data = value;
                if (before.data < newNode.data)
                    before.right = newNode;
                else if (before.data > newNode.data)
                    before.left = newNode;
                return true;
            }
        }

        public bool Contains(int value)
        {
            if (root == null) return false;
            Node current = root;
            while (current != null)
            {
                if (current.data == value)
                    return true;
                if (current.data < value)
                    current = current.right;
                else
                    current = current.left;
            }

            return false;
        }
        public void Remove(int value)
        {
            this.root = Remove(this.root, value);
        }

        private Node Remove(Node parent, int key)
        {
            if (parent == null) return parent;

            if (key < parent.data) parent.left = Remove(parent.left, key);
            else if (key > parent.data)
                parent.right = Remove(parent.right, key);

            else
            {
                if (parent.left == null)
                    return parent.right;
                else if (parent.right == null)
                    return parent.left;

                parent.data = MinValue(parent.right);

                parent.right = Remove(parent.right, parent.data);
            }
            return parent;
        }

        private int MinValue(Node node)
        {
            int minv = node.data;

            while (node.left != null)
            {
                minv = node.left.data;
                node = node.left;
            }

            return minv;
        }
        public void PrintTree(StringBuilder buildTree, Node p, int level)
        {
            if (p == null) return;
            PrintTree(buildTree, p.left, level + 1);
            buildTree.Append(new string('.', level) + p.data + "\n");
            PrintTree(buildTree, p.right, level + 1);
        }
    }

    class Program
    {
        public static void Main()
        {
            string[] data = File.ReadAllLines("input.txt");
            Tree tree = new Tree();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                string[] command = data[i].Trim().Split();
                switch (command[0])
                {
                    case "ADD":
                        if (tree.Add(int.Parse(command[1])))
                            result.Append("DONE\n");
                        else
                            result.Append("ALREADY\n");
                        break;
                    case "SEARCH":
                        if (tree.Contains(int.Parse(command[1])))
                            result.Append("YES\n");
                        else
                            result.Append("NO\n");
                        break;
                    case "DELETE":
                        if (tree.Contains(int.Parse(command[1])))
                        {
                            tree.Remove(int.Parse(command[1]));
                            result.Append("DONE\n");
                        }
                        else
                            result.Append("CANNOT\n");
                        break;
                    case "PRINTTREE":
                        tree.PrintTree(result, tree.root, 0);
                        break;
                }
            }
            File.WriteAllText("output.txt", result.ToString());
        }
    }
}


