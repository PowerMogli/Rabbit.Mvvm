namespace SoftCareManager.Contracts.Authorization
{
    public interface IUser
    {
        string Name { get; }

        Right Rights { get; }

        Role Roles { get; }

        void AddRole(Role roleToAdd);

        bool IsInRole(Role roleToCheck);

        void RemoveRole(Role roleToRemove);
    }
}