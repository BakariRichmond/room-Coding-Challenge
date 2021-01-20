using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject quitMenu;
      [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerSpawnLocation;
     [SerializeField]
    private GameObject Camera;

    // Start is called before the first frame update
    void Start () {

    }
    void Awake () {
        quitMenu.SetActive (false);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown ("escape")) {
            if (!quitMenu.active) {
                quitMenu.SetActive (true);
            } else {
                quitMenu.SetActive (false);
            }
        }

    }
    public void StartGame(){
        Destroy(Camera);
        GameObject player = Instantiate (playerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
        player.transform.position = playerSpawnLocation.position;

    }

    public void QuitGame(){
        Application.Quit ();
    }
    public void CloseMenu(GameObject menu){
        menu.SetActive(false);

    }
}