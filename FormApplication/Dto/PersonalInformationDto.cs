﻿using Form.Core;
using Newtonsoft.Json;

namespace Form.Dto
{
    public class PersonalInformationDto
    {
      
        public string FirstName { get; set; }

    
        public string LastName { get; set; }

    
        public string Email { get; set; }

     
        public string Phone { get; set; }

       
        public string Nationality { get; set; }

     
        public string CurrentResidence { get; set; }

       
        public string IdNumber { get; set; }

        
        public string DateOfBirth { get; set; }

       
        public string Gender { get; set; }

        
        public List<CustomQuestionDtos> AdditionalMultipleChoiceQuestion { get; set; }
    }
}
