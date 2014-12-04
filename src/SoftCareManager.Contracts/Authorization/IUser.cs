namespace SoftCareManager.Contracts.Authorization
{
    public interface IUser
    {
        string Name { get; }
        Role Roles { get; }
        Right Rights { get; }

        void AddRole(Role roleToAdd);

        void RemoveRole(Role roleToRemove);

        bool IsInRole(Role roleToCheck);
    }
}