using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Form.Data;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using form.Controllers;
using Form.Core;
using Form.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FormApplication.Tests.Controller
{
    public class EmoloyerControllerTests
    {
        private readonly IEmoloyeeFormRepository _emoloyeeFormRepository;
        private readonly IMapper _mapper;

        public EmoloyerControllerTests()
        {
            _emoloyeeFormRepository = A.Fake<IEmoloyeeFormRepository>();
            _mapper = A.Fake<IMapper>();

        }

        [Fact]
        public async Task GetEmoyerForms_ReturnsOkResultWithEmployerFormDtos()
        {
            // Arrange
            var controller = new EmoloyerController(_emoloyeeFormRepository, _mapper);
            var formId = "2"; 
            var expectedForm = new EmployerForm
            {
                Id = "2",
                FormId = "2",
                ProgramTitle = "Program Title",
                ProgramDescription = "Program Description",
                PersonalInformation = new PersonalInformation
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Nationality = "USA",
                    CurrentResidence = "New York",
                    IdNumber = "ABC123",
                    DateOfBirth = "1990-01-01",
                    Gender = "Male",
                    AdditionalMultipleChoiceQuestion = new List<CustomQuestion>
            {
                new CustomQuestion
                {
                    QuestionId = "Q1",
                    Type = "multiplechoice",
                    Question = "Select your additional preference",
                    Other = false,
                    Choices = new List<string>(),
                    MaxChoices = 1
                }
            }
                },
                CustomQuestions = new List<CustomQuestion>
        {
            new CustomQuestion
            {
                QuestionId = "Q2",
                Type = "paragraph",
                Question = "please tell me about yourself in less than 500 words",
                Other = false,
                Choices = null,
                MaxChoices = 0
            },
            
        },
                AdditionalQuestions = new List<CustomQuestion>
        {
            new CustomQuestion
            {
                QuestionId = "Q7",
                Type = "yes/no",
                Question = "Are you a student?",
                Other = false,
                Choices = null,
                MaxChoices = 0
            }
        }
            };

            
            A.CallTo(() => _emoloyeeFormRepository.GetEmployeeAsync(formId)).Returns(new List<EmployerForm> { expectedForm });

           
            var expectedDto = _mapper.Map<EmployerFormDto>(expectedForm);
            A.CallTo(() => _mapper.Map<EmployerFormDto>(A<EmployerForm>.Ignored)).Returns(expectedDto);

            // Act
            var actionResult = await controller.GetEmoyerForms(formId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var employerFormDtos = Assert.IsAssignableFrom<IEnumerable<EmployerFormDto>>(okResult.Value);
            Assert.Single(employerFormDtos); 
            Assert.Equal(expectedDto, employerFormDtos.First()); 
        }

    }
}

