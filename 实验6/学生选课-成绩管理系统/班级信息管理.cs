﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 学生选课_成绩管理系统
{
    public partial class 班级信息管理 : Form
    {
        public 班级信息管理()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            SqlConnection sqlConnection = new SqlConnection(@"server=.;database=JWGLDB;integrated security=sspi");
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            try
            {
                sqlConnection.Open();

                string sql2 = "select name from major where department=(select no from department where name=(@department))";//专业
                sqlCommand.CommandText = sql2;
                sqlCommand.Parameters.Add("@department", SqlDbType.NVarChar, 255).Value = comboBox1.Text;
                SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    comboBox2.Items.Add(sqlDataReader2[0].ToString());
                }
                comboBox2.Text = (string)comboBox2.Items[0];
                sqlDataReader2.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void 班级信息管理_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"server=.;database=JWGLDB;integrated security=sspi");
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            try
            {
                sqlConnection.Open();
                //MessageBox.Show(sqlConnection.State.ToString());
                string sql = "select name from department";//学院
                sqlCommand.CommandText = sql;
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    comboBox1.Items.Add(sqlDataReader1[0].ToString());
                }
                comboBox1.Text = (string)comboBox1.Items[0];
                sqlDataReader1.Close();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"server=.;database=JWGLDB;integrated security=sspi");
            string sql = "select class.no as '编号',class.name as '名称',monitor as '班长',class.Counsellor as '辅导员',size as '人数',major.name as '专业',department.name as '学院' from class,major,department where class.major=major.no and class.department=department.name and major=(select no from major where name like '" + comboBox2.Text + "%')";
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sql;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet dataSet = new DataSet();
            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet);//将原表名作为默认表名
                dataGridView1.DataSource = dataSet.Tables[0];
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
