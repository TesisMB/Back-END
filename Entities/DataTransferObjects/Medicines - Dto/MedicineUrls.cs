using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicineUrls
    {
        public ICollection<string> Urls { get; }

        public MedicineUrls(ICollection<string> urls)
        {
            Urls = urls;
        }
    }
}
