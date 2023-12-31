using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GhostScript : CharacterBase
{
    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    float speed = 5;
    int frameCount = 5;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isGhost = true;
        base.Start();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        m_SpriteRenderer.color = Color.white;
    }
    
    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("isLevel", level);    
        speed = 5 + Mathf.Sqrt(level);
        //Debug.Log("Ghost Speed = ", speed);
        if(controlled){
            m_SpriteRenderer.color = Color.white;
            if(Input.GetKey("w")){
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(velo.x, speed, 0);
            } else if(Input.GetKey("s")){
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(velo.x, -speed, 0);
            } else {
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(velo.x, 0, 0);
            }
    
            if(Input.GetKey("a")){
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, velo.y, 0);
                m_SpriteRenderer.flipX= false;

            } else if(Input.GetKey("d")){
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(speed, velo.y, 0);
                m_SpriteRenderer.flipX= true;
            } else {
                Vector3 velo = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, velo.y, 0);
            }
        } else {
            if (level<=10){
                m_SpriteRenderer.color = Color.yellow;
            } else if (level>10 && level<=20){
                m_SpriteRenderer.color = Color.blue;
            } else if (level>20 && level<=30){
                m_SpriteRenderer.color = Color.red;
            } else{
                m_SpriteRenderer.color = Color.white;
            }


        }
        base.Update();
        
    }

    void FixedUpdate(){

        if(!controlled){

            CharacterScript player = GameObject.Find("Characters").GetComponent<CharacterScript>();
            if(frameCount <= 0){
                if(Random.Range(0, 10) >= 5 - Mathf.Sqrt(level)){
                    transform.position = Vector2.MoveTowards(transform.position, player.currentPlayer.transform.position, speed*Time.deltaTime);
                } else {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-.75f, .75f)*speed, Random.Range(-.75f, .75f)*speed, 0);
                }
                
                frameCount = 25;
            }

            frameCount--;
        //-114, -5
        //93, 30
            if(transform.position.x <= -63){
                transform.position = new Vector3(-63, transform.position.y, 0);
            } else if(transform.position.x >= 42){
                transform.position = new Vector3(42, transform.position.y, 0);
            }

            if(transform.position.y <= 6){
                transform.position = new Vector3(transform.position.x, 6, 0);
            } else if(transform.position.y >= 71){
                transform.position = new Vector3(transform.position.x, 71, 0);
            }

            
        }
        
    }
}
