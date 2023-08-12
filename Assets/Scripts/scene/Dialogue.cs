using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace scene
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject dialoguePrefab; // Prefab containing UI elements for dialogue
        public string[] dialogueText; // Array of strings containing the dialogue
        public float textSpeed = 0.05f; // Speed at which text is displayed
        public Sprite[] speakers;
        public int[] speakerIndex;

        private GameObject _dialogueInstance; // Instance of the dialogue UI
        private TextMeshProUGUI _dialogueUIText; // Reference to the Text component in the dialogue UI
        private Image _dialogueUISpeaker; // Reference to the Image component in the dialogue UI
        private int _currentLine; // Current line of dialogue being displayed
        private bool _isTalking;
        private bool _isOn;

        // Start dialogue when something triggers it, for example a button press
        public void StartDialogue()
        {
            _isOn = true;
            _dialogueInstance = Instantiate(dialoguePrefab, transform.position, Quaternion.identity, transform);
            _dialogueUIText = _dialogueInstance.GetComponentInChildren<TextMeshProUGUI>();
            _dialogueUIText.text = "";
            _dialogueUISpeaker = _dialogueInstance.transform.Find("Image").GetComponent<Image>();
            StartCoroutine(DisplayText());
        }

        // Coroutine to handle displaying text one character at a time
        private IEnumerator DisplayText()
        {
            _isTalking = true;

            _dialogueUISpeaker.sprite = speakers[speakerIndex[_currentLine]];
            foreach (char c in dialogueText[_currentLine])
            {
                _dialogueUIText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            _isTalking = false;
        }

        // Update method to handle player input
        private void Update()
        {
            // If the dialogue is done displaying and the player presses a key, move to the next line or end the dialogue
            if (_isOn && !_isTalking && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
            {
                _currentLine++;
                if (_currentLine < dialogueText.Length)
                {
                    _dialogueUIText.text = "";
                    StartCoroutine(DisplayText());
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        // Method to end the dialogue
        private void EndDialogue()
        {
            Destroy(_dialogueInstance);
            _currentLine = 0;
        }
    }
}