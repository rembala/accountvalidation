using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBusinessLayer.Validations.Interfaces
{
    public interface IFileAccountMessageValidator
    {
        string GetErrorMessageIfAccountIsInvalid(string bankAccount);
    }
}
