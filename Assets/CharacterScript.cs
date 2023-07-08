using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public CharacterBase currentPlayer;
    CharacterBase currentGhost;
    bool currentlyGhost = true;
    bool pDown = false;
    // Start is called before the first frame update
    void Start()
    {
        CharacterBase[] children = this.GetComponentsInChildren<CharacterBase>();
        for(int i = 0; i < children.Length; i++){
            children[i].controlled = false;
        }
        currentPlayer.controlled = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey("p") && !pDown){
            if(currentlyGhost){
                CharacterBase[] children = this.GetComponentsInChildren<CharacterBase>();
                CharacterBase closest = currentPlayer;
                float closestdist = 1000000;
                for(int i = 0; i < children.Length; i++){
                     
                    if(!children[i].isGhost){
                        float distance = Vector3.Distance(currentPlayer.transform.position, children[i].transform.position);
                        Debug.Log(distance);
                        if(distance < closestdist){
                            closest = children[i];
                            closestdist = distance;
                        }
                    }
                }
                Debug.Log(closestdist);
                if(closestdist <= 2){
                    currentlyGhost = false;
                    currentGhost = currentPlayer;
                    currentPlayer = closest;
                    currentPlayer.controlled = true;
                    currentGhost.gameObject.SetActive(false);
                }
            } else {
                Debug.Log("not ghost");
                currentGhost.transform.position = currentPlayer.transform.position;
                currentPlayer.controlled = false;
                currentPlayer = currentGhost;
                currentPlayer.gameObject.SetActive(true);
                currentPlayer.controlled = true; 
                currentlyGhost = true;
            }
        } 
        pDown = Input.GetKey("p");





    }
}