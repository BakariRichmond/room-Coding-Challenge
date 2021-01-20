using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CycleText : MonoBehaviour {
    [SerializeField]
    private float cycleTime = 1f;
    private IEnumerator coroutine;
    [SerializeField]
    private List<string> textList = new List<string> ();
    [SerializeField]
    private bool waiting = false;
    private int index;
    private TextMeshPro textmeshPro;
    // Start is called before the first frame update
    void Start () {

        textmeshPro = GetComponent<TextMeshPro> ();
    }

    // Update is called once per frame
    void Update () {
        //calls cycleNext coroutine after waiting is finished
        if (!waiting) {

            waiting = true;
            coroutine = CycleNext ();
            StartCoroutine (coroutine);

        }

    }
    IEnumerator CycleNext () {

        yield return new WaitForSeconds (cycleTime);
        //sets text to the next index in the text list, and repeats at the end
        if (textList.Count >= 1) {
            textmeshPro.SetText (textList[index]);

            if (index >= textList.Count - 1) {
                index = 0;
            } else {
                index++;
            }
        } else { Debug.Log ("textList is empty"); }
        waiting = false;

    }
}