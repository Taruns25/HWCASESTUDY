using Microsoft.Data.SqlClient;
using projectmanagement.entity;
using System;
using System.Collections.Generic;

namespace projectmanagement.dao
{
    public class ProjectRepositoryImpl : IProjectRepository
    {
        private readonly string connectionString;
        public ProjectRepositoryImpl(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ProjectRepositoryImpl()
        {
            connectionString = projectmanagement.util.DBConnUtil.GetConnectionString();
        }

        // Create an employee in the database
        public bool CreateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Employee (Name, Designation, Gender, Salary) VALUES (@Name, @Designation, @Gender, @Salary)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Designation", employee.Designation);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while creating employee: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating employee: " + ex.Message);
                return false;
            }
        }

        // Delete an employee from the database
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Employee WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", employeeId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while deleting employee: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting employee: " + ex.Message);
                return false;
            }
        }

        // Get all employees from the database
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Employee";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee emp = new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Designation = reader.GetString(2),
                                    Gender = reader.GetString(3),
                                    Salary = (double)reader.GetDecimal(4)
                                };
                                employees.Add(emp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while retrieving employees: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while retrieving employees: " + ex.Message);
            }
            return employees;
        }

        // Create a new project in the database
        public bool CreateProject(Project project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Project (ProjectName, Description, StartDate, Status) VALUES (@ProjectName, @Description, @StartDate, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        cmd.Parameters.AddWithValue("@Description", project.Description);
                        cmd.Parameters.AddWithValue("@StartDate", project.StartDate);
                        cmd.Parameters.AddWithValue("@Status", project.Status);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while creating project: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating project: " + ex.Message);
                return false;
            }
        }

        // Delete a project from the database
        public bool DeleteProject(int projectId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Project WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", projectId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while deleting project: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting project: " + ex.Message);
                return false;
            }
        }

        // Create a new task in the database
        public bool CreateTask(projectmanagement.entity.Task task) // Use fully qualified name for Task
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Task (TaskName, ProjectId, EmployeeId, Status) VALUES (@TaskName, @ProjectId, @EmployeeId, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                        cmd.Parameters.AddWithValue("@ProjectId", task.ProjectId);
                        cmd.Parameters.AddWithValue("@EmployeeId", task.EmployeeId);
                        cmd.Parameters.AddWithValue("@Status", task.Status);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while creating task: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating task: " + ex.Message);
                return false;
            }
        }

        // Delete a task from the database
        public bool DeleteTask(int taskId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Task WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", taskId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while deleting task: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting task: " + ex.Message);
                return false;
            }
        }

        // Get all tasks for an employee in a project
        public List<projectmanagement.entity.Task> GetAllTasksForEmployeeInProject(int employeeId, int projectId)
        {
            List<projectmanagement.entity.Task> tasks = new List<projectmanagement.entity.Task>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Task WHERE EmployeeId = @EmployeeId AND ProjectId = @ProjectId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                projectmanagement.entity.Task task = new projectmanagement.entity.Task
                                {
                                    TaskId = reader.GetInt32(0),
                                    TaskName = reader.GetString(1),
                                    ProjectId = reader.GetInt32(2),
                                    EmployeeId = reader.GetInt32(3),
                                    Status = reader.GetString(4)
                                };
                                tasks.Add(task);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while retrieving tasks: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while retrieving tasks: " + ex.Message);
            }
            return tasks;
        }

        // Assign a project to an employee
        public bool AssignProjectToEmployee(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO EmployeeProject (EmployeeId, ProjectId) VALUES (@EmployeeId, @ProjectId)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while assigning project to employee: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while assigning project to employee: " + ex.Message);
                return false;
            }
        }

        // Assign a task in a project to an employee
        public bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO EmployeeTask (EmployeeId, ProjectId, TaskId) VALUES (@EmployeeId, @ProjectId, @TaskId)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@TaskId", taskId);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while assigning task to employee: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while assigning task to employee: " + ex.Message);
                return false;
            }
        }
    }
}
