using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace NaiveBayes.src.Structure
{
    public interface IEmail
    {
        public double Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        [EnumIgnoreCase]
        public EmailClass Class { get; set; }
        public string? Date { get; set; }

        public void PerformDeserializationLogic();
    }
}
