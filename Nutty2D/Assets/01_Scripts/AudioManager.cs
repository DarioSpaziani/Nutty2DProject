using System;
using Nutty2D;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
	public AudioSource audioSource;

	public AudioClip winSound, loseSound, drinkSound, trashSound;
	
	private void Awake() {
		OnReload();
	}

	public void PlayWinSound() {
		audioSource.PlayOneShot(winSound);
	}

	public void PlayLoseSound() {
		audioSource.PlayOneShot(loseSound);
	}
	
	public void PlayDrinkSound() {
		audioSource.PlayOneShot(drinkSound);
	}
	
	public void PlayTrashSound() {
		audioSource.PlayOneShot(trashSound);
	}
	

}
