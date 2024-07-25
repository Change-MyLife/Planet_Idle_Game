using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Application.targetFrameRate = 30;
            init();
        }

        BunkerData _bunker = new BunkerData();
        public BunkerData Bunker
        {
            get { return _bunker; }
        }
        WaveData _wave = new WaveData();
        public WaveData Wave
        {
            get { return _wave; }
        }
        CreditType _credit = new CreditType();
        public CreditType Credit
        {
            get { return _credit; }
        }

        void init()
        {
            LoadBunker();
            LoadWave();
            LoadCredit();
        }

        // 벙커 데이터
        void LoadBunker()
        {
            _bunker = LoadJson<BunkerData>("Bunker");
            _bunker.SetValue();
            _bunker.SetCost();
        }

        // 적 웨이브 데이터
        void LoadWave()
        {
            _wave = LoadJson<WaveData>("EnemyWave");
            _wave.SetValue();
        }

        void LoadCredit()
        {
            _credit = LoadJson<CreditType>("Credit");
        }

        T LoadJson<T>(string json) where T : class
        {
            var v = Resources.Load<TextAsset>("Data/" + json);
            T data = JsonUtility.FromJson<T>(v.text);
         
            return data;
        }

        T SaveJson<T>(T data) where T : class
        {
            var json = JsonUtility.ToJson(data);
            /* TODO
             * FileSave Code
             */
            return data;
        }
    }
}