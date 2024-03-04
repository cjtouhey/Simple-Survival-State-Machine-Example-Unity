using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class playerSystem : MonoBehaviour
{
    public float PlayerHealth = 100;
    public float PlayerSpeed;

    public float PlayerTemp = 36;
    public float PlayerHunger = 100;
    public float PlayerThirst = 100;

    public  playerCondition condition;
    public playerHunger hunger;
    public  playerThirst thirst;
    public playersInjuries  injuries;
    public insideOutSide sheltered;
   

    PlayerManager manager;
    timeManager time;
    
    float decreaseRate = 0.05f;
    float increaseRate = 0.1f;

    public DynamicMoveProvider controller;

    public bool isNight;
    public bool startNeeds;
    public bool stopNeeds; 
    bool control; 

    private void Start()
    {
        controller = gameObject.GetComponentInChildren<DynamicMoveProvider>();
        manager = GetComponent<PlayerManager>();
        time = GameObject.FindFirstObjectByType<timeManager>();
        PlayerSpeed = Controls.playerMoveSpeed ;
        controller.moveSpeed = PlayerSpeed;

        
    }
    // Update is called once per frame
    void Update()
    {
        stateMachines();
        conditioning();

        tempDown();

        PlayerTemp = Mathf.Clamp(PlayerTemp, 30, 37);
        PlayerHealth = Mathf.Clamp(PlayerHealth, 0, 100);
        PlayerHunger = Mathf.Clamp(PlayerHunger, 0, 100);
        PlayerThirst = Mathf.Clamp(PlayerThirst, 0, 100);

        if (startNeeds && control == false) { StartCoroutine(needsDecrease()); print("needs started");  control = true;  startNeeds = false; }
        if (startNeeds && control == true) { StopCoroutine(needsDecrease()); print("needs stopped");  control = false;  startNeeds = false; }

    }

    public void tempUp()
    {
        PlayerTemp += decreaseRate * Time.deltaTime;
    }

   public void tempDown()
    {
        PlayerTemp -= increaseRate * Time.deltaTime;
    }

    void conditioning()
    {
        if (PlayerHealth <= 50) { PlayerSpeed /= 2; }
        if (isNight) { tempDown(); }
        
        //Hunger
        if (PlayerHunger > 75) { hunger = playerHunger.satisfied; }
        if (PlayerHunger <= 75 && PlayerHunger > 50) { hunger = playerHunger.pekish; }
        if (PlayerHunger <= 50 && PlayerHunger > 25) { hunger = playerHunger.hungry; }
        if (PlayerHunger <= 25 ) { hunger = playerHunger.starving; }
        //Thirst

        if (PlayerThirst > 75) { thirst = playerThirst.satisfied; }
        if (PlayerThirst <= 75 && PlayerThirst> 50) { thirst = playerThirst.needADrink; }
        if (PlayerThirst <= 50 && PlayerThirst> 25) { thirst = playerThirst.thirsty; }
        if (PlayerThirst <= 25) { thirst = playerThirst.dehydrated; }

        //Injuries

        if (PlayerHealth <= 101 && PlayerHealth > 75 ) { injuries = playersInjuries.healthy; }
        if (PlayerHealth <= 75 && PlayerHealth > 50) { injuries = playersInjuries.normal; }
        if (PlayerHealth <= 50 && PlayerHealth > 25) { injuries = playersInjuries.injured; }
        if (PlayerHealth <=25) { injuries = playersInjuries.dying; }
        if (PlayerHealth <= 0) { injuries = playersInjuries.dead; }



        if (manager.temp == PlayerManager.tempreture.normal)
        {
            PlayerTemp = 36;
        }

        if (manager.temp == PlayerManager.tempreture.cold)
        {
            tempDown();
        }

        if (manager.temp == PlayerManager.tempreture.hot)
        {
            tempUp();
        }

        if (manager.temp == PlayerManager.tempreture.veryCold)
        {
            decreaseRate = .1f;
            tempDown();
        }

        if (manager.temp == PlayerManager.tempreture.veryHot)
        {
            increaseRate = .1f;
            tempUp();
        }


        if (manager.isNight) { }
    }

    void stateMachines()
    {

        switch (condition)
        {
            case playerCondition.normal:

                
                break;

            case playerCondition.freezing:

                break;

            case playerCondition.hot:

                break;

            case playerCondition.boiling:

                break;



        }


        switch (hunger)
        {

            case playerHunger.satisfied:

                break;

            case playerHunger.pekish:

                break;

            case playerHunger.hungry:
                
                break;

            case playerHunger.starving:

                break;



        }

        switch (thirst)
        {
            case playerThirst.satisfied:

                break;

            case playerThirst.needADrink:

                break;

            case playerThirst.thirsty:

                break;

            case playerThirst.dehydrated:

                break;



        }

        switch (injuries)
        {

            case playersInjuries.healthy:

                PlayerSpeed = Controls.playerMoveSpeed + 1;
                break;

            case playersInjuries.normal:

                PlayerSpeed = Controls.playerMoveSpeed;
                break;

            case playersInjuries.hurt:
                PlayerSpeed = Controls.playerMoveSpeed - 1 ;

                break;
            case playersInjuries.injured:
                PlayerSpeed = Controls.playerMoveSpeed -2;
                break;

            case playersInjuries.dying:

                PlayerSpeed = Controls.playerMoveSpeed - 3;
                break;

            case playersInjuries.dead:

                break;
        }

        switch (sheltered)
        {

            case insideOutSide.inside:

                break;

            case insideOutSide.outside:

                break; 



        }

        }

    IEnumerator needsDecrease()
    {
      

        while (true)
        {
            yield return new WaitForSecondsRealtime(time.secsPerMin * 2);
            hunger -= 10;
            thirst -= 10;
            
            if (hunger <= 0 || thirst <=0 ) { PlayerHealth -= GameConstants.starvationDecreaseRate; }
            if (hunger <= 0 && thirst <= 0) { PlayerHealth -= GameConstants.starvationDecreaseRate * 2; }





            yield return null; 
        }
    }
    }

