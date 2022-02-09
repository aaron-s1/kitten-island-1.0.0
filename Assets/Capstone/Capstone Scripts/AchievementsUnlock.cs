using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsUnlock : MonoBehaviour {

	// Achievements Menu management

	[Space(10)]
	public GameObject catLockedIcon;
	public GameObject catUnlockedIcon;

	[Space(7)]
	public GameObject sunLockedIcon;
	public GameObject sunUnlockedIcon;

	[Space(7)]
	public GameObject fishLockedIcon;
	public GameObject fishUnlockedIcon;


	public void CatAchievementUnlocked() {
		catLockedIcon.SetActive(false);
		catUnlockedIcon.SetActive(true);
	}

	public void SunAchievementUnlocked() {
		sunLockedIcon.SetActive(false);
		sunUnlockedIcon.SetActive(true);
	}

	public void FishAchievementUnlocked() {
		fishLockedIcon.SetActive(false);
		fishUnlockedIcon.SetActive(true);
	}
}
