using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class SqlAccess
{
    public static MySqlConnection mySqlConnection;
    //数据库名称
    public static string database = "yanxianfeng";
    //数据库IP
    private static string host = "localhost";
    //用户名
    private static string username = "root";
    //用户密码
    private static string password = "li0505";

    private static string Charset = "utf8";

    //public static string sql = string.Format("database={0};server={1};user={2};password={3};port={4}",
    //database, host, username, password, "3306");
    public static string sql = string.Format("database={0};server={1};user={2};password={3};Charset={4}",
    database, host, username, password, "utf8");

    public static MySqlConnection con;
    private MySqlCommand com;

    #region BaseOperation
    /// <summary>
    /// 构造方法开启数据库
    /// </summary>
    public SqlAccess()
    {
        con = new MySqlConnection(sql);
        OpenMySQL(con);
    }
    /// <summary>
    /// 启动数据库
    /// </summary>    
    /// <param name="con"></param>
    public void OpenMySQL(MySqlConnection con)
    {
        con.Open();
        Debug.Log("数据库已连接");
    }
    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public void CreateTable(string _sql, MySqlConnection con)
    {
        string sqlstring = ("SELECT * FROM information_schema.TABLES where table_name='student' and TABLE_SCHEMA='yanxianfeng'");
        //MySqlCommand com = new MySqlCommand(sql, con);
        MySqlDataAdapter adp = new MySqlDataAdapter(sqlstring,con);
        DataSet ds = new DataSet();

        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count>0)
        {
            Debug.Log("数据库表已存在");
        }
        else
        {
            com = new MySqlCommand(_sql, con);
            int res = com.ExecuteNonQuery();
        Debug.Log(res);
            Debug.Log("数据库表创建");
        }
        
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public void InsertInfo(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
        Debug.Log(res);
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public void DeleteInfo(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
        Debug.Log(res);
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public void UpdateInfo(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
        Debug.Log(res);
    }
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public Dictionary<int, List<string>> QueryInfo(string sql, MySqlConnection con)
    {
        int indexDic = 0;
        int indexList = 0;
        Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
        MySqlCommand com = new MySqlCommand(sql, con);
        MySqlDataReader reader = com.ExecuteReader();
        Debug.Log(reader);
        while (true)
        {
            if (reader.Read())
            {
                List<string> list = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader[indexList].ToString());
                    indexList++;
                }
                dic.Add(indexDic, list);
                indexDic++;
                indexList = 0;
            }
            else
            {
                break;
            }
        }
        return dic;
    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseMySQL()
    {
        (new MySqlConnection(sql)).Close();
        Debug.Log("关闭数据库");
    }
    #endregion

}
