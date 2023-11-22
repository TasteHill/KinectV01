using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows.Forms;
using KinectV01;

namespace FirstPage
{
    public class DB
    {
        private MySqlConnection connection;
        private string strconn = "Server=211.112.84.57;port=3307;Database=ke;Uid=root;Pwd=rootpw";


        public List<Idol> getIdolsFromDB()                                  //아이돌 리스트 생성
        {
            MySqlConnection connection = new MySqlConnection(strconn);
            string query = "select * from idol natural join score natural join user;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            List<Idol> resultIdols = new List<Idol>();


            try
            {
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String idolName = reader["idol_name"].ToString();
                        String userName = reader["user_name"].ToString();
                        int idolnum = int.Parse(reader["idol_no"].ToString());
                        int userScore = int.Parse(reader["score"].ToString());
                        bool isIdolExist = false;
                        Idol existIdol = null;

                        foreach (var idol in resultIdols)
                        {
                            if (idol.IName.Equals(idolName))
                            {
                                isIdolExist = true;
                                existIdol = idol;
                                break;
                            }
                        }

                        if (isIdolExist)
                        {
                            existIdol.Inumber = idolnum;
                            existIdol.IdolUsers.Add(userName, userScore);
                            existIdol.IScore += userScore;

                        }
                        else
                        {
                            Idol idol = new Idol(idolName);
                            idol.Inumber = idolnum;
                            idol.IdolUsers.Add(userName, userScore);
                            idol.IScore += userScore;
                            resultIdols.Add(idol);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultIdols;

        }


        public List<User> getUserFromDB()                              //유저 리스트 생성
        {
            MySqlConnection connection = new MySqlConnection(strconn);
            string query = "select * from idol natural join score natural join user;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            List<User> resultUsers = new List<User>();


            try
            {
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String idolName = reader["idol_name"].ToString();
                        String userName = reader["user_name"].ToString();
                        int usernum = int.Parse(reader["user_no"].ToString());
                        int userScore = int.Parse(reader["score"].ToString());
                        bool isUserExist = false;
                        User existUser = null;

                        foreach (var user in resultUsers)
                        {
                            if (user.UName.Equals(userName))
                            {
                                isUserExist = true;
                                existUser = user;
                                break;
                            }
                        }

                        if (isUserExist)
                        {
                            existUser.Unumber = usernum;
                            existUser.UserIdols.Add(idolName, userScore);
                            existUser.UScore += userScore;

                        }
                        else
                        {
                            User User = new User();
                            User.Unumber = usernum;
                            User.UName = userName;
                            User.UserIdols.Add(idolName, userScore);
                            User.UScore += userScore;
                            resultUsers.Add(User);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultUsers;

        }

        //EnterName에서 호출 유저이름, 아이돌이름, 아이돌리스트, 유저리스트를 받고
        //DB에 상황에 맞게 데이터 저장 후
        //'유저명 아이돌명' 형태의 스트링 반환
        public String SetUserAndIdol(String userName, String IdolName, List<Idol> idolList, List<User> userList)
        {

            bool isUserExist = false;
            bool isIdolExist = false;
            foreach (var user in userList)                     //유저명이 존재하면 true
            {
                if (user.UName.Equals(userName))
                {
                    isUserExist = true;
                    break;
                }
            }

            foreach (var idol in idolList)                    //아이돌명이 존재하면 true
            {
                if (idol.IName.Equals(IdolName))
                {
                    isIdolExist = true;
                    break;
                }
            }

            if (isUserExist && isIdolExist)                     //둘다 있으면
            {
                MessageBox.Show("닉네임: " + userName + "님 환영합니다!");
                return userName + " " + IdolName;
            }
            else if (isUserExist && !isIdolExist)                               //아이돌명이 없으면 
            {
                InsertNewIdolName(IdolName);
                MessageBox.Show("닉네임: " + userName + "님 환영합니다!");
                return userName + " " + IdolName;
            }
            else if(!isUserExist && isIdolExist)
            {
                InsertNewNickName(userName);
                return userName + " " + IdolName;
            }
            else if(!isUserExist && !isIdolExist)
            {
                InsertNewNickName(userName);
                InsertNewIdolName(IdolName);
                return userName + " " + IdolName;
            }
        }

        private void InsertNewNickName(string newNickName)
        {
            MySqlConnection connection = new MySqlConnection(strconn);
            connection.Open();
            string sql = "Select user_name from user";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@newNickName", newNickName);
            int count = 1;

            try
            {
                using (MySqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        if (read.HasRows)
                        {
                            count++;
                        }
                    }
                    read.Close();
                }
            }
            catch 
            {
            }

            try
            {
                
                sql = "INSERT INTO user(user_no, user_name) VALUES ('"+count+"', '"+newNickName+"')";
                cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader readName = cmd.ExecuteReader())
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("사용자 정보 등록 완료");
                    readName.Close();
                }
            }
            catch 
            {
            }
        }

        private void InsertNewIdolName(string IdolName)
        {
            MySqlConnection connection = new MySqlConnection(strconn);
            connection.Open();
            string sql = "Select idol_name from idol";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@newNickName", IdolName);
            int count = 0;

            try
            {
                using (MySqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        if (read.HasRows)
                        {
                            count++;
                        }
                    }
                    read.Close();
                }
            }
            catch 
            {
            }

            try
            {
                count++;
                cmd = new MySqlCommand(sql, connection);
                sql = "INSERT INTO idol(idol_no, idol_name) VALUES ('" + count + "', '"+ IdolName+"')";
                using (MySqlDataReader readName = cmd.ExecuteReader())
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("아이돌 정보 등록 완료");
                    readName.Close();
                }
            }
            catch 
            {
            }

        }

        //점수추가
        //유저 번호와 아이돌 번호와 점수를 받아와 
        // 다 score 테이블에 저장
        public void InsertNewScore(int user_no, int idol_no, string score)
        {
            MySqlConnection connection = new MySqlConnection(strconn);
            connection.Open();
            try
            {
                string sql = "INSERT INTO score(user_no, idol_no, score) VALUES (user_no, idol_no, score)";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader readName = cmd.ExecuteReader())
                {
                    cmd.ExecuteNonQuery();
                    readName.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}


