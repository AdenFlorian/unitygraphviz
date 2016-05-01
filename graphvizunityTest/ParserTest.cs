using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using UnitTestProject1;
using UnityEngine;

[TestClass]
public class ParserTest {
    [TestMethod]
    public void ParseGraphSourceTest() {

        var fileStream = new FileStream("graph_plain_01", FileMode.Open);
        StreamReader strmRdr = new StreamReader(fileStream, Encoding.UTF8);
        string source = strmRdr.ReadToEnd();

        Parser parser = new Parser(new TestLogger());

        GVGraph graph = parser.ParseGraphSource(source);

        Assert.IsNotNull(graph);
        Assert.AreEqual(1f, graph.scalefactor);
        Assert.AreEqual(0.75f, graph.width);
        Assert.AreEqual(1.5f, graph.height);

        Assert.AreEqual(2, graph.nodes.Length);

        Assert.AreEqual("a", graph.nodes[0].name);
        Assert.AreEqual(0.375f, graph.nodes[0].x);
        Assert.AreEqual(1.25f, graph.nodes[0].y);
        Assert.AreEqual(0.75f, graph.nodes[0].xsize);
        Assert.AreEqual(0.5f, graph.nodes[0].ysize);
        Assert.AreEqual("a", graph.nodes[0].label);
        Assert.AreEqual("solid", graph.nodes[0].style);
        Assert.AreEqual("ellipse", graph.nodes[0].shape);
        Assert.AreEqual("black", graph.nodes[0].color);
        Assert.AreEqual("lightgrey", graph.nodes[0].fillcolor);

        Assert.AreEqual("b", graph.nodes[1].name);
        Assert.AreEqual(0.375f, graph.nodes[1].x);
        Assert.AreEqual(0.25f, graph.nodes[1].y);
        Assert.AreEqual(0.75f, graph.nodes[1].xsize);
        Assert.AreEqual(0.5f, graph.nodes[1].ysize);
        Assert.AreEqual("b", graph.nodes[1].label);
        Assert.AreEqual("solid", graph.nodes[1].style);
        Assert.AreEqual("ellipse", graph.nodes[1].shape);
        Assert.AreEqual("black", graph.nodes[1].color);
        Assert.AreEqual("lightgrey", graph.nodes[1].fillcolor);

        Assert.AreEqual(1, graph.edges.Length);
        Assert.AreEqual("a", graph.edges[0].tailNodeName);
        Assert.AreEqual("b", graph.edges[0].headNodeName);
        Assert.AreEqual(4, graph.edges[0].pointCount);
        Assert.AreEqual(4, graph.edges[0].points.Length);
        Assert.AreEqual(new Vector2(0.375f, 0.99579f), graph.edges[0].points[0]);
        Assert.AreEqual(new Vector2(0.375f, 0.84509f), graph.edges[0].points[1]);
        Assert.AreEqual(new Vector2(0.375f, 0.65162f), graph.edges[0].points[2]);
        Assert.AreEqual(new Vector2(0.375f, 0.50145f), graph.edges[0].points[3]);
        Assert.AreEqual(null, graph.edges[0].label);
        Assert.AreEqual(Vector2.zero, graph.edges[0].labelPosition);
        Assert.AreEqual("solid", graph.edges[0].style);
        Assert.AreEqual("black", graph.edges[0].color);
    }
}
