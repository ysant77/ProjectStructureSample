using ProjectStructureSample.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace ProjectStructureSample.Projects
{
    public class Project1 
    {
        public string Title { get; set; }
        private DataTable _model { get; set; }
        private static Project1 _instance;
        private DatabaseContext _context;
        public object ModelObject { get; set; }
        private Project1()
        {
            Title = "Project1";
            _context = DatabaseContext.GetInstance();
            _model = _context.GetProjectModel("getAllProject1Models");
            ModelObject = ModelGenerator.GetClass("Project1Model", _context.Properties);
           
           
        }
        public DataTable GetModel()
        {
            return _model;
        }
        public static Project1 GetInstance()
        {
            if (_instance == null)
                _instance = new Project1();
            return _instance;
        }

        private void LoadModelData()
        {
            //_model.Add(new Project1Model { Id = 1, Name = "Yatharth" });
            //_model.Add(new Project1Model { Id = 2, Name = "Yash" });
            var db = DatabaseContext.GetInstance();
            
        }
    }
}