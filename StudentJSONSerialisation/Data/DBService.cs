using StudentJSONSerialisation.Entyties;
using System.Data.SqlClient;

namespace StudentJSONSerialisation.Data
{
    public static class DBService
    {
        private static string _connectionString = "Data Source=DESKTOP-KNGCD9Q;Initial Catalog=StudentAndGroups;Integrated Security=True";

        public static void AddStudent(Student student)
        {
            using SqlConnection conn = new(_connectionString);

            conn.Open();
            SqlCommand cmd = new($"INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES (@f, @l, @a, @g)", conn);
            cmd.Parameters.AddWithValue("@f", student.FirstName);
            cmd.Parameters.AddWithValue("@l", student.LastName);
            cmd.Parameters.AddWithValue("@a", student.Age);
            cmd.Parameters.AddWithValue("@g", student.Group?.ID ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public static void AddGroup(Group group)
        {
            using SqlConnection conn = new(_connectionString);

            conn.Open();
            SqlCommand cmd = new("INSERT INTO Groups (Name) Values (@n)", conn);
            cmd.Parameters.AddWithValue("@n", group.Name);
            cmd.ExecuteNonQuery();
        }

        public static List<Student> LoadStudents(List<Group> groups)
        {
            List<Student> students = new();
            using SqlConnection conn = new(_connectionString);
            conn.Open();

            SqlCommand cmd = new("SELECT Id, FirstName, LastName, Age, GroupId FROM Students", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int groupId = reader.IsDBNull(4) ? -1 : reader.GetInt32(4);
                Group? group = groups.FirstOrDefault(g => g.ID == groupId);

                Student student = new Student(
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3))
                {
                    id = reader.GetInt32(0),
                    Group = group
                };

                students.Add(student);
            }

            return students;
        }

        public static List<Group> LoadGroups()
        {
            List<Group> groups = new();
            using SqlConnection conn = new(_connectionString);
            conn.Open();

            SqlCommand cmd = new("SELECT Id, Name FROM Groups", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Group group = Group.CreateGroup(reader.GetString(1));
                group.ID = reader.GetInt32(0);
                groups.Add(group);
            }

            return groups;
        }

        public static void UpdateStudent(Student student)
        {
            using SqlConnection conn = new(_connectionString);
            conn.Open();

            SqlCommand cmd = new("UPDATE Students SET FirstName = @f, LastName = @l, Age = @a, GroupId = @g WHERE Id = @id",conn);

            cmd.Parameters.AddWithValue("@f", student.FirstName);
            cmd.Parameters.AddWithValue("@l", student.LastName);
            cmd.Parameters.AddWithValue("@a", student.Age);
            cmd.Parameters.AddWithValue("@g", student.Group?.ID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", student.id);

            cmd.ExecuteNonQuery();
        }
    }
}
