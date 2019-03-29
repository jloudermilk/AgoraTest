using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
#if(UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif

public class TestHome : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if(UNITY_2018_3_OR_NEWER)
		if (Permission.HasUserAuthorizedPermission(Permission.Microphone)){
		
		} else {
			Permission.RequestUserPermission(Permission.Microphone);
		}

		if (Permission.HasUserAuthorizedPermission(Permission.Camera)){
		
		} else {
			Permission.RequestUserPermission(Permission.Camera);
		}
		#endif
	}

	// Update is called once per frame
	void Update () {
	}

	static TestHelloUnityVideo app = null;

	private void onJoinButtonClicked() {
		// get parameters (channel name, channel profile, etc.)
		GameObject go = GameObject.Find ("ChannelName");
		InputField field = go.GetComponent<InputField>();

		// create app if nonexistent
		if (ReferenceEquals (app, null)) {
			app = new TestHelloUnityVideo (); // create app
			app.loadEngine (); // load engine
		}

		// join channel and jump to next scene
		app.join (field.text);
		SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
		SceneManager.LoadScene ("TestSceneHelloVideo", LoadSceneMode.Single);
	}

	private void onLeaveButtonClicked() {
		if (!ReferenceEquals (app, null)) {
			app.leave (); // leave channel
			app.unloadEngine (); // delete engine
			app = null; // delete app
			SceneManager.LoadScene ("TestSceneHome", LoadSceneMode.Single);
		}
	}

	public void onButtonClicked() {
		// which GameObject?
		if (name.CompareTo ("JoinButton") == 0) {
			onJoinButtonClicked ();
		}
		else if(name.CompareTo ("LeaveButton") == 0) {
			onLeaveButtonClicked ();
		}
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		if (scene.name.CompareTo("TestSceneHelloVideo") == 0) {
			if (!ReferenceEquals (app, null)) {
				app.onSceneHelloVideoLoaded (); // call this after scene is loaded
			}
			SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		}
	}
}
