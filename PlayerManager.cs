using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum tempreture { normal,hot,veryHot, cold, veryCold }
    public tempreture temp; 
    public bool isNight;


    private void Update()
    {
        states();
    }

    void states ()
    {
        switch (temp)
        {
            case tempreture.normal:

                break;

            case tempreture.cold:

                break;


            case tempreture.hot:

                break;

            case tempreture.veryCold:

                break;


            case tempreture.veryHot:

                break;

        }

    }
}
