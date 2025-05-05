using StudentJSONSerialisation.Entyties;
using System.Data.SqlClient;

namespace StudentJSONSerialisation.Data
{
    public static class DBService
    {
        

        public static void AddStudent (Student student)
        {
            using SqlConnection conn = new( _connectionString );

            conn.Open();
            SqlCommand cmd = new($"INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES (@f, @l, @a, @g)", conn);
            cmd.Parameters.AddWithValue("@f",student.FirstName);
            cmd.Parameters.AddWithValue("@l",student.LastName);
            cmd.Parameters.AddWithValue("@a",student.Age);
            cmd.Parameters.AddWithValue("@g",student.Group?.ID ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public static void AddGroup(Group group)
        {
            using SqlConnection conn = new(_connectionString);

            conn.Open();
            SqlCommand cmd = new("INSERT INTO Groups (Name) Values (@n)",conn);
            cmd.Parameters.AddWithValue("@n",group.Name);
            cmd.ExecuteNonQuery();
        }

        public static List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            using SqlConnection conn = new(_connectionString);

            conn.Open();
            SqlCommand cmd = new("Select s.Id, s.FirstName,s.LastName,s.Age,g.Id as Group.Id, g.Name FROM Students s LEFT JOIN Groups g ON s.GroupId = g.Id", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                Group? group = null;
                if (!reader.IsDBNull(4))
                {
                    group = Group.CreateGroup(reader.GetString(5));
                    group.ID = reader.GetInt32(4);
                }

                var student = new Student(reader.GetString(1), reader.GetString(2), reader.GetInt32(3)) { id = reader.GetInt32(0),Group = group };
                students.Add(student);
            }
            return students;
        } 
    }
}
