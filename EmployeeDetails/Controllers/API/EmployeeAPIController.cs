using EmployeeDetails.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails.Controllers.API
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeAPIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("api/EmployeeAPI/GetEmpData")]
        public IActionResult GetEmpData()
        {
            try
            {
                List<Employee> list = new List<Employee>();
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                SqlCommand cmd = new SqlCommand("Select * from Emp", con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.EmpId = Convert.ToInt32(dt.Rows[i]["EmpId"]);
                    emp.FirstName = dt.Rows[i]["FirstName"].ToString();
                    emp.LastName = dt.Rows[i]["LastName"].ToString();
                    emp.Phonenumber = dt.Rows[i]["PhoneNumber"].ToString();
                    emp.Email = dt.Rows[i]["Email"].ToString();
                    emp.Salary = float.Parse(dt.Rows[i]["Salary"].ToString());
                    if (!dt.Rows[i].IsNull("CreatedOn"))
                    {
                        emp.CreatedOn = (DateTime)dt.Rows[i]["CreatedOn"];
                    }
                    if (!dt.Rows[i].IsNull("UpdatedOn"))
                    {
                        emp.UpdatedOn = (DateTime)dt.Rows[i]["UpdatedOn"];
                    }
                    list.Add(emp);
                }
                con.Close();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message});
            }
        }

        [HttpGet]
        [Route("api/EmployeeAPI/GetEmpDataById")]
        public IActionResult GetEmpDataById(int Empid)
        {
            try
            {
                Employee emp = new Employee();
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                SqlCommand cmd = new SqlCommand("Select * from Emp Where EmpId=@Empid", con);
                cmd.Parameters.AddWithValue("@EmpId", Empid);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    emp.EmpId = Convert.ToInt32(reader["EmpId"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.Phonenumber = reader["PhoneNumber"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.Salary = float.Parse(reader["Salary"].ToString());
                    if (!reader.IsDBNull("CreatedOn"))
                    {
                        emp.CreatedOn = (DateTime)reader["CreatedOn"];
                    }
                    if (!reader.IsDBNull("UpdatedOn"))
                    {
                        emp.UpdatedOn = (DateTime)reader["UpdatedOn"];
                    }
                }

                reader.Close();
                con.Close();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/EmployeeAPI/AddEmpData")]
        public IActionResult AddEmpData([FromBody]Employee model)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                if (model.EmpId == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Emp " +
                               "VALUES (@FirstName, @LastName, @Phonenumber, @Email, @Salary, @CreatedOn, @UpdatedOn)", con);

                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Phonenumber", model.Phonenumber);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedOn", model.UpdatedOn ?? (object)DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Ok(model);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Emp " +
                   "SET FirstName = @FirstName, LastName = @LastName, " +
                   "PhoneNumber = @Phonenumber, Email = @Email, " +
                   "Salary = @Salary, UpdatedOn = @UpdatedOn " +
                   "WHERE EmpId = @EmpId", con);

                    cmd.Parameters.AddWithValue("@EmpId", model.EmpId);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Phonenumber", model.Phonenumber);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/EmployeeAPI/DeleteEmp")]
        public IActionResult DeleteEmp(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                SqlCommand cmd = new SqlCommand("Delete from Emp Where EmpId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
