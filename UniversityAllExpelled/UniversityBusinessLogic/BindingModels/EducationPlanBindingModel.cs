﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class EducationPlanBindingModel
    {
        public int? Id { get; set; }

        public string StreamName { get; set; }

        public int Hours { get; set; }

        public Dictionary<int, string> Lectors { get; set; }
        public Dictionary<int, string> Students{ get; set; }
    }
}
