namespace Ccms.Common.StoredProcedures
{
    using Ccms.Common;

    public class ProjectStoredProcedure : ISingletonDependency
    {
        public const string GetNewProject = "Project.GetNewProject";
        public const string GetNewForm = "Project.GetNewForm";
        public const string SetAnswer = "Project.SetAnswer";
        public const string SetSampleStatus = "Project.SetSampleStatus";
        public const string UpdSample = "Project.UpdSample";
    }
}
