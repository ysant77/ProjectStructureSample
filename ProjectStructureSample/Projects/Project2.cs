using ProjectStructureSample.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace ProjectStructureSample.Projects
{
    public class Project2
    {
        public string Title { get; set; }
        private DataTable _model { get; set; }
        private static Project2 _instance;
        private DatabaseContext _context;
        public object ModelObject { get; set; }
        private Project2()
        {
            Title = "Project2";
            _context = DatabaseContext.GetInstance();
            _model = _context.GetProjectModel("getAllProject2Models");
            ModelObject = ModelGenerator.GetClass("Project2Model", _context.Properties);
           
        }

        public static Project2 GetInstance()
        {
            if (_instance == null)
                _instance = new Project2();
            return _instance;
        }
        public DataTable GetModel()
        {
            return _model;
        }
        private void LoadModelData()
        {
           // _model.Add(new Project2Model { Id = 1, Description = "Day1", CreatedDate = DateTime.Now, IsActive = true });
            //_model.Add(new Project2Model { Id = 2, Description = "Day2", CreatedDate = DateTime.Now, IsActive = false });
        }
    }
}
