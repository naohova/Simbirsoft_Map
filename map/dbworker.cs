using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Drawing;

namespace map
{

    class dbworker
    {
        MySqlConnection Connection;
        MySqlConnectionStringBuilder Connect = new MySqlConnectionStringBuilder();

        public dbworker(string server, string user, string pass, string database) //Database Worker yes
        {
            Connect.Server = server;
            Connect.UserID = user;
            Connect.Password = pass;
            Connect.Port = 3306;
            Connect.Database = database;
            Connect.CharacterSet = "utf8";
            Connection = new MySqlConnection(Connect.ConnectionString);
        }

        public List<String> A(int id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT NAME, adress, information, id FROM  Attractions WHERE Coordinats_Place_id = " + id + ";";
            List<String> bd = new List<String>();
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bd.Add(reader.GetString(0));
                        bd.Add(reader.GetString(1));
                        bd.Add(reader.GetString(2));
                        bd.Add(reader.GetString(3));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Connection.Close(); }
            return bd;
        }

        public PointLatLng Cordinates(int id) //стартовая метка города 
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT Coordinates_City.X,Coordinates_City.Y FROM Coordinates_City WHERE City_id = " + id + ";";
            List<Coord> bd = new List<Coord>();
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Coord databd = new Coord
                        {
                            x = reader.GetDouble(0),
                            y = reader.GetDouble(1),
                        };
                        bd.Add(databd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Connection.Close(); }

            return new PointLatLng(bd[0].x, bd[0].y);
        }

        public List<Coord> Food(int id) //метки
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT X, Y, id, Name FROM a WHERE Place_id = "+ id.ToString() +" and City_id = "+ User.City_id + ";";
         // command.CommandText = "SELECT X, Y, id FROM a WHERE Place_id = "+ id.ToString() +" and City_id = "+ User.City_id + ";";
            List<Coord> bd = new List<Coord>();
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Coord databd = new Coord
                        {
                            x = reader.GetDouble(0),
                            y = reader.GetDouble(1),
                            id = reader.GetInt32(2),
                            name = reader.GetString(3),
                        };
                        bd.Add(databd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Connection.Close();
            }
            return bd;
        }

        public void visited(int Attraction_id, bool da)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = da ? "INSERT INTO `Liorkin`.`Visited` (`User_id`, `Attraction_id`) VALUES(?User.id, ?Attraction_id);" : "DELETE FROM `Liorkin`.`Visited` WHERE  `User_id`= ?User.id AND Attraction_id =?Attraction_id;";
            command.Parameters.Add("?User.id", MySqlDbType.Int32).Value = User.id;
            command.Parameters.Add("?Attraction_id", MySqlDbType.Int32).Value = Attraction_id;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool Visited_Check(int Attraction_id)
        {          
            MySqlCommand command = Connection.CreateCommand();          
            command.CommandText = "SELECT COUNT(*) FROM Visited WHERE User_id = "+ User.id + " AND Attraction_id = " + Attraction_id + ";";
            bool check = false;
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        check = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Connection.Close();
            }
            return check;
        }

        public void Add_User(string name, string surname, string login, string pass1, string pass2, int flag)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO `Users` (`Name`, `Surname`, `login`, `password`, `flag`) VALUES (?name, ?surname, ?login, ?password, ?flag);";
            if (pass1 == pass2)
            {
                command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                command.Parameters.Add("?surname", MySqlDbType.VarChar).Value = surname;
                command.Parameters.Add("?login", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("?password", MySqlDbType.VarChar).Value = pass1;
                command.Parameters.Add("?flag", MySqlDbType.VarChar).Value = flag;

                Connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!");

                    
                    int IDoo = USER_ID(login);
                    command.CommandText = "INSERT INTO `Users_Info` (`Users_id`, `secname`, `age`, `about_me`, `gender_id`,`city_id`) VALUES (@Users_id_value, @secname_value, @age_value, @about_me_value, @gender_value, @city_value)";
                    string empty = null;

                    command.Parameters.Add("@Users_id_value", MySqlDbType.VarChar).Value = IDoo;
                    command.Parameters.Add("@secname_value", MySqlDbType.VarChar).Value = empty;
                    command.Parameters.Add("@age_value", MySqlDbType.VarChar).Value = empty;
                    command.Parameters.Add("@about_me_value", MySqlDbType.VarChar).Value = empty;
                    command.Parameters.Add("@gender_value", MySqlDbType.Int32).Value = empty;
                    command.Parameters.Add("@city_value", MySqlDbType.Int32).Value = empty;

                    
                    if (command.ExecuteNonQuery() == 1)
                    {
                    }

                }
                else
                    MessageBox.Show("Error");

                Connection.Close();
            }
            else
                MessageBox.Show("Пароли не совпадают");
        }

        public int USER_ID(string login)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT id FROM Users WHERE login = '{login}'";
            object value = command.ExecuteScalar();
            int ID = Convert.ToInt32(value);
            return ID;
        }

        public bool login(string login, string pass)
        {
            if ((login == "Логин") || (pass == "Пароль"))
            {
                MessageBox.Show("Вы заполнили не все поля...", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else
            {
             
                DataTable table = new DataTable();
                Connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = Connection.CreateCommand();
                command.CommandText = "SELECT * FROM `Users` WHERE `login` = @LoginUserlock AND `password` = @PasswordUserlock";             
                command.Parameters.Add("@LoginUserlock", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@PasswordUserlock", MySqlDbType.VarChar).Value = pass;
                adapter.SelectCommand = command;
                adapter.Fill(table);

                Connection.Close();
                if (table.Rows.Count > 0)
                {
                    User.id = Convert.ToInt32(table.Rows[0].ItemArray[0]);
                    User.lvl = Convert.ToInt32(table.Rows[0].ItemArray[5]);
                    MessageBox.Show("Вы успешно авторизовались!");
                    return true;
                }
                else
                {
                    MessageBox.Show("Ошибочка... Проверьте логин и пароль!");
                    return false;
                }
                
            }
        }

        public void download(int Place_id, int City_id, string name, string adress, string info, double x, double y)
        {

            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = " INSERT INTO `Liorkin`.`Coordinats_Place` (`Place_id`, `X`, `Y`) VALUES(?Place_id, ?x, ?y);";
           
            command.Parameters.Add("?Place_id", MySqlDbType.Int32).Value = Place_id;
            command.Parameters.Add("?X", MySqlDbType.Double).Value = x;
            command.Parameters.Add("?Y", MySqlDbType.Double).Value = y;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            command = Connection.CreateCommand();
            /////////////////////////////////////////////////////////////
            command.CommandText = "INSERT INTO `Liorkin`.`Attractions` (`User_id`, `Place_id`, `City_id`, `Coordinats_Place_id`, `Name`, `Adress`, `Information`) VALUES (?User_id, ?Place_id, ?City_id, (SELECT MAX(id) FROM `Liorkin`.`Coordinats_Place`), ?Name, ?Adress, ?Information);";

            command.Parameters.Add("?User_id", MySqlDbType.Int32).Value = User.id;
            command.Parameters.Add("?Place_id", MySqlDbType.Int32).Value = Place_id;
            command.Parameters.Add("?City_id", MySqlDbType.Int32).Value = User.City_id;
            command.Parameters.Add("?Name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?Adress", MySqlDbType.VarChar).Value = adress;
            command.Parameters.Add("?Information", MySqlDbType.VarChar).Value = info;

            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }

        public Boolean check(string log1)
        {
            MySqlCommand command = Connection.CreateCommand();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            command.CommandText = "SELECT * FROM `Users` WHERE `login` = @LoginUserlock ";
            command.Parameters.Add("@LoginUserlock", MySqlDbType.VarChar).Value = log1;

            Connection.Open();

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Логин занят!");
                Connection.Close();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }
        }

        public string Usr_name(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT Name FROM Users WHERE id = {id}";
            object value = command.ExecuteScalar();
            string name = Convert.ToString(value);
            Connection.Close();
            return name;
        }

        public string Usr_surname(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT Surname FROM Users WHERE id = {id}";
            object value = command.ExecuteScalar();
            string surname = Convert.ToString(value);
            Connection.Close();
            return surname;
        }

        public string Usr_secname(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT secname FROM Users_Info WHERE Users_id = {id}";
            object value = command.ExecuteScalar();
            string secname = Convert.ToString(value);
            Connection.Close();
            return secname;
        }

        public string Usr_age(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT age FROM Users_Info WHERE Users_id = {id}";
            object value = command.ExecuteScalar();
            string age = Convert.ToString(value);
            Connection.Close();
            return age;
        }
        /////////////////////////////////////////////////////////////////
        public string Usr_city(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT City.Name FROM Users_Info JOIN City ON City_id = City.id WHERE Users_id = {id}";
            object value = command.ExecuteScalar();
            string name = Convert.ToString(value);
            Connection.Close();
            return name;
        }

        public int Usr_status(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT flag FROM Users WHERE id = {id}";
            object value = command.ExecuteScalar();
            int status = Convert.ToInt32(value);
            Connection.Close();
            return status;
        }
        /////////////////////////////////////////////////////////////////
        public string Usr_gender(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT Gender.Name FROM Users_Info JOIN Gender ON gender_id = Gender.id WHERE Users_id = {id}";
            object value = command.ExecuteScalar();
            string gender = Convert.ToString(value);
            Connection.Close();
            return gender;
        }

        public string Usr_about(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT about_me FROM Users_Info WHERE Users_id = {id}";
            object value = command.ExecuteScalar();
            string about = Convert.ToString(value);
            Connection.Close();
            return about;
        }

        public string Usr_password(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT password FROM Users WHERE id = {id}";
            object value = command.ExecuteScalar();
            string password = Convert.ToString(value);
            Connection.Close();
            return password;
        }

        public void Set_password(int id, string password)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users SET password = '{password}' WHERE  id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void Set_name(int id, string name)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users SET name = '{name}' WHERE  id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void Set_surname(int id, string surname)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users SET surname = '{surname}' WHERE  id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void Set_secname(int id, string secname)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users_Info SET secname = '{secname}' WHERE  Users_id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }
        /////////////////////////////////////////////////////////////////
        public void Set_city(int id, int city_id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users_Info SET city_id = '{city_id}' WHERE  Users_id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }
        /////////////////////////////////////////////////////////////////
        public void Set_gender(int id, int gender_id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users_Info SET gender_id = '{gender_id}' WHERE  Users_id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void Set_aboutme(int id, string aboutme)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users_Info SET about_me = '{aboutme}' WHERE  Users_id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public void Set_age(int id, string age)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users_Info SET age = '{age}' WHERE  Users_id = {id}";
            command.ExecuteNonQuery();
            Connection.Close();
        }


        public List<string> selectattr(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT Attractions.Name FROM Visited JOIN Attractions ON Attraction_id = Attractions.id WHERE Visited.User_id = {id}";
            MySqlDataReader reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader[0].ToString());
            Connection.Close();
            return names;
        }

        public List<string> selectattraddress(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT Attractions.Adress FROM Visited JOIN Attractions ON Attraction_id = Attractions.id WHERE Visited.User_id = {id}";
            MySqlDataReader reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader[0].ToString());
            Connection.Close();
            return names;
        }
        public List<string> selectcity(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT City.Name FROM Visited_City JOIN City ON City_id = City.id WHERE Visited_City.User_id = {id}";
            MySqlDataReader reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader[0].ToString());
            Connection.Close();
            return names;
        }

        public void Set_rate(int stat, int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"UPDATE Users SET flag = '{stat}' WHERE  id = {id}";
            User.lvl = stat;
            command.ExecuteNonQuery();
            Connection.Close();
        }

        public string RateInfo(int id)
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT name FROM Rate WHERE id = {id}";
            object value = command.ExecuteScalar();
            string RateName = Convert.ToString(value);
            Connection.Close();
            return RateName;
        }

        public int Get_MaxRate()
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT MAX(id) FROM Rate";
            object value = command.ExecuteScalar();
            int maxid = Convert.ToInt32(value);
            Connection.Close();
            return maxid;
        }

        public string PointInfo(int id) //вывод мето пользователя
        {
            Connection.Open();
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = $"SELECT NAME, Adress FROM Attractions WHERE User_id = {id}";
            object value = command.ExecuteScalar();
            string PointName = Convert.ToString(value);
            Connection.Close();
            return PointName;
        }

        public void Visited_City(int City_id, bool da)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = da ? "INSERT INTO `Liorkin`.`Visited_City` (`User_id`, `City_id`) VALUES (?User.id, ?City_id);" : "DELETE FROM `Liorkin`.`Visited_City` WHERE  `User_id`= ?User.id AND City_id =?City_id;";
            command.Parameters.Add("?User.id", MySqlDbType.Int32).Value = User.id;
            command.Parameters.Add("?City_id", MySqlDbType.Int32).Value = City_id;           
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool Visited_Check_City(int City_id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Visited_City WHERE User_id = " + User.id + " AND City_id = " + City_id + ";";
            bool check = false;
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        check = Convert.ToBoolean(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Connection.Close();
            }
            return check;
        }



        public List<int> Edit_Markers()
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT Attractions.Coordinats_Place_id FROM Attractions WHERE User_id = " + User.id + ";";
            List<int> bd = new List<int>();
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bd.Add(reader.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                Connection.Close();
            }
            return bd;
        }

        public List<string> Edit_Information(int id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT a.Name, a.Adress, a.Information FROM a WHERE id = " + id + ";";
            List<string> bd = new List<string>();
            try
            {
                Connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bd.Add(reader.GetString(0));
                        bd.Add(reader.GetString(1));
                        bd.Add(reader.GetString(2));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return bd;
        }



        public void Delete_Markers(int id)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "DELETE FROM `Liorkin`.`Coordinats_Place` WHERE  `id`=" + id + ";";
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Update_Markers(int id, string name, string adress, string info)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE `Liorkin`.`Attractions` SET `Name`= ?name, `Adress`= ?adress, `Information`= ?info  WHERE  `Coordinats_Place_id`= ?id;";
            //"DELETE FROM `Liorkin`.`Visited_City` WHERE  `User_id`= ?User.id AND City_id =?City_id;";

            command.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("?adress", MySqlDbType.VarChar).Value = adress;
            command.Parameters.Add("?info", MySqlDbType.VarChar).Value = info;
            command.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Connection.Close();
            }
        }



        //public List<Coord> Name_Markers(int id) //имена
        //{
        //    MySqlCommand command = Connection.CreateCommand();
        //    command.CommandText = "SELECT Attractions.Name FROM Attractions WHERE Place_id = " + id + ";";
        //    //SELECT Attractions.Name FROM Attractions WHERE Place_id = 1
        //    List<Coord> bd = new List<Coord>();
        //    try
        //    {
        //        Connection.Open();
        //        using (DbDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Coord databd = new Coord
        //                {
        //                    name = reader.GetString(0),
        //                };
        //                bd.Add(databd);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {

        //        Connection.Close();
        //    }
        //    return bd;
        //}


        // UPDATE `Liorkin`.`Attractions` SET `Name`='s', `Adress`='s', `Information`='s' WHERE  `id`=53;

        public DataTable getTableInfo(string query)//Combobox_worker
        {
            Connection.Open();
            MySqlCommand queryExecute = new MySqlCommand(query, Connection);
            DataTable ass = new DataTable();
            ass.Load(queryExecute.ExecuteReader());
            Connection.Close();
            return ass;
        }

    }
}