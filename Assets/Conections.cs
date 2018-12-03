using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conections : MonoBehaviour {
    public Cor[] cores;
    private bool[,] connectionsMatrix;
    // Use this for initialization
    void Start() {
        connectionsMatrix = new bool[,]{{false, true, true, true, true, true, true, false, false, false, false, false},
                                        {false, false, true, true, true, true, true, true, false, false, false, false},
                                        {false, false, false, true, true, true, true, true, true, false, false, false},
                                        {false, false, false, false, true, true, true, true, true, true, false, false},
                                        {false, false, false, false, false, true, true, true, true, true, true, false},
                                        {false, false, false, false, false, false, true, true, true, true, true, true},
                                        {true, false, false, false, false, false, false, true, true, true, true, true},
                                        {true, true, false, false, false, false, false, false, true, true, true, true},
                                        {true, true, true, false, false, false, false, false, false, true, true, true},
                                        {true, true, true, true, false, false, false, false, false, false, true, true},
                                        {true, true, true, true, true, false, false, false, false, false, false, true},
                                        {true, true, true, true, true, true, false, false, false, false, false, false}};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int FindInList(Cor c) {
        for (int i = 0; i < cores.Length; i++) {
            if(cores[i] == c) {
                return i;
            }
        }
        return 0;
    }
    
    public bool DoesConect(Cor c, Cor d) {
        return connectionsMatrix[FindInList(c), FindInList(d)];
    }
}
