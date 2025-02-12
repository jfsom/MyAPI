namespace MyAPI.Models
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId)
        {
            //Any Business Logic
            return _userRepository.GetUserById(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            //Any Business Logic
            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            //Any Business Logic
            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            //Any Business Logic
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            //Any Business Logic
            _userRepository.DeleteUser(userId);
        }
    }
}