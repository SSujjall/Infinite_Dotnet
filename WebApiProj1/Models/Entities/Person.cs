namespace WebApiProj1.Models.Entities
{
    public class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Dob { get; set; }

        //public Person(string id, string name, int age, DateTime dob)
        //{
        //    Id = id;
        //    Name = name;
        //    Age = age;
        //    Dob = dob;
        //}
    }
}
