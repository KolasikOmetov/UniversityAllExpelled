using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityDatabaseImplement.Implements
{
    public class ReportStorage : IReportStorage
    {
        public ReportEducationPlanSubjectsViewModel GetEducationPlanSubjects(EducationPlanBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ReportEducationPlansViewModel> GetFullListEducationPlans(ReportEducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var eps = from lector in context.Lectors
                          join epLectors in context.EducationPlanLectors
                          on lector.Id equals travelTours.TourID
                          where tour.OperatorID == _OperatorID
                          join travel in context.Travels
                          on travelTours.TravelID equals travel.ID
                          where travel.DateStart >= model.DateFrom
                          where travel.DateEnd <= model.DateTo
                          join tourguide in context.TourGuides
                          on tour.ID equals tourguide.TourID
                          join guide in context.Guides
                          on tourguide.GuideID equals guide.ID
                          where guide.OperatorID == _OperatorID
                          join guideExcursion in context.GuideExcursions
                          on guide.ID equals guideExcursion.GuideID
                          join excursion in context.Excursions
                          on guideExcursion.ExcursionID equals excursion.ID
                          select new ReportGuidesViewModel
                          {
                              DateStartTravel = travel.DateStart,
                              GuideSurname = guide.Surname,
                              GuideName = guide.Name,
                              GuideWorkPlace = guide.WorkPlace,
                              ExcursionName = excursion.Name,
                              TourName = tour.Name
                          };
                return eps.ToList();
            }
        }
    }
}
