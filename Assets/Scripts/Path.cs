using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

    public Color lineColor;
    //list where nodes are stored
    private List<Transform> nodes = new List<Transform>();
    //function for pathing lines
    //OnDrawGizmos if outline, Selected if hidden
    void OnDrawGizmos() {
        Gizmos.color = lineColor;
        //function to draw the line between nodes
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        //loop through nodes
        for(int i = 0; i < pathTransforms.Length; i++) {
            if(pathTransforms[i] != transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
        //draw a line between nodes
        for(int i = 0; i < nodes.Count; i++) {
            Vector3 currentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;

            if (i > 0) {
                previousNode = nodes[i - 1].position;
            }

            Gizmos.DrawLine(previousNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }

}
