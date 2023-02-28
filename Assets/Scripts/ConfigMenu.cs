using UnityEngine;
public class ConfigMenu : MonoBehaviour {
	public GameObject NOVRstuff; //single camera
	public GameObject VRStuff; //Two eyed camera
	float testTime = 10f; //time to go back from vr
						  // Use this for initialization



	public Animator mAnim;
	public GameObject mFader;
	public GameObject mFadeIn;


	void Start () {
		EndTest ();
		Invoke("DeactivateFader", 1.15f);
	}
	//Load next scene when continue is pressed
	public void LoadNextScene(){
		this.enabled = false; //Security check to avoid multiple instances
		//Load next scene (hardcoded)
		mFader.SetActive(true);

		Invoke("Play", 1.15f);
		mAnim.Play("FadeOut");

	}
	//Deactivates non vr camera, and activates vr
	public void StartTest(){
		NOVRstuff.SetActive (false);
		VRStuff.SetActive (true);
		Invoke ("EndTest", testTime);
	}
	//Deactivates vr camera and deactivates non vr
	public void EndTest(){
		VRStuff.SetActive (false);
		NOVRstuff.SetActive (true);
	}
	void Play()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(2);
	}
	void DeactivateFader()
	{
		mFadeIn.SetActive(false);
	}

}
