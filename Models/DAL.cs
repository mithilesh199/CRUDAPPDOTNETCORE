using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAPPDOTNETCORE.Models
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM FirstTable",connection);
            DataTable dt = new DataTable();
            List<Employee> lstEmployees = new List<Employee>();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    employee.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    employee.Age = Convert.ToString(dt.Rows[i]["Age"]);
                    employee.Gender = Convert.ToString(dt.Rows[i]["Gender"]);
                    employee.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    lstEmployees.Add(employee);

                }
            }
            if(lstEmployees.Count>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.listEmployee = lstEmployees;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
               response.listEmployee = null;
            }

            return response;
        }

        public Response GetAllEmployeeById(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM FirstTable  WHERE ID = '"+id+"'", connection);
            DataTable dt = new DataTable();
            Employee Employees = new Employee();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    employee.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    employee.Age = Convert.ToString(dt.Rows[0]["Age"]);
                    employee.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    employee.Email = Convert.ToString(dt.Rows[0]["Email"]);
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.Employee = employee;
                
            }          
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data found";
                response.Employee = null;
                
            }

            return response;
        }

        public Response AddEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO FirstTable(Name,Age,Gender,Email) VALUES('"+ employee .Name+ "','" + employee.Age + "','" + employee.Gender + "','" + employee.Email + "')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                  response.StatusCode = 200;
                  response.StatusMessage = "Employee added";                  
            }
            else
            {
                  response.StatusCode = 100;
                  response.StatusMessage = "No Data inserted";                
            }
            return response;
        }

        public Response UpdateEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("UPDATE FirstTable SET Name ='" + employee.Name + "', Age ='" + employee.Age + "', Gender = '" + employee.Gender + "', Email = '" + employee.Email + "' WHERE ID = '"+ employee.Id + "' ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee updated";                
            }
            else
            {
                 response.StatusCode = 100;
                 response.StatusMessage = "No Data updated";                 
            }
            return response;
        }

        public Response DeleteEmployee(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("DELETE From FirstTable WHERE ID = '" +id + "' ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                   response.StatusCode = 200;
                   response.StatusMessage = "Employee deleted";
                
            }
            else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No employee deleted";                    
        }

            return response;
        }
    }
}
