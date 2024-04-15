using DemoApp.Models;
using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;
using DemoApp1.UNIT_OF_WORK.INTERFACE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DemoApp.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResumeService _ResumeService;
        private readonly ApplicationDbContext _dbContext;

        public ResumeController(IUnitOfWork unitOfWork, IResumeService ResumeService, ApplicationDbContext dbContext) 
        {
            _unitOfWork = unitOfWork;
            _ResumeService = ResumeService; 
            _dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult Step1()
        {
            // Retrieve Step 1 data from TempData or session storage if available
            Step1ViewModel model = TempData["Step1Data"] as Step1ViewModel ?? new Step1ViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Step1(Step1ViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { status = false });

            }

            // Save Step 1 data to TempData or session storage
            TempData["Step1Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            // Redirect to Step 2
            return Ok(new { status = true });

        }

        [HttpGet]
        public ActionResult Step2()
        {
            // Retrieve Step 1 data from TempData or session storage
            Step1ViewModel step1Data = TempData["Step1Data"] as Step1ViewModel;
            if (step1Data == null)
            {
                // Redirect to Step 1 if Step 1 data is not available
                return RedirectToAction("Step1");
            }

            // Proceed to Step 2
            Step2ViewModel model = TempData["Step2Data"] as Step2ViewModel ?? new Step2ViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Step2(Step2ViewModel model)
        {
            if (model.EducationDetails == null)
            {
                return Ok(new { status = false });

            }

            // Save Step 2 data to TempData or session storage
            TempData["Step2Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(model.EducationDetails);

            // Redirect to Step 3
            return Ok(new { status = true });

        }


        [HttpGet]
        public ActionResult Step3()
        {
            Step2ViewModel step2Data = TempData["Step2Data"] as Step2ViewModel;
            if (step2Data == null)
            {
                return RedirectToAction("Step2");
            }

            // Proceed to Step 3
            Step3ViewModel model = TempData["Step3Data"] as Step3ViewModel ?? new Step3ViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Step3(Step3ViewModel model)
        {
            if (model.WorkExperiences==null )
            {
                return Ok(new { status = false });

            }

            TempData["Step3Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(model.WorkExperiences);
            return Ok(new { status = true });

        }

        [HttpGet]
        public ActionResult Step4()
        {
            Step3ViewModel step3Data = TempData["Step3Data"] as Step3ViewModel;
            if (step3Data == null)
            {
                return Ok(new { status = false });

            }

            // Proceed to Step 4
            Step4ViewModel model = TempData["Step4Data"] as Step4ViewModel ?? new Step4ViewModel();
            return Ok(new { status = true });

        }

        [HttpPost]
        public ActionResult Step4(Step4ViewModel model)
        {
            if (model.KnownLanguages == null)
            {
                return Ok(new { status = false });

            }

            TempData["Step4Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(model.KnownLanguages);
            return Ok(new { status = true });

        }

        [HttpGet]
        public ActionResult Step5()
        {
            Step4ViewModel step4Data = TempData["Step4Data"] as Step4ViewModel;
            if (step4Data == null)
            {
                return RedirectToAction("Step4");
            }

            // Proceed to Step 5
            Step5ViewModel model = TempData["Step5Data"] as Step5ViewModel ?? new Step5ViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Step5(Step5ViewModel model)
        {
            if (model.TechnicalExperiences == null)
            {
                return Ok(new { status = false });

            }

            TempData["Step5Data"] = Newtonsoft.Json.JsonConvert.SerializeObject(model.TechnicalExperiences);
            return Ok(new { status = true });

        }

        [HttpGet]
        public ActionResult Step6()
        {
            Step5ViewModel step5Data = TempData["Step5Data"] as Step5ViewModel;
            if (step5Data == null)
            {
                return RedirectToAction("Step5");
            }

            // Proceed to Step 6
            Step6ViewModel model = TempData["Step6Data"] as Step6ViewModel ?? new Step6ViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Step6(Step6ViewModel model)
        {
            int insertedId = 0;

            if (model.Preference == null)
            {
                return Ok(new { status = false });
            }

            // Retrieve TempData values
            var step1Data = JsonConvert.DeserializeObject<Step1ViewModel>((string)TempData["Step1Data"]);
            var step2Data = JsonConvert.DeserializeObject<List<EducationDetailEntity>>((string)TempData["Step2Data"]);
            var step3Data = JsonConvert.DeserializeObject<List<WorkExperienceEntity>>((string)TempData["Step3Data"]);
            var step4Data = JsonConvert.DeserializeObject<List<LanguageProficiencyEntity>>((string)TempData["Step4Data"]);
            var step5Data = JsonConvert.DeserializeObject<List<TechnicalExperienceEntity>>((string)TempData["Step5Data"]);

            // Check if TempData values are null and handle accordingly
            if (step1Data == null || step2Data == null || step3Data == null || step4Data == null || step5Data == null)
            {
                // Handle missing TempData values
                return RedirectToAction("Step1");
            }

            // Create a new Resume instance and populate it with data from all steps
            var resume = new ResumeEntity
            {
                FullName = step1Data.FullName,
                Summary = step1Data.Summary,
                Email = step1Data.Email,
                Phone = step1Data.Phone,
                Address = step1Data.Address,
                DateOfBirth = step1Data.DateOfBirth,
                Preference = model.Preference
            };

            // Add resume to the database
            _unitOfWork.ResumeRepo.Add(resume);
            _unitOfWork.SaveChanges(); // This will generate the ID for the resume entity

            // Now, add related entities to the database context
            foreach (var educationDetail in step2Data)
            {
                educationDetail.ResumeId = resume.Id; // Assign the resume ID to the foreign key
                _unitOfWork.EducationDetailRepo.Add(educationDetail); // Add education detail to context
            }

            foreach (var workExperience in step3Data)
            {
                workExperience.ResumeId = resume.Id;
                _unitOfWork.WorkExperienceRepo.Add(workExperience);
            }

            foreach (var knownLanguage in step4Data)
            {
                knownLanguage.ResumeId = resume.Id;
                _unitOfWork.LanguageProficiencyRepo.Add(knownLanguage);
            }

            foreach (var technicalExperience in step5Data)
            {
                technicalExperience.ResumeId = resume.Id;
                _unitOfWork.TechnicalExperienceRepo.Add(technicalExperience);
            }

            // Save changes to persist related entities to the database
            _unitOfWork.SaveChanges();

            // Retrieve the inserted ID
            insertedId = resume.Id;

            // Clear TempData after completion
            TempData.Clear();

            // Redirect to success page or take other appropriate action
            return Ok(new { status = true, id = insertedId });
        }


        [HttpGet]
        public ActionResult ResumeSubmitSuccess([FromQuery] int id)
        {
            // Fetch the specific resume and its associated details by ID
            var resume =  _ResumeService.GetResumeByIdAsync(id);

            if (resume == null)
            {
                // Handle the case where the resume with the given ID is not found
                return NotFound();
            }

            // Create the view model
            var successViewModel = new SuccessViewModel
            {
                SuccessMessage = "Success!", // Set your success message here
                Resume = resume.Result
            };

            return View("_ResumeSubmitSuccessView.cshtml", successViewModel);

        }
    }

}
