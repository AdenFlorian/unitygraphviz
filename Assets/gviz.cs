using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class gviz : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Start the child process.
        Process p = new Process();
        // Redirect the output stream of the child process.
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
        p.StartInfo.Arguments = @"-Tplain H:\unity_projects\graphvizinunity\Assets\graph.gv";
        p.Start();
        // Do not wait for the child process to exit before
        // reading to the end of its redirected stream.
        //p.WaitForExit();
        // Read the output stream first and then wait.
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();

        UnityEngine.Debug.Log("output: " + output);

        Parser.ParseGraphSource(output);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
