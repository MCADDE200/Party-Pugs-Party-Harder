using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataScript : MonoBehaviour {

    public int selectedSkin;
    public int tickets;
    //Power up charges
    public int goldenBone;
    public int crossPug;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);

        tickets = 0;
        goldenBone = 0;
        crossPug = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
