namespace GUI_Index.Interfaces
{
    public interface IUser
    {

        string id { get; set; }

        string Username { get; set; }

        string GivenName { get; set; }

        string LastName { get; set; }

        

        string Password { get; set; }
    }
}