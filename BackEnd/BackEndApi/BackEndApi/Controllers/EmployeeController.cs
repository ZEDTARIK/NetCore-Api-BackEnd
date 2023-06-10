using BackEndApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = @"SELECT EmployeeId, EmployeeName, Department, DateOfJoining, PhotoFileName 
                             FROM dbo.Employee";

            string dataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader reader;
            DataTable dataTable = new DataTable();

            using(SqlConnection sqlConnection= new SqlConnection(dataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    reader= sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                }
                sqlConnection.Close();
            }
                
            return new JsonResult(dataTable);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT EmployeeId, EmployeeName, Department, DateOfJoining, PhotoFileName 
                             FROM dbo.Employee 
                              WHERE EmployeeId = @EmployeeId";

            string dataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader reader;
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(dataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", id);
                    reader = sqlCommand.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post(Employee employee) 
        {
            string query = @"INSERT INTO dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName)
                                VALUES (@EmployeeName, @Department, @DateOfJoining, @PhotoFileName)";

            string dataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader reader;
            using(SqlConnection sqlConnection = new SqlConnection(dataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                    sqlCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    sqlCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);

                    reader = sqlCommand.ExecuteReader();
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return new JsonResult(employee);
        }

        [HttpPut]
        public JsonResult Put(Employee employee) 
        {
            string query = @" UPDATE dbo.Employee 
                                SET EmployeeName =@EmployeeName, 
                                    Department =@Department, 
                                    DateOfJoining= @DateOfJoining, 
                                    PhotoFileName = @PhotoFileName
                             WHERE EmployeeId= @EmployeeId";

            string dataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(dataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    sqlCommand.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    sqlCommand.Parameters.AddWithValue("@Department", employee.Department);
                    sqlCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    sqlCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);

                    reader = sqlCommand.ExecuteReader();
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return new JsonResult(employee);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE  FROM dbo.Employee WHERE EmployeeId = @EmployeeId";
            string dataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader reader;

            using (SqlConnection sqlConnection = new SqlConnection(dataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", id);
                    reader = sqlCommand.ExecuteReader();
 
                }
                sqlConnection.Close();
            }
            return new JsonResult("Employee Deleted !");
        }

        [Route("saveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);

            } catch (Exception)
            {
                return new JsonResult("anonymos.png");
            }
        }
    }
}
