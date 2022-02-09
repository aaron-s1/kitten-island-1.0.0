using UnityEngine;
using System.Collections;

public class CreateOcean : MonoBehaviour 
{
	public Material ocean_material;
	public Renderer ocean_renderer;
	public GameObject oceanPerimeter;



	void Start () 
	{
		
		ocean_renderer = Ocean.gameObject.GetComponent<MeshRenderer>();
		ocean_renderer.receiveShadows = false;
		//ocean_renderer.reflectionProbeUsage = false;

		Ocean.gameObject.SetActive(true);	
		Ocean.gameObject.transform.position 					= new Vector3(0.0f, 1.40f, 0.0f);
		Ocean.gameObject.transform.localScale 					= new Vector3(400f, 60f, 400f);
		//Ocean.gameObject.AddComponent<ThumpCollide>();


		Ocean.gameObject.GetComponent<MeshRenderer>().material 	= ocean_material;

		Ocean.audio_source.volume 						= 0.125f;
		Ocean.audio_source.Play();
		Ocean.gameObject.tag = "Ocean";

		// turn on ocean perimeter
		transform.GetChild(1).gameObject.SetActive(true);
	}


	void Update () 
	{
		Ocean.AdjustPitch();
		Ocean.SetSoundPositionRelativeToViewer();
	}



	/*
	void OnGUI()
	{
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(16.0f, 0.0f, 128.0f, 24.0f), Framerate.Fps().ToString());
	}*/
}