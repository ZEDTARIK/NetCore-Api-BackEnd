using BackEndApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BackEndApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
	   // Dependency Injection
		private readonly IConfiguration _configuration;

		public DepartmentController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		public JsonResult Get()
		{
			string query = @"SELECT DepartmentId, DepartmentName 
							 FROM dbo.Department";

			DataTable dataTable= new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader MyReader;
			try
			{
				using (SqlConnection myCon = new SqlConnection(sqlDataSource))
				{
					myCon.Open();
					using (SqlCommand myCommand = new SqlCommand(query, myCon))
					{
						MyReader = myCommand.ExecuteReader();
						dataTable.Load(MyReader);
						MyReader.Close();
					}
					myCon.Close();
				}
				return new JsonResult(dataTable);
			}
			catch (Exception)
			{
				throw new Exception("Error");
			}
		}

		[HttpGet("{id}")]
		public JsonResult Get(int id)
		{
			string query = @"SELECT DepartmentId, DepartmentName FROM dbo.Department WHERE DepartmentId = @DepartmentId";
			DataTable dataTable = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader MyReader;
			try
			{
				using (SqlConnection myCon = new SqlConnection(sqlDataSource))
				{
					myCon.Open();
					using (SqlCommand myCommand = new SqlCommand(query, myCon))
					{
						myCommand.Parameters.AddWithValue("DepartmentId", id);
						MyReader = myCommand.ExecuteReader();
						dataTable.Load(MyReader);
						MyReader.Close();
						myCon.Close();
					}
				}
				return new JsonResult(dataTable);
			}
			catch (Exception)
			{
				throw new Exception("Error");
			}
		}

		[HttpPost]
		public JsonResult Post(Department department)
		{
			string query = @"INSERT INTO dbo.Department VALUES (@DepartmentName)";

			DataTable dataTable = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader MyReader;

			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("DepartmentName", department.DepartmentName);
					MyReader = myCommand.ExecuteReader();
					dataTable.Load(MyReader);
					MyReader.Close();
				}
				myCon.Close();
			}
			return new JsonResult(department.DepartmentName);
		}

		[HttpPut]
		public JsonResult Put(Department department)
		{
			string query = @"UPDATE dbo.Department 
								SET DepartmentName = @DepartmentName
							WHERE DepartmentId = @DepartmentId";

			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader MyReader;

			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("DepartmentId", department.DepartmentId);
					myCommand.Parameters.AddWithValue("DepartmentName", department.DepartmentName);
					MyReader = myCommand.ExecuteReader();
					MyReader.Close();
				}
				myCon.Close();
			}
			return new JsonResult(department);
		}

		[HttpDelete("{id}")]
		public JsonResult Delete(int id)
		{
			string query = @"DELETE FROM dbo.Department WHERE DepartmentId = @DepartmentId";

			DataTable dataTable = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			SqlDataReader MyReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("DepartmentId", id);
					MyReader = myCommand.ExecuteReader();
					dataTable.Load(MyReader);
					MyReader.Close();
				}
				myCon.Close();
			}
			return new JsonResult("Department Deleted");
		}
	}
}
