using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Ink.Runtime;
using System.Collections;
using Cinemachine;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {

    public static event Action<Story> OnCreateStory;
	public PlayerController player;
	
    void Awake ()
	{
		_audioSource = GetComponent<AudioSource>();
		InitializeAudioClips();

		// Remove the default message
		RemoveChildren();
		StartStory();
	}

    private void InitializeAudioClips()
    {
        foreach(var clip in _audioClips)
        {
			_clips.Add(clip.name
				.ToLower()
				.Replace(" ", " "), clip);
        }
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
		RefreshView();
        StartCoroutine(RefreshView());

		// Disable PlayerController script
		player.enabled = false;

	}

	void Endstory()
    {
		RemoveChildren();
	}

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!

    private IEnumerator RefreshView ()
	{
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue)
		{
			while (_audioSource.isPlaying)
				yield return null;

			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);

			// Add audio clip #
			foreach(var tag in story.currentTags)
            {
				if(tag.StartsWith("Clip."))
                {
					var clipName = tag.Substring("Clip.".Length, tag.Length - "Clip.".Length);
					PlayClip(clipName);
                }

				else if (tag.StartsWith("Camera."))
                {
					var cameraName = tag.Substring("Camera.".Length, tag.Length - "Camera.".Length);
					var allCameras = FindObjectsOfType<CinemachineVirtualCamera>();
					foreach(var camera in allCameras)
                    {
						camera.Priority = camera.name == cameraName ? 100: 10;
                    }
                }
            }
		}

		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0)
		{
			foreach (var choice in story.currentChoices)
			{
				//Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView(choice.text.Trim());
				// Tell the button what to do when we press it

				button.onClick.AddListener(delegate
				{
					OnClickChoiceButton(choice);
				});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			Button choice = CreateChoiceView("Let's kill a troll!");
			choice.onClick.AddListener(delegate{
				//StartStory();
				Endstory();

				// Disable PlayerController script
				player.enabled = true;
			});
		}
	}

    private void PlayClip(string clipName)
    {
        if (_clips.TryGetValue(clipName.ToLower(), out var clip))
        {
			_audioSource.PlayOneShot(clip);
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
		StartCoroutine (RefreshView());
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	[SerializeField] private TextAsset inkJSONAsset = null;
	public Story story;

	[SerializeField] private Canvas canvas = null;

	// UI Prefabs
	[SerializeField] Text textPrefab = null;
    [SerializeField] Button buttonPrefab = null;
    [SerializeField] List<AudioClip> _audioClips;

	Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
	AudioSource _audioSource;
}
