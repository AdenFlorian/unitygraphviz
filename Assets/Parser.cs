using UnityEngine;
using System.Collections;

public class Parser {

    string graphSourceString;

    public Parser (string graphSourceString) {
        this.graphSourceString = graphSourceString;
    }

    public static GVGraph ParseGraphSource(string source) {
        GVGraph graph = null;

        string[] lines = source.Split('\n');

        Debug.Log("Lines...");
        for (int i = 0; i < lines.Length; i++) {
            Debug.Log("Line " + i + ": " + lines[i]);
        }
        Debug.Log("...End Lines");

        graph = ParseGraphSource(lines[0]);

        return graph;
    }

    GVGraph ParseGraphLine(string graphString) {
        GVGraph graph = null;

        return graph;
    }
}
