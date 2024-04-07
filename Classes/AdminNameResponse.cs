namespace OrderUp_API.Classes {
    public class AdminNameResponse {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AdminNameResponse() { }

        public AdminNameResponse(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
