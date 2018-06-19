using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static GameManager instance = null;
	[SerializeField]Quest selectedQuest;
	List<Character> playerCharacters = new List<Character>();
	[SerializeField]Character pcPrefab;
	[SerializeField]StandartStats pcStandartStats;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		playerCharacters.Add (Instantiate(pcPrefab));
		playerCharacters.Add (Instantiate(pcPrefab));
		foreach (Character pc in playerCharacters) {
			pc.setStats (pcStandartStats);
		}
		DontDestroyOnLoad (this);
	}

	//TODO colocar outra coisa para se responsabilizar por mudar de cenas?
	public void CallNextScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}

	public Quest getSelectedQuest(){return selectedQuest;}
	public static GameManager getInstance(){return instance;}
	public List<Character> getPlayerCharacters(){return playerCharacters;}
}