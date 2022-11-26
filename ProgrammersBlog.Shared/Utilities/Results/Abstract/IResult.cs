using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } // result status enum olarak verildi. Sonuç durumuna göre durum belirlenicek
        public string Message { get; }
        public Exception Exception { get; }
    }
}
