using ProjectStructureSample.Projects;
using System;

namespace ProjectStructureSample
{
    public static class ProjectFactory
    {
        public static object GetProjectInstance(string projectName)
        {
            switch (projectName)
            {
                case "Project 1": return Project1.GetInstance();
                case "Project 2": return Project2.GetInstance();
                default: throw new Exception();
            }
        }
    }
}
