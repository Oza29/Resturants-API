namespace Core.Users
{
    public record CurrentUser
    {
        public CurrentUser(string id,string email,IEnumerable<string>roles, string? nationality=null, DateOnly? dateOfBirth=null)
        {
            Id= id;
            Email = email;
            Roles = roles;
            Nationality= nationality;
            DateOfBirth= dateOfBirth;
        }
        public string Id { get; set; } 
        public string Email { get; set; }
        public IEnumerable<string>Roles { get; set; }
        public string? Nationality {  get; set; }
        public DateOnly? DateOfBirth { get; set; }

      public  bool IsInRole(string role)=>Roles.Contains(role);
    }
}
