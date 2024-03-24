using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class JSONWriter : MonoBehaviour
{
    [System.Serializable]
    public class infos
    {
        public int tiros;
        public int curas;
        public int iniM;
        public int danoI;
        public float tempo;
    }

    public infos infoJogo = new infos();
    
    public void outputJSON()
    {
        infoJogo.tempo = Time.time;
        infoJogo.tiros = PlayerMovement.tiros;
        infoJogo.curas = PlayerMovement.collectAmount;
        infoJogo.iniM = PlayerMovement.enemyKilled;
        infoJogo.iniM = PlayerMovement.danoI;

        string strOutput = JsonUtility.ToJson(infoJogo); //criando a string que sera salva
        File.WriteAllText(Application.dataPath + "/JSONs/infos.txt", strOutput); //salvando o arquivo na pasta do jogo
    }
}
