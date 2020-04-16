using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Text;
using UnityEngine;


public class Helperi
{
    static System.Random a = new System.Random(); //Deklarējam šeit jo funkcijā, ja katru reizi deklarēs, tad tie laiki būs pārāk tuvi un atgriezīs tās pašas vērtības
    static StreamWriter sw;
    public enum SkerslaRezims
    {
        GoLeftAndRight,
        GoLeft,
        GoRight,
        PunchLeftAndRight,
        PunchLeft,
        PunchRight,
        RandomMovement,
        Empty,
        None
    }
    public enum SkersluVeids
    {
        Abi,
        Kreisais,
        Labais
    }
    //TODO Pielietot loggerī
    public enum Tag
    {
        Kluda,
        Bridinajums,
        Info,
        Debug
    }

    //-2.3 līdz 2.3
    public static float GetRandomFloat(float Min, float Max)
    {

        double b = a.NextDouble(); //0.000 - 1.000
        float range = Max - Min; //4.6
        return (float)(Min + (range*b)); 
    }

    public static bool GetRandomBool()
    {
        double b = a.NextDouble();
        if (b > 0.5) return true;
        return false;
    }
    public static void Log (string msg)
    {
        Debug.Log("Temp Log Here: " + msg);
    }



}
