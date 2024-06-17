//using MySql.Data.MySqlClient;
using MySqlConnector;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MySQL : MonoBehaviour
{
    public static MySqlConnection SqlConn;
    
    public Text text;

    //static string ipAddress = "127.0.0.1";
    static string ipAddress = "192.168.1.63";
    //static string ipAddress = "14.63.67.166";
    static string db_id = "root";
    static string db_pw = "8524";
    static string db_name = "test";

    string strConn = string.Format("Server={0};Uid={1};Pwd={2};Database={3};charset=utf8mb4;", ipAddress, db_id, db_pw, db_name);

    private void Awake()
    {
        SqlConn = new MySqlConnection(strConn);
    }

    void Start()
    {
        //TEST();

        string query = "SELECT * FROM test.tb_test";
        OnSelectRequest(query);
    }
    private void Update()
    {
        //Debug.Log("MySQL 상태 : " + SqlConn.State);
        text.text = SqlConn.State.ToString();
    }

    void TEST()
    {
        try
        {
            SqlConn.Open();   //DB 연결
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public static bool OnInsertOrUpdateRequest(string str_query)
    {
        try
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = SqlConn;
            sqlCommand.CommandText = str_query;

            SqlConn.Open();

            sqlCommand.ExecuteNonQuery();

            SqlConn.Close();

            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }
    }

    public static void OnSelectRequest(string p_query)
    {
        try
        {
            SqlConn.Open();   //DB 연결

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = p_query;
            cmd.Connection = SqlConn;

            MySqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Debug.Log("ID: " + id + ", Name: " + name);
            }

            reader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            //SqlConn.Close();
        }
    }

    private void OnApplicationQuit()
    {
        SqlConn.Close();
    }
}