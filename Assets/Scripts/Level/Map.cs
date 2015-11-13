using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject[] mapParts = GameObject.FindGameObjectsWithTag("Quad");

        foreach (GameObject go in mapParts)
        {
            Transform quad = go.transform;
            Texture2D texture = new Texture2D(Mathf.CeilToInt(quad.localScale.x),
                                               Mathf.CeilToInt(quad.localScale.y));

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    float random = Random.Range(0.925f, 0.95f);
                    Color color = new Color(random, random, random);
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;

            quad.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }

//		Transform padContainer = transform.FindChild ("PadContainer");
//		for (int i = 0; i < padContainer.childCount; i++) {
//			Transform child = padContainer.GetChild(i);
//
//			if(child.name == "Quad")
//			{
//				float random = Random.Range(0.8f, 0.9f);
//				child.GetComponent<Renderer>().material.SetColor("_Color", new Color(random,random,random));
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
