using UnityEngine;
using System.Collections;
using System;

public class Parser {
    
    Logger logger;

    public Parser (Logger logger) {
        this.logger = logger;
    }

    public GVGraph ParseGraphSource(string source) {
        GVGraph graph;

        string[] lines = source.Split('\n');

        logger.Log("Lines...");
        for (int i = 0; i < lines.Length; i++) {
            logger.Log("Line " + i + ": " + lines[i]);
        }
        logger.Log("...End Lines");

        graph = ParseGraphLine(lines[0]);

        int nodeCount = CountNodes(lines);
        graph.nodes = new GVNode[nodeCount];

        for (int i = 0; i < nodeCount; i++) {
            graph.nodes[i] = ParseNode(lines[1 + i]);
        }

        int edgeCount = CountEdges(lines);
        graph.edges = new GVEdge[edgeCount];

        for (int i = 0; i < edgeCount; i++) {
            graph.edges[i] = ParseEdge(lines[1 + nodeCount]);
        }

        return graph;
    }

    private GVEdge ParseEdge(string edgeLine) {
        string[] strings = edgeLine.Split(' ');

        GVEdge edge = new GVEdge();

        edge.tailNodeName = strings[1];
        edge.headNodeName = strings[2];
        edge.pointCount = int.Parse(strings[3]);
        edge.points = new Vector2[edge.pointCount];

        for (int i = 0; i < edge.pointCount; i++) {
            edge.points[i] = new Vector2(float.Parse(strings[4 + (i * 2)]),
                float.Parse(strings[5 + (i * 2)]));
        }

        int x = 4 + (edge.pointCount * 2);

        if (strings.Length - x == 2) {
            edge.style = strings[x];
            edge.color = strings[x + 1].TrimEnd('\r');
        } else if (edgeLine.Length - x == 5) {
            edge.label = strings[x];
            edge.labelPosition.x = int.Parse(strings[x + 1]);
            edge.labelPosition.y = int.Parse(strings[x + 2]);
            edge.style = strings[x + 3];
            edge.color = strings[x + 4].TrimEnd('\r');
        } else {
            throw new Exception("Incorrect number of elements at end of edge line string");
        }

        return edge;
    }

    private int CountEdges(string[] lines) {
        int count = 0;

        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].StartsWith("edge ")) {
                count++;
            }
        }

        return count;
    }

    private GVNode ParseNode(string nodeLine) {
        string[] strings = nodeLine.Split(' ');

        GVNode node = new GVNode();

        node.name = strings[1];
        node.x = float.Parse(strings[2]);
        node.y = float.Parse(strings[3]);
        node.xsize = float.Parse(strings[4]);
        node.ysize = float.Parse(strings[5]);
        node.label = strings[6];
        node.style = strings[7];
        node.shape = strings[8];
        node.color = strings[9];
        node.fillcolor = strings[10].TrimEnd('\r');

        return node;
    }

    private int CountNodes(string[] lines) {
        int count = 0;

        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].StartsWith("node ")) {
                count++;
            }
        }

        return count;
    }

    GVGraph ParseGraphLine(string graphString) {
        GVGraph graph = new GVGraph();

        string[] strings = graphString.Split(' ');

        Debug.Assert(strings.Length == 4);
        Debug.Assert(strings[0] == "graph");

        graph.scalefactor = float.Parse(strings[1]);
        graph.width = float.Parse(strings[2]);
        graph.height = float.Parse(strings[3]);

        return graph;
    }
}
