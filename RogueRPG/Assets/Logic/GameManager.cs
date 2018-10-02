using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static GameManager instance = null;
	[SerializeField]Quest selectedQuest;
	List<Character> playerCharacters = new List<Character>();
	[SerializeField]Character pcPrefab;
	[SerializeField]StandartStats pcGuerreiroStats;
	[SerializeField]StandartStats pcMagoStats;
	[SerializeField]List<StandartStats> pcStats;
	[SerializeField]List<string> pcNames;

	void Awake(){
		MakeItASingleton();

//		TODO Fill with players. Depois pensar em como fazer isso de maneira mais eficiente.

//		playerCharacters.Add (Instantiate(pcPrefab));
//		playerCharacters.Add (Instantiate(pcPrefab));
//		playerCharacters[0].setStats(pcGuerreiroStats);
//		playerCharacters [1].setStats (pcMagoStats);
//		playerCharacters [0].setName ("Dante");
//		playerCharacters [1].setName ("Roxas");

		DontDestroyOnLoad (this);
	}

	//TODO colocar outra coisa para se responsabilizar por mudar de cenas
	public void LoadDungeonScene(){
		SceneManager.LoadScene(BATTLE_SCENE_INDEX);
	}

	void MakeItASingleton(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public List<Character> getEnemiesAtFloor(int floor){return selectedQuest.getCurrentDungeon ().getBattleGroups () [floor].getEnemies ();}
	public List<Character> getEnemiesDelayedAtFloor (int floor){return selectedQuest.getCurrentDungeon().getBattleGroups()[floor].getEnemiesDelayed();}
	public List<StandartStats> getHeroesStats (){return pcStats;}
	public List<string> getHeroesNames (){return pcNames;}
	public Quest getSelectedQuest(){return selectedQuest;}
	public static GameManager getInstance(){return instance;}
	public List<Character> getPlayerCharacters(){return playerCharacters;}

	public const int BATTLE_SCENE_INDEX = 1;
}