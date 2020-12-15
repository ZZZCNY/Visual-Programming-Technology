﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 学生选课_成绩管理系统
{
    public partial class 学生信息管理 : Form
    {
        public 学生信息管理()
        {
            InitializeComponent();
        }

        private void 学生信息管理_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"server=.;database=JWGLDB;integrated security=sspi");
            string sql1 = "select name from department";//学院
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            try
            {
                sqlConnection.Open();
                //MessageBox.Show(sqlConnection.State.ToString());
                sqlCommand.CommandText = sql1;
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    comboBox1.Items.Add(sqlDataReader1[0].ToString());
                }
                comboBox1.Text = (string)comboBox1.Items[0];
                sqlDataReader1.Close();

                string sql2 = "select name from major where department=(@department)";//专业
                sqlCommand.CommandText = sql2;
                sqlCommand.Parameters.Add("@department", SqlDbType.NVarChar, 255).Value = comboBox1.Text;
                SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    comboBox2.Items.Add(sqlDataReader2[0].ToString());
                }
                comboBox2.Text = (string)comboBox2.Items[0];
                sqlDataReader2.Close();

                string sql3 = "select name from class";//班级
                sqlCommand.CommandText = sql3;
                SqlDataReader sqlDataReader3 = sqlCommand.ExecuteReader();
                while (sqlDataReader3.Read())
                {
                    comboBox3.Items.Add(sqlDataReader3[0].ToString());
                }
                comboBox3.Text = (string)comboBox3.Items[0];
                sqlDataReader3.Close();


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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            SqlConnection sqlConnection = new SqlConnection(@"server=.;database=JWGLDB;integrated security=sspi");
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            try
            {
                sqlConnection.Open();
                string sql2 = "select name from major where department=(@department)";//专业
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
        }
    }
}
