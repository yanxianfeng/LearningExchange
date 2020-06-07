using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMySql : MonoBehaviour
{
    /// <summary>
    /// 工具类对象
    /// </summary>
    private SqlAccess sqlAce;

    MySqlConnection con;
    void Start()
    {
        CreateStudentTable();
        //InsertInfomation();
        QueryInfomation1();
        //QueryInfomation2();
        //UpdateInfomation();
        //DeleteInfomation();
        //DeleteTable();
    }
    //1、建立一个“学生”表Student，它由学号Sid、姓名Sname、性别Ssex、年龄Sage、 ,类型自己定义
    /// <summary>
    /// 创建学生表
    /// </summary>
    public void CreateStudentTable()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = ("create table student(Sid varchar(12),Sname varchar(8),Sage int,Sdept varchar(8)) CHARSET=utf8");
        sqlAce.CreateTable(sql, con);
        sqlAce.CloseMySQL();
    }
    //2、添加自己所在组的所有成员到表中。
    /// <summary>
    /// 学生表插值
    /// </summary>
    public void InsertInfomation()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql1 = ("insert into Student(Sid,Sname,Sage,Sdept) values('1001','学生A','20','土木工程')");
        string sql2 = ("insert into Student(Sid,Sname,Sage,Sdept) values('1002','学生B','15','计算机与科学')");
        string sql3 = ("insert into Student(Sid,Sname,Sage,Sdept) values('1003','学生C','16','电子商务')");
        string sql4 = ("insert into Student(Sid,Sname,Sage,Sdept) values('1004','学生D','25','电子竞技')");
        string sql5 = ("insert into Student(Sid,Sname,Sage,Sdept) values('1005','学生E','23','网络工程')");
        string[] str = new string[5] { sql1, sql2, sql3, sql4, sql5 };
        for (int i = 0; i < str.Length; i++)
        {
            sqlAce.InsertInfo(str[i], con);
        }
        sqlAce.CloseMySQL();
    }
    //3、查询年龄在18至24岁之间的学生的姓名和专业 
    /// <summary>
    /// 查询学生信息01
    /// </summary>
    public void QueryInfomation1()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = ("select Sname,Sdept from Student where Sage<'24' and Sage>'18' ");
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        for (int i = 0; i < dic.Count; i++)
        {
            Debug.Log(string.Format("学生姓名：{0} 学生专业：{1}", dic[i][0], dic[i][1]));
        }
        sqlAce.CloseMySQL();
    }
    //4、查询所有学生的学号与姓名
    /// <summary>
    /// 查询学生信息02
    /// </summary>
    public void QueryInfomation2()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = ("select Sid,Sname from Student");
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        for (int i = 0; i < dic.Count; i++)
        {
            Debug.Log(string.Format("学生学号：{0} 学生姓名：{1}", dic[i][0], dic[i][1]));
        }
        sqlAce.CloseMySQL();
    }
    //5、将自己的年龄改为18岁
    /// <summary>
    /// 更新学生信息
    /// </summary>
    public void UpdateInfomation()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = ("update Student set Sage='18' where Sname='学生A'");
        sqlAce.UpdateInfo(sql, con);
        sqlAce.CloseMySQL();
    }
    //6、删除学号为单数的学生记录
    /// <summary>
    /// 删除表信息
    /// </summary>
    public void DeleteInfomation()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql2 = ("select Sid from Student");
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql2, con);
        sqlAce.CloseMySQL();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        for (int i = 0; i < dic.Count; i++)
        {
            if (Convert.ToInt32(dic[i][0]) % 2 == 1)
            {
                string sql1 = string.Format("delete from Student where Sid='{0}'", dic[i][0]);
                sqlAce.DeleteInfo(sql1, con);
            }
        }
        sqlAce.CloseMySQL();
    }
    /// <summary>
    /// 删除表
    /// </summary>
    public void DeleteTable()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = ("drop table Student");
        sqlAce.DeleteInfo(sql, con);
    }
}
