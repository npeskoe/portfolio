using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data
{
    class DvdRepositoryADO : IDvdRepository
    {
        public class SelectQuery
        {
            public static List<DVD> GetAllDVDs()
            {
                List<DVD> dvds = new List<DVD>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "GetAllDvds";

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DVD currentRow = new DVD();

                            currentRow.Title = dr["Title"].ToString();
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];
                            currentRow.Director = dr["Director"].ToString();
                            currentRow.Rating = dr["Rating"].ToString();
                            currentRow.Notes = dr["Notes"].ToString();

                            currentRow.DvdID = (int)dr["DvdID"];
                            dvds.Add(currentRow);
                        }
                    }
                }

                return dvds;
            }
        }

        public class SelectWithParameters
        {
            public static List<DVD> GetDVD(int dvdID)
            {
                List<DVD> dvds = new List<DVD>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "GetDvd";

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DVD currentRow = new DVD();

                            currentRow.Title = dr["Title"].ToString();
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];
                            currentRow.Director = dr["Director"].ToString();
                            currentRow.Rating = dr["Rating"].ToString();
                            currentRow.Notes = dr["Notes"].ToString();

                            currentRow.DvdID = (int)dr["DvdID"];
                            dvds.Add(currentRow);

                        }
                    }

                    return dvds;
                }
            }
        }
        public class InsertQuery
        {
            public static void InsertDVD(int dvdID, string title, int releaseYear, string director, string rating, string notes)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "AddDvd";

                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);
                    cmd.Parameters.AddWithValue("@Director", director);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Notes", notes);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public class UpdateQuery
        {
            public static void UpdateDVD(int dvdID, string title, int releaseYear, string director, string rating, string notes)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "UpdateDvd";

                    cmd.Parameters.AddWithValue("@DvdID", dvdID);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);
                    cmd.Parameters.AddWithValue("@Director", director);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Notes", notes);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public class DeleteQuery
        {
            public static void DeleteDVD(int dvdID)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandText = "DeleteDvd";

                    cmd.Parameters.AddWithValue("@DvdID", dvdID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private List<DVD> _Dvds = new List<DVD>();

        public List<DVD> GetAll()
        {
            _Dvds = SelectQuery.GetAllDVDs();

            return _Dvds;
        }

        public DVD Get(int dvdID)
        {
            _Dvds = SelectWithParameters.GetDVD(dvdID);

            return _Dvds.FirstOrDefault(d => d.DvdID == dvdID);
        }

        public void Add(DVD dvd)
        {
            InsertQuery.InsertDVD(dvd.DvdID, dvd.Title, dvd.ReleaseYear, dvd.Director, dvd.Rating, dvd.Notes);
            dvd.DvdID = _Dvds.Max(d => d.DvdID) + 1;
            _Dvds.Add(dvd);
        }

        public void Edit(DVD dvd)
        {
            UpdateQuery.UpdateDVD(dvd.DvdID, dvd.Title, dvd.ReleaseYear, dvd.Director, dvd.Rating, dvd.Notes);

            var found = _Dvds.FirstOrDefault(d => d.DvdID == dvd.DvdID);

            if (found != null)
                found = dvd;
        }

        public void Delete(int dvdID)
        {
            DeleteQuery.DeleteDVD(dvdID);

            _Dvds.RemoveAll(d => d.DvdID == dvdID);
        }

    }
}

