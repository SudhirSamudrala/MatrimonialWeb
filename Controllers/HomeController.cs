using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MatrimonialWebsite.Models;

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
            string Role = "";
            DataSet ds = new DataSet("cust");
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("select Role from userstable o where o.EmailId=@Email and Password=@password", conn);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    try
                    {
                        conn.Open();
                        Role = Convert.ToString(cmd.ExecuteScalar());
                        if (Role == "")
                        {
                            ViewBag.Message = "Invalid User.";
                            return View("Login");
                        }
                        MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT (Select id from userstable Where EmailId = '" + (obj.Email).ToString() + "' and Password='" + (obj.password).ToString() + "' ) as MID,o.* FROM userstable o Where o.id not in (Select id from userstable Where EmailId = '" + (obj.Email).ToString() + "' and Password='" + (obj.password).ToString() + "')", conn);


                        //SELECT(Select id from userstable Where EmailId = 'gail.k@gmail.com' and Password = '123456') as MID,o.* FROM userstable o Where o.id not in (Select id from userstable Where EmailId = 'gail.k@gmail.com' and Password = '123456')  
                        da.Fill(ds, "Customers");
                        //And o.Gender not in (Select Gender from userstable Where EmailId = '" + (obj.Email).ToString() + "' and Password='" + (obj.password).ToString() + "'
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
                Role = "";
            }       
            Session["uid"] = obj.Email;
            Session["pwd"] = obj.password;
            if (ds.Tables[0].Rows.Count!=0)
            {
                Session["id"] = ds.Tables[0].Rows[0]["MID"].ToString();
            }
            Session["Role"] = Role;
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
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("Update userstable set FileName=@FileName Where id =@id", conn);
                    cmd.Parameters.AddWithValue("@FileName", "/UploadedFiles/" + file.FileName);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Session["id"]));

                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteNonQuery()); 
                        if (count == 0)
                        {
                            return View("FileUpload");
                        }
                    }
                    catch (Exception ex)
                    {
                        return View("FileUpload");
                    }
                }
            }
            else
            {
                ViewBag.Message = "Upload Photo.";
                return View("FileUpload");
            }
            return View("Login");
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(RegistrationDetails obj)
        { 
            int count = 0;
            string id = "";
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd1 = new MySql.Data.MySqlClient.MySqlCommand("select Count(*) from userstable o where o.EmailId=@username And o.Password=@password", conn);
                    cmd1.Parameters.AddWithValue("@username", obj.EmailId);
                    cmd1.Parameters.AddWithValue("@password", obj.Password);
                    try
                    {
                        conn.Open();
                        count = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (count > 0)
                        { 
                            ViewBag.Message = "User Name Already Exist.";
                            return View("UserRegistration");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into userstable(FullName,EmailId,Age,Mobile,Password,RegDate,Profession,Hobbies,Religion,Native,Height,Weight,Role,Gender) values(@FullName,@EmailId,@Age,@Mobile,@Password,@RegDate,@Profession,@Hobbies,@Religion,@Native,@Height,@Weight,@Role,@Gender); SELECT LAST_INSERT_ID();", conn);                                                                                                                               
                    cmd.Parameters.AddWithValue("@FullName", obj.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                    cmd.Parameters.AddWithValue("@Age", obj.Age);
                    cmd.Parameters.AddWithValue("@Mobile", obj.mobile);
                    cmd.Parameters.AddWithValue("@Password", obj.Password);
                    cmd.Parameters.AddWithValue("@RegDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Profession", obj.Profession);
                    cmd.Parameters.AddWithValue("@Hobbies", obj.Hobbies);
                    cmd.Parameters.AddWithValue("@Religion", obj.Religion);
                    cmd.Parameters.AddWithValue("@Native", obj.Native);
                    cmd.Parameters.AddWithValue("@Height", obj.Height);
                    cmd.Parameters.AddWithValue("@Weight", obj.Weight);
                    cmd.Parameters.AddWithValue("@Role", obj.Role);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);

                    try
                    {
                        //conn.Open();
                        //count = cmd.ExecuteNonQuery();
                        id = Convert.ToString(cmd.ExecuteScalar());
                        if (id == "0" || id == "" || id == null || id == "null")
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
            //return View("Login");
            Session["id"] = id;
            return View("FileUpload");
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
                if ((Session["Role"]).ToString()== "Admin")
                {
                    //---------------
                    string Role = "";
                    DataSet ds = new DataSet("cust");
                    try
                    {
                        using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                        { 
                            try
                            { 
                                MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT (Select id from userstable Where EmailId = '" + (Session["uid"]).ToString() + "' and Password='" + (Session["pwd"]).ToString() + "' ) as MID,o.* FROM userstable o Where o.id not in (Select id from userstable Where EmailId = '" + (Session["uid"]).ToString() + "' and Password='" + (Session["pwd"]).ToString() + "')", conn);
                                da.Fill(ds, "Customers");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        Role = "";
                    } 
                    Session["id"] = ds.Tables[0].Rows[0]["MID"].ToString();
                    //Session["Role"] = Role;
                    Models.RegistrationDetails Re = new Models.RegistrationDetails();
                    Re.DT = ds.Tables[0];
                    return View("Index", Re);
                    //--------------- 
                } 
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
                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter("SELECT * FROM userstable o Where o.EmailId not in ('"+ (Session["uid"]).ToString() + "')", conn);
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
                    string Query = "";
                    if ((Session["Role"]).ToString() == "Admin")
                    {
                        Query = "SELECT a.Uid,a.id,a.AppliedUserId,u.FullName,a.MeetingDate,a.MeetingTime,'" + (Session["Role"]).ToString() + "' as Role FROM applyformeting a,userstable u WHERE a.AppliedUserId=u.id order by u.id";
                    }
                    else
                    {
                        Query = "SELECT a.Uid,a.id,a.AppliedUserId,u.FullName,a.MeetingDate,a.MeetingTime,'" + (Session["Role"]).ToString() + "' as Role FROM applyformeting a,userstable u WHERE a.AppliedUserId=u.id and a.id=" + "'" + obj.RegistrId + "'  order by u.id";
                    }

                    MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(Query, conn);
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
            if (ds.Tables[0].Rows.Count!=0)
            {
                Session["Aid"] = ds.Tables[0].Rows[0][0].ToString();
            }
            return View(Re);

        }
        [HttpPost]
        public ActionResult UpdateMeeting(Models.RegistrationDetails obj)
        {
            //-----
            int count = 0;
            if (obj.MeetingDate == null)
            {
                return RedirectToAction("IntrestDetail");
            }
            if (obj.MeetingTime == null)
            {
                return RedirectToAction("IntrestDetail");
            }
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("update applyformeting set MeetingDate = @MeetingDate , MeetingTime = @MeetingTime where Uid = @Uid", conn);
                    cmd.Parameters.AddWithValue("@MeetingTime", (obj.MeetingTime + " " + obj.AMorPM).ToString());
                    cmd.Parameters.AddWithValue("@MeetingDate", (obj.MeetingDate).ToString("yyyy-MM-dd"));
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