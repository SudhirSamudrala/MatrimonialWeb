using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatrimonialWebsite.Controllers
{
    public class HomeController : Controller
    {
        string myConnectionString = "server=localhost;uid=root;" + "database=matrimonial";
        [HttpGet]
        public ActionResult Login()
        {
            
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.loginDetail obj)
        {
            string newProdID = "";
            DataSet ds = new DataSet("cust");
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select Count(*) from userstable o where o.EmailId=@Email and Password=@password", conn);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    try
                    {
                        conn.Open();
                        newProdID = Convert.ToString(cmd.ExecuteScalar());
                        if (Convert.ToInt32(newProdID) == 0)
                        {
                            return View("Login");
                        }
                        MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter
                        ("SELECT *,(Select id from userstable Where EmailId = '" + (obj.Email).ToString() + "') as MID FROM userstable o Where o.EmailId not like('" + (obj.Email).ToString() + "')", conn);
                        da.Fill(ds, "Customers");
                        //("SELECT * FROM userstable o where o.EmailId="+"'"+ obj.Email + "'", conn);
                        //string query = "SELECT * FROM Customers";
                        //using (MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand(query))
                        //{
                        //    using (MySql.Data.MySqlClient.MySqlDataAdapter sda = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                        //    {
                        //        sda.Fill(ds);
                        //    }
                        //}

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                newProdID = "";
            }       
            Session["uid"] = obj.Email;
            Session["pwd"] = obj.password;
            Session["id"] = ds.Tables[0].Rows[0][8].ToString();
            Models.RegistrationDetails Re = new Models.RegistrationDetails();
            Re.DT = ds.Tables[0];
            return View("Index", Re);
        }

        [HttpGet]
        public ActionResult UserRegistration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserRegistration(Models.RegistrationDetails obj)
        {
            int count = 0;
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into userstable(FullName,EmailId,Age,Mobile,Password,RegDate) values(@FullName,@EmailId,@Age,@Mobile,@Password,@RegDate)", conn);
                                                                                                                                
                    cmd.Parameters.AddWithValue("@FullName", obj.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@Age", obj.Age);
                    cmd.Parameters.AddWithValue("@Mobile", obj.mobile);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@RegDate", DateTime.Now);
                    try
                    {
                        conn.Open();
                        count = cmd.ExecuteNonQuery();
                        if (count == 0)
                        {
                            return View("UserRegistration");
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("UserRegistration");
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                count = 0;
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["uid"] = null;
            Session["pwd"] = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult IntrestData(Models.RegistrationDetails obj)
        {
            int id = 0;
            int count = 0;
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select id from userstable o where o.EmailId=@Email and o.Password=@password", conn);
                    cmd.Parameters.AddWithValue("@Email", (Session["uid"]).ToString());
                    cmd.Parameters.AddWithValue("@password", (Session["pwd"]).ToString());
                    try
                    {
                        conn.Open();
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        return View("Login");
                    }

                    MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand("select Count(*) from applyformeting o where o.id=@id and o.AppliedUserId=@AppliedUserId", conn);
                    cmd1.Parameters.AddWithValue("@id", id);
                    cmd1.Parameters.AddWithValue("@AppliedUserId", obj.RegistrId);
                    try
                    {
                        count = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (count > 0)
                        {
                            return RedirectToAction("IntrestDetail");
                        }
                        else
                        {
                            MySql.Data.MySqlClient.MySqlCommand cmd2 = new MySql.Data.MySqlClient.MySqlCommand("insert into applyformeting(id,AppliedUserId) values(@id,@AppliedUserId)", conn);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.Parameters.AddWithValue("@AppliedUserId", obj.RegistrId);
                            //cmd2.Parameters.AddWithValue("@MeetingDate", DateTime.Now);
                            //cmd2.Parameters.AddWithValue("@MeetingTime", '0');
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("IntrestDetail");
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                return RedirectToAction("IntrestDetail");
            }
            return RedirectToAction("IntrestDetail");
        }

        public ActionResult IntrestDetail()
        {
            DataSet ds = new DataSet("cust");
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {                 
                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM userstable o Where o.EmailId not like ('"+ (Session["uid"]).ToString() + "')", conn);
                    da.Fill(ds, "Customers");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
            }
            Models.RegistrationDetails Re = new Models.RegistrationDetails();
            Re.DT = ds.Tables[0];
            return View("Index", Re);
        }

        [HttpGet]
        public ActionResult ApplyForMeeting(Models.RegistrationDetails obj)
        {
            DataSet ds = new DataSet("cust");
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT a.Uid,a.id,a.AppliedUserId,u.FullName,a.MeetingDate,a.MeetingTime FROM applyformeting a,userstable u WHERE a.AppliedUserId=u.id and a.id=" + "'" + obj.RegistrId + "'  order by u.id", conn);
                    da.Fill(ds, "Customers");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
            }
            Models.RegistrationDetails Re = new Models.RegistrationDetails();
            Re.DT = ds.Tables[0];
            return View(Re);
        }

        [HttpGet]
        public ActionResult ScheduleMeeting(Models.RegistrationDetails obj)
        {
            DataSet ds = new DataSet("cust");
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT a.Uid,a.id,a.AppliedUserId,u.FullName,a.MeetingDate,a.MeetingTime FROM applyformeting a,userstable u WHERE a.AppliedUserId=u.id and a.id=" + "'" + obj.id + "' and a.AppliedUserId=" + "'" + obj.AppliedUserId + "'  order by u.id", conn);
                    da.Fill(ds, "Customers");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
            }
            Models.RegistrationDetails Re = new Models.RegistrationDetails();
            Re.DT = ds.Tables[0];
            Session["Aid"] = ds.Tables[0].Rows[0][0].ToString();
            return View(Re);

        }
        [HttpPost]
        public ActionResult UpdateMeeting(Models.RegistrationDetails obj)
        {
            //-----
            int count = 0;
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("update applyformeting set MeetingDate = @MeetingDate , MeetingTime = @MeetingTime where Uid = @Uid", conn);
                    cmd.Parameters.AddWithValue("@MeetingTime", (obj.MeetingTime + "" + obj.AMorPM).ToString());
                    cmd.Parameters.AddWithValue("@MeetingDate", (Convert.ToDateTime(obj.MeetingDate)).ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Uid", (Session["Aid"]).ToString());
                    conn.Open();
                    count = cmd.ExecuteNonQuery();
                    if (count == 0)
                    {
                        return RedirectToAction("IntrestDetail");
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                count = 0;
            }
            return RedirectToAction("IntrestDetail");
        }
    }
}