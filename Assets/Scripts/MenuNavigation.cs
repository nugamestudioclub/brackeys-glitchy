using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {
	[SerializeField]
	private List<Button> buttons = new List<Button>();

	[ReadOnly]
	[SerializeField]
	private int index;

	[ReadOnly]
	[SerializeField]
	private bool inputEnabled;

	[SerializeField]
	private List<KeyCode> clickKeys = new List<KeyCode>();

	[SerializeField]
	private List<KeyCode> previousKeys = new List<KeyCode>();

	[SerializeField]
	private List<KeyCode> nextKeys = new List<KeyCode>();

	[SerializeField]
	private float waitForSeconds = 2.0f;

	private void Awake() {
		index = 0;
		Select();
		inputEnabled = false;
	}

	void Start() {
		StartCoroutine(Wait());
		inputEnabled = true;
	}

	private void Update() {
		if( !inputEnabled )
			return;

		if( GetAnyDown(clickKeys) ) {
			Click();
		}
		else if( GetAnyDown(previousKeys) ) {
			Previous();
		}
		else if( GetAnyDown(nextKeys) ) {
			Next();
		}
	}

	private void Select() {
		buttons[index].Select();
	}

	private void Click() {
		buttons[index].onClick.Invoke();
	}

	public void Previous() {
		index = (--index + buttons.Count) % buttons.Count;
		Select();

	}

	public void Next() {
		index = ++index % buttons.Count;
		Select();
	}

	public static bool GetAnyDown(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKeyDown(key) )
				return true;
		return false;
	}

	public static bool GetAny(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKey(key) )
				return true;
		return false;
	}

	public static bool GetAnyUp(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKeyUp(key) )
				return true;
		return false;
	}

	private IEnumerator Wait() {
		yield return new WaitForSeconds(waitForSeconds);
	}
}