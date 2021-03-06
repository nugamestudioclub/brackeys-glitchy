using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {
	public static void ToMenu() {
		if (GameMemory.Instance.AllGamesWon())
        {
			SceneManager.LoadScene("Final_Ending", LoadSceneMode.Single);
		} else
        {
			SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
		}
		
	}
	public static void ToPlatformer() {
		GameMemory.Instance.Load(GameId.Platformer);
		SceneManager.LoadScene("Platformer_Scene", LoadSceneMode.Single);
	}

	public static void ToPlatformerEnd() {
		SceneManager.LoadScene("Platformer_Ending", LoadSceneMode.Single);
		GameMemory.Instance.WinGame(GameId.Platformer);
	}

	public static void ToTanks()
	{
		GameMemory.Instance.Load(GameId.Tanks);
		SceneManager.LoadScene("Tank_Scene", LoadSceneMode.Single);
	}

	public static void ToTankEnd() {
		SceneManager.LoadScene("Tank_Ending", LoadSceneMode.Single);
		GameMemory.Instance.WinGame(GameId.Tanks);
	}

	public static void ToSpace()
	{
		GameMemory.Instance.Load(GameId.SpaceShooter);
		SceneManager.LoadScene("Space_Scene", LoadSceneMode.Single);
	}

	public static void ToSpaceEnd() {
		SceneManager.LoadScene("Space_Ending", LoadSceneMode.Single);
		GameMemory.Instance.WinGame(GameId.SpaceShooter);
	}

	public static void ToCredits() {
		SceneManager.LoadScene("Credits_Scene", LoadSceneMode.Single);
	}

	public static void Quit() {
		Application.Quit();
	}
}