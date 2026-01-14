using DirectoryService.Core.Locations;

namespace DirectoryService.Core.Departments
{
    public sealed class DepartmentLocation
    {
        public Guid DepartmentId { get; private set; }

        public Department Department { get; private set; }

        public Guid LocationId {  get; private set; }

        public Location Location { get; private set; }
    }
}
