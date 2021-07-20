using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	[SerializeField] private CameraMovement _camera;

	// 2-D array structure that stores all the outfits
	private Material[][] outfits = new Material[3][];
	private int selectedSection = 0;

	// Character model
	[SerializeField] private GameObject player;

	[SerializeField] private Material[] face;
	[SerializeField] private Material[] body;
	[SerializeField] private Material[] exp;
	private int selectedItem = 0;

	private int[] PlayerData = new int[3];

	// Random outfits will be generated after startup
	void Start()
	{
		for (int i = 0; i < PlayerData.Length; i++)
		{
			PlayerData[i] = 0;
		}

		outfits[0] = body;
		outfits[1] = face;
		outfits[2] = exp;
		Randomize();
	}

	float speed = 1f;
	float delta = 0.33f;  //delta is the difference between min y to max y.

	// Fancy Stuff
	void Update() 
	{
		float y = Mathf.Sin(speed * Time.time) * delta;
		Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
		transform.position = pos;

		//Debug.Log(PlayerData[0] + " " +  PlayerData[1] + " " + PlayerData[2]);
	}

	/*
	 * The following two functions changes the item in a specific outfit category
	 */
	public void NextItem()
	{
		selectedItem = PlayerData[selectedSection];
		selectedItem++;
		if (selectedItem >= outfits[selectedSection].Length)
		{
			selectedItem = 0;
		}
		Material[] mats = player.GetComponent<MeshRenderer>().materials;
		mats[selectedSection] = outfits[selectedSection][selectedItem];
		player.GetComponent<MeshRenderer>().materials = mats;

		// Remeber the player's choice in section
		PlayerData[selectedSection] = selectedItem;
	}

	public void PreviousItem()
	{
		selectedItem = PlayerData[selectedSection];
		selectedItem--;
		if (selectedItem < 0)
		{
			selectedItem += outfits[selectedSection].Length;
		}
		Material[] mats = player.GetComponent<MeshRenderer>().materials;
		mats[selectedSection] = outfits[selectedSection][selectedItem];
		player.GetComponent<MeshRenderer>().materials = mats;

		// Remeber the player's choice in section
		PlayerData[selectedSection] = selectedItem;
	}

	/*
	 * The following two functions changes the outfit category
	 */
	public void NextSection()
	{
		selectedSection = selectedSection + 1;
		if (selectedSection >= outfits.Length) 
		{
			selectedSection = 0;
		}

		_camera.SetCurView(selectedSection);
	}

	public void PreviousSection()
	{
		selectedSection--;
		if (selectedSection < 0)
		{
			selectedSection += outfits.Length;
		}

		_camera.SetCurView(selectedSection);
	}

	public void StartGame()
	{
		PlayerPrefs.SetInt("selectedCharacter", PlayerData[0]);
		PlayerPrefs.SetInt("selectedDeco", PlayerData[1]);
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	public void BackToTitle()
	{
		#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	public void Randomize()
	{
		int selectedItem1 = Random.Range(0, body.Length);
		Material[] mats = player.GetComponent<MeshRenderer>().materials;
		mats[0] = outfits[0][selectedItem1];
		player.GetComponent<MeshRenderer>().materials = mats;

		PlayerData[0] = selectedItem1;
		//Debug.Log("Data1: " + selectedItem1);

		int selectedItem2 = Random.Range(0, face.Length);
		mats = player.GetComponent<MeshRenderer>().materials;
		mats[1] = outfits[1][selectedItem2];
		player.GetComponent<MeshRenderer>().materials = mats;

		PlayerData[1] = selectedItem2;
		//Debug.Log("Data2: " + selectedItem2);

		int selectedItem3 = Random.Range(0, exp.Length);
		mats = player.GetComponent<MeshRenderer>().materials;
		mats[2] = outfits[2][selectedItem3];
		player.GetComponent<MeshRenderer>().materials = mats;

		PlayerData[2] = selectedItem3;
		//Debug.Log("Data3: " + selectedItem3);
	}
}
