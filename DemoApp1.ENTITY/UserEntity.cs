using static DemoApp1.COMMON.ENUMS;

namespace DemoApp1.ENTITY
{
    public class UserEntity : DB_COMMONFIELDS
    {
        public int Id { get; set; }

        public string FullName {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
