using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace AJ
{
    public class Node
    {
        public bool isBlocked;
        public bool isGoal;
    
    }
   
    public class SecondWorldScanner : MonoBehaviour
    {
        public Node[,] Nodes;
        public int     xNum = 50;
        public int     zNum = 50;
        public LayerMask layerMask;
        public LayerMask goalMask;
   
        // Start is called before the first frame update
        void Start()
        {
            // Create the actual array
            Nodes = new Node[xNum,zNum];
   		
            for (int x = 0; x < xNum; x++)
            {
                for (int z = 0; z < zNum; z++)
                {
                    Nodes[x,z] = new Node();
   				
                    if (Physics.CheckBox(transform.position + new Vector3(x, 0, z), new Vector3(0.5f, 50f, 0.5f), Quaternion.identity, layerMask.value))
                    {
                        Nodes[x, z].isBlocked = true;
                    }
                
                    if (Physics.CheckBox(transform.position + new Vector3(x, 0, z), new Vector3(0.5f, 50f, 0.5f), Quaternion.identity, goalMask.value))
                    {
                        Nodes[x, z].isGoal = true;
                    }
                }
            }
        }
   
        void OnDrawGizmos()
        {
   		
            Vector3 transformPosition = transform.position;
            Vector3 size              = new Vector3(0.5f, 0.5f, 0.5f);
   
            for (int x = 0; x < xNum; x++)
            {
                for (int z = 0; z < zNum; z++)
                {
                    if (Nodes != null && Nodes[x,z].isBlocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(transformPosition + new Vector3(x, 0, z), size);
                    }
                
                    if (Nodes != null && Nodes[x,z].isGoal)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawCube(transformPosition + new Vector3(x, 0, z), size);
                    }
                }
            }
        }
    }
}