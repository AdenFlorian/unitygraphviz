using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class gviz : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Start the child process.
        Process p = new Process();
        // Redirect the output stream of the child process.
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
        //p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\neato.exe";
        //p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\fdp.exe";
        p.StartInfo.Arguments = @"-Tplain H:\unity_projects\graphvizinunity\Assets\graph.gv";
        p.Start();
        // Do not wait for the child process to exit before
        // reading to the end of its redirected stream.
        //p.WaitForExit();
        // Read the output stream first and then wait.
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();

        //UnityEngine.Debug.Log("output: " + output);

        Parser parser = new Parser(new UnityLogger());

        GVGraph graph = parser.ParseGraphSource(output);

        DrawGraph(graph);
    }

    // Update is called once per frame
    void Update () {

    }

    private void DrawGraph(GVGraph graph) {
        // Graph outline
        {
            GameObject graphCubeGO = Instantiate(Resources.Load<GameObject>("graphCube"));
            graphCubeGO.transform.localScale = new Vector3(graph.width * 2, graph.height * 2, 4f);
            graphCubeGO.transform.position = new Vector3(0, 0, 0f);
        }

        // Nodes
        for (int i = 0; i < graph.nodes.Length; i++) {
            GameObject graphNodeGO = Instantiate(Resources.Load<GameObject>("graphNode"));
            graphNodeGO.transform.localScale = new Vector3(graph.nodes[i].xsize, graph.nodes[i].ysize, 0.5f);
            graphNodeGO.transform.position = new Vector3(graph.nodes[i].x, graph.nodes[i].y, 0f);
            graphNodeGO.GetComponentInChildren<Text>().text = graph.nodes[i].name + "|" + graph.nodes[i].label;
        }

        // Edges
        for (int i = 0; i < graph.edges.Length; i++) {
            GameObject lineGO = Instantiate(Resources.Load<GameObject>("line"));
            LineRenderer lineRenderer = lineGO.GetComponent<LineRenderer>();
            Vector3[] positions = new Vector3[graph.edges[i].pointCount];
            for (int j = 0; j < positions.Length; j++) {
                positions[j].x = graph.edges[i].points[j].x;
                positions[j].y = graph.edges[i].points[j].y;
            }

            Color lineColor;

            switch (graph.edges[i].color) {
                case "red":
                    lineColor = Color.red;
                    break;
                case "green":
                    lineColor = Color.green;
                    break;
                case "blue":
                    lineColor = Color.blue;
                    break;
                default:
                    lineColor = Color.black;
                    break;
            }

            lineRenderer.SetWidth(0.1f, 0.1f);
            lineRenderer.SetColors(lineColor, lineColor);
            lineRenderer.SetVertexCount(positions.Length);
            lineRenderer.SetPositions(positions);
        }
        
    }
}
