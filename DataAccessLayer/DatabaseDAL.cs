using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WpfPicDB.Models;

namespace WpfPicDB.DataAccessLayer
{
    public class DatabaseDAL
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public DatabaseDAL(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["Database"].ToString();
            }
            else
            {
                _connectionString = connectionString;
            }
            _connection = new SqlConnection(_connectionString);
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        private void CmdExe(SqlCommand command)
        {
            OpenConnection();
            using (command)
            {
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }

        private void AddEXIF(EXIFModel exif, int id)
        {
            SqlCommand cmd = new SqlCommand("Select Id from EXIF WHERE PictureId = @id; SELECT SCOPE_IDENTITY();", _connection);
            cmd.Parameters.AddWithValue("@id", id);
            OpenConnection();
            int? exists = (int?)cmd.ExecuteScalar();
            if (exists.HasValue)
            {
                cmd = new SqlCommand("UPDATE EXIF SET Cameramodel = @cm, Author = @auth, Datatype = @datatype, Date = @date WHERE PictureId = @id", _connection);
            }
            else
            {
                cmd = new SqlCommand("INSERT INTO EXIF (PictureId, Cameramodel, Author, Datatype, Date) VALUES (@id, @cm, @auth, @datatype, @date);", _connection);
            }
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cm", exif.Cameramodel);
            cmd.Parameters.AddWithValue("@auth", exif.Author);
            cmd.Parameters.AddWithValue("@datatype", exif.Datatype);
            cmd.Parameters.AddWithValue("@date", exif.Date);

            CmdExe(cmd);
        }

        private void AddIPTC(IPTCModel iptc, int id)
        {
            SqlCommand cmd = new SqlCommand("Select Id from IPTC WHERE PictureId = @id; SELECT SCOPE_IDENTITY();", _connection);
            cmd.Parameters.AddWithValue("@id", id);
            OpenConnection();
            int? exists = (int?)cmd.ExecuteScalar();
            if (exists.HasValue)
            {
                cmd = new SqlCommand("UPDATE IPTC SET Title = @title, Description = @desc, Keywords = @keywords WHERE PictureId = @id", _connection);
            }
            else
            {
                cmd = new SqlCommand("INSERT INTO IPTC (PictureId, Title, Description, Keywords) VALUES (@id, @title, @desc, @keywords)", _connection);
            }
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@title", iptc.Title);
            cmd.Parameters.AddWithValue("@desc", iptc.Description);
            cmd.Parameters.AddWithValue("@keywords", iptc.Keywords);
            CmdExe(cmd);
        }

        public void AddPhotographer(PhotographerModel photographer)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Photographer (Firstname, Lastname, Birthdate) VALUES (@firstname, @lastname, @birthdate)", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@firstname", photographer.FirstName);
            cmd.Parameters.AddWithValue("@lastname", photographer.LastName);
            cmd.Parameters.AddWithValue("@birthdate", photographer.Birthdate);
            CmdExe(cmd);
        }

        public void UpdatePhotographer(PhotographerModel photographer)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Photographer SET Firstname = @firstname, Lastname = @lastname, Birthdate = @birthdate WHERE Id = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", photographer.Id);
            cmd.Parameters.AddWithValue("@firstname", photographer.FirstName);
            cmd.Parameters.AddWithValue("@lastname", photographer.LastName);
            cmd.Parameters.AddWithValue("@birthdate", photographer.Birthdate);
            CmdExe(cmd);
        }

        public void DeletePhotographer(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Picture WHERE PhotographerId = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            CmdExe(cmd);
            cmd = new SqlCommand("DELETE FROM Photographer WHERE Id = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            CmdExe(cmd);
        }


        public void AddPicture(PictureModel picture, int? id)
        {
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Picture WHERE Picture = @buffer AND PhotographerID = @id; SELECT SCOPE_IDENTITY();", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@buffer", picture.Bytes);
            if (id == null)
            {
                cmd.Parameters.AddWithValue("@id", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@id", id);
            }
            OpenConnection();
            int? autoId = (int?)cmd.ExecuteScalar();
            if (!autoId.HasValue)
            {
                cmd = new SqlCommand("INSERT INTO Picture (Picture, PhotographerId) VALUES (@buffer, @id); SELECT SCOPE_IDENTITY();", _connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@buffer", picture.Bytes.ToArray());
                if (id == null)
                {
                    cmd.Parameters.AddWithValue("@id", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@id", id);
                }
                autoId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            AddEXIF(picture.EXIF, (int)autoId);
            AddIPTC(picture.IPTC, (int)autoId);
            CloseConnection();
        }

        public void DeletePicture(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Picture WHERE Id = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            CmdExe(cmd);
        }

        public List<PictureModel> ReadAllPictures()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Picture", _connection);
            cmd.CommandType = CommandType.Text;
            return ReadPictures(cmd);
        }

        private List<PictureModel> ReadPictures(SqlCommand cmd)
        {
            List<PictureModel> retlist = new List<PictureModel>();
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["PhotographerId"] == DBNull.Value)
                    {
                        retlist.Add(new PictureModel((int)reader["Id"], (byte[])reader["Picture"], ReadEXIF((int)reader["Id"]), ReadIPTC((int)reader["Id"]), new PhotographerModel()));
                    }
                    else
                    {
                        retlist.Add(new PictureModel((int)reader["Id"], (byte[])reader["Picture"], ReadEXIF((int)reader["Id"]), ReadIPTC((int)reader["Id"]), ReadPhotographer((int)reader["PhotographerId"])));
                    }
                    retlist.Last().Index = retlist.IndexOf(retlist.Last());
                }
            }
            CloseConnection();
            return retlist;
        }
        private EXIFModel ReadEXIF(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM EXIF WHERE PictureId = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new EXIFModel((DateTime)reader["Date"], (string)reader["Cameramodel"], (string)reader["Author"], (string)reader["Datatype"]);
                }
            }
            return new EXIFModel(DateTime.Today);
        }

        private IPTCModel ReadIPTC(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM IPTC WHERE PictureId = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new IPTCModel((string)reader["Title"], (string)reader["Description"], (string)reader["Keywords"]);
                }
            }
            return new IPTCModel();
        }

        private PhotographerModel ReadPhotographer(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Photographer WHERE id= @id", _connection);
            cmd.Parameters.AddWithValue("@id", id);
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    PhotographerModel photographer = new PhotographerModel((int)reader["Id"], (string)reader["FirstName"], (string)reader["LastName"], (DateTime)reader["Birthdate"]);
                    return photographer;
                }
            }
            return new PhotographerModel();
        }

        public List<PictureModel> SearchPictures(string search)
        {
            SqlCommand cmd = new SqlCommand("SELECT Id From Photographer WHERE FirstName LIKE @search OR LastName LIKE @search", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", search);
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ReadPhotographerPictures((int)reader["Id"]);
                }
            }
            CloseConnection();
            return new List<PictureModel>();
        }

        private List<PictureModel> ReadPhotographerPictures(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Picture WHERE PhotographerId = @id", _connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            return ReadPictures(cmd);
        }

        public List<PhotographerModel> ReadPhotographers()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Photographer", _connection);
            List<PhotographerModel> retlist = new List<PhotographerModel>();
            OpenConnection();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    retlist.Add(new PhotographerModel((int)reader["Id"], (string)reader["FirstName"], (string)reader["LastName"], (DateTime)reader["Birthdate"]));
                }
            }
            CloseConnection();
            return retlist;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}
