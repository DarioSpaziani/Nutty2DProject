using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Nutty2D {

	public class Manager : Singleton<Manager> {
		public int winningPoints = 3;
		public int[] scores;

		public List<Transform> spawnPoints;
		public List<GameObject> bottles;

		private GameObject activeBottle;

		public GameObject startCanvas;
		public GameObject optionCanvas;
		public GameObject gameCanvas;

		public TextMeshProUGUI startCanvasText;
		public TextMeshProUGUI optionCanvasText;

		private bool optionOpen;
		private bool wantStart;
		private bool hasWin;

		public Sprite normalSprite, ripSprite, happySprite, angrySprite;
		public Image optionNuttyImage;

		public GameObject resumeButton, nextLevel;

		private void Awake() {
			OnReload();
		}

		private void Start() {
			StartCoroutine(MainRoutine());

			scores = new int[bottles.Count];
		}

		private IEnumerator MainRoutine() {
			
			yield return StartCoroutine(StartPhase());
			
			yield return StartCoroutine(MainPhase());

			optionCanvas.SetActive(true);
			if (!hasWin) {
				AudioManager.Instance.PlayLoseSound();
				optionNuttyImage.sprite = ripSprite;
				optionCanvasText.text = "Emh, sei forse abituato ad alzare il gomito?";
				resumeButton.SetActive(false);
			}
			
			else {
				AudioManager.Instance.PlayWinSound();
				optionNuttyImage.sprite = happySprite;
				optionCanvasText.text = "FOOOORTE, sei stato bravo!";
				resumeButton.SetActive(false);
				nextLevel.SetActive(true);
			}
		}

		private IEnumerator StartPhase() 
		{
			optionCanvas.SetActive(false);
			gameCanvas.SetActive(false);
			startCanvas.SetActive(true);
			nextLevel.SetActive(false);
			resumeButton.SetActive(true);
			startCanvasText.text = "BENVENUTO! \n" + "Fai bere a nutty " + winningPoints + " bottiglie di ogni tipo, se sfori nutty sviene!";

			while (!wantStart) {
				yield return null;
			}
			

			optionCanvas.SetActive(false);
			gameCanvas.SetActive(true);
			startCanvas.SetActive(false);
		}

		private IEnumerator MainPhase() {
			SpawnBottles();
			bool endPhase = false;

			while (!endPhase) {
				hasWin = false;

				//Controllo se uno score ha superato il limite consentito e nel caso usciamo dal loop while con hasWin a false
				foreach (int score in scores) {
					if (score > winningPoints) {
						print("hai perso");
						hasWin = false;
						endPhase = true;
						break;
					}
				}


				//Controlliamo se tutti gli score sono uguali al winning point, usciamo con hasWin a true;
				if (!endPhase) {
					foreach (int score in scores) {
						if (score == winningPoints) {
							hasWin = true;
							endPhase = true;
						}
						else {
							hasWin = false;
							endPhase = false;
							break;
						}
					}
				}
				yield return null;
			}
			if (hasWin)
				print("HAI VINTO!");
		}

		public void SpawnBottles() {
			if (activeBottle != null)
				Destroy(activeBottle);

			int spawnIndex = Random.Range(0, spawnPoints.Count);
			int bottleIndex = Random.Range(0, bottles.Count);

			activeBottle = Instantiate(bottles[bottleIndex], spawnPoints[spawnIndex]);
		}

		public void StartGame() {
			wantStart = true;
		}

		public void Score(int bottleIndex) {
			AudioManager.Instance.PlayDrinkSound();
			scores[bottleIndex]++;

			print("Rosso :" + scores[0] + " Blu :" + scores[1] + " Verde:" + scores[2]);
		}

		public void Option() {
			gameCanvas.SetActive(false);
			optionCanvas.SetActive(true);
			
			resumeButton.SetActive(true);
			optionNuttyImage.sprite = angrySprite;

			optionCanvasText.text = "OPZIONI";
		}

		public void Resume() {
			gameCanvas.SetActive(true);
			optionCanvas.SetActive(false);
		}

		public void Restart() {
			SceneManager.LoadScene(0);
		}

		public void NextLevel() {

			winningPoints *= 2;
			optionCanvas.SetActive(false);
			startCanvas.SetActive(true);
			scores = new int[bottles.Count];
			wantStart = false;

			StartCoroutine(MainRoutine());
		}

		public void Quit() {
			Application.Quit();
		}
	}
}