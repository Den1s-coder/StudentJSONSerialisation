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

        public static void LoadData(out List<Student> students, out List<Group> groups)
        {
            students = new();
            groups = new();

            Dictionary<int, Group> groupDict = new();

            using SqlConnection conn = new(_connectionString);
            conn.Open();

            SqlCommand cmd = new("SELECT s.Id, s.FirstName, s.LastName, s.Age, g.Id AS GroupId, g.Name AS GroupName " +
                                 "FROM Students s LEFT JOIN Groups g ON s.GroupId = g.Id", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Group? group = null;

                if (!reader.IsDBNull(4)) 
                {
                    int groupId = reader.GetInt32(4);
                    string groupName = reader.GetString(5);

                    if (!groupDict.ContainsKey(groupId))
                    {
                        group = Group.CreateGroup(groupName);
                        group.ID = groupId;
                        groupDict.Add(groupId, group);
                    }

                    group = groupDict[groupId];
                }

                var student = new Student(
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3))
                {
                    id = reader.GetInt32(0),
                    Group = group
                };

                students.Add(student);
            }

            groups.AddRange(groupDict.Values);
        }


        //TODO Update student after added him to group
    }
}
