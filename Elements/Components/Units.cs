using Godot;
using System;
using System.Collections.Generic;

public class Units
{
    public static List<Node> AllChildren(Node startNode)
    {
        Queue<Node> queue = new Queue<Node>();
        List<Node> allChildren = new List<Node>();

        if (startNode == null) return allChildren;

        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();
            foreach (Node child in currentNode.GetChildren())
            {
                allChildren.Add(child);
                queue.Enqueue(child);
            }
        }

        return allChildren;
    }
}
