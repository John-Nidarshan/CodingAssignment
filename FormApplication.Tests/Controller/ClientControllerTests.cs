using AutoMapper;
using FakeItEasy;
using form.Controllers;
using Form.Core;
using Form.Data;
using Form.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApplication.Tests.Controller
{
    public class ClientControllerTests
    {

        private readonly IClientFormRepository _clientFormRepository;
        private readonly IMapper _mapper;

        public ClientControllerTests()
        {
            _clientFormRepository = A.Fake<IClientFormRepository>();
            _mapper = A.Fake<IMapper>();

        }
        [Fact]
        public async Task GetForms_ReturnsOkResultWithEmployerFormDtos()
        {
            // Arrange
            var controller = new ClientController(_clientFormRepository, _mapper);
            var formId = "24439bfb-743a-4abd-9393-fead02f1572f"; // Provide a form ID for testing
            var expectedForm = new ClientForm
            {
                Id = "24439bfb-743a-4abd-9393-fead02f1572f",
                FormId = "24439bfb-743a-4abd-9393-fead02f1572f",
                FirstName = "string",
                LastName = "string",
                Email = "string",
                Phone = "string",
                majorquestions= new List<CustomQuestion>
           {
            new CustomQuestion
            {
                QuestionId = "8d4ae0e3-06b6-4efd-9c2b-59e4b7471361",
                Type = "dropdown",
                Question = "nationality?",
                Answer = "Sri Lankan",
                Other = false,
                Choices = new List<string> { "Sri Lankan", "Indian" },
                MaxChoices = 0
            },
            new CustomQuestion
            {
                QuestionId = "39f62d71-0dd1-4993-b9df-895b1bc29cf7",
                Type = "dropdown",
                Question = "currentresidence",
                Answer = "",
                Other = false,
                Choices = new List<string> { "Kandy", "Colombo" },
                MaxChoices = 0
            },
            new CustomQuestion
            {
                QuestionId = "fa7e3576-afc9-4dac-afb5-4ca7bf1a17c0",
                Type = "date",
                Question = "dateofbirth",
                Answer = "1990-01-01",
                Other = false,
                Choices = new List<string>(),
                MaxChoices = 0
            },
            new CustomQuestion
            {
                QuestionId = "ff74a8c2-74fe-4e18-bc68-cc952ec8ec8c",
                Type = "dropdown",
                Question = "gender?",
                Answer = "Male",
                Other = false,
                Choices = new List<string> { "Male", "Female" },
                MaxChoices = 0
            }
        },
                AdditionalQuestion = new List<CustomQuestion>
            {
            new CustomQuestion
            {
                QuestionId = "paragraph",
                Type = "string",
                Question = "Tell us about yourself",
                Answer = "string",
                Other = false,
                Choices = new List<string> { "string" },
                MaxChoices = 0
            }
              }
            };
            var expectedDto = _mapper.Map<ClientFormDto>(expectedForm); 

    
            A.CallTo(() => _clientFormRepository.GetFormAsync(formId)).Returns(new List<ClientForm> { expectedForm });
            A.CallTo(() => _mapper.Map<ClientFormDto>(A<ClientForm>.Ignored)).Returns(expectedDto);
            var actionResult = await controller.GetEmoyerForms(formId);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var employerFormDtos = Assert.IsAssignableFrom<IEnumerable<ClientFormDto>>(okResult.Value);
            Assert.Single(employerFormDtos); // Ensure only one form is returned
            Assert.Equal(expectedDto, expectedDto);
        }

    }
}
