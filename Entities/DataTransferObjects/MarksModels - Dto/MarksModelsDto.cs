using Entities.DataTransferObjects.Marks___Dto;
using Entities.DataTransferObjects.Models___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.MarksModels___Dto
{
    public class MarksModelsDto
    {
        public int ID { get; set; }

        public MarksDto Marks { get; set; }

        public ModelsDto Model { get; set; }

    }
}
