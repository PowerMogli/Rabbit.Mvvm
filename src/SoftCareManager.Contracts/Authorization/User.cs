namespace SoftCareManager.Contracts.Authorization
{
    public class User : IUser
    {
        public User(string name, Role roles, Right rights)
        {
            Name = name;
            Roles = roles;
            Rights = rights;
        }

        public string Name { get; private set; }

        public Right Rights { get; private set; }

        public Role Roles { get; private set; }

        public void AddRole(Role roleToAdd)
        {
            Roles |= roleToAdd;
        }

        public bool IsInRole(Role roleToCheck)
        {
            return Roles.HasFlag(roleToCheck);
        }

        public void RemoveRole(Role roleToRemove)
        {
            Roles &= ~roleToRemove;
        }
    }
}