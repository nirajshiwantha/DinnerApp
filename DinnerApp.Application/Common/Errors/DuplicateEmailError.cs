using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinnerApp.Application.Common.Errors;

    public class DuplicateEmailError : IError
    {
        public List<IError> Reasons => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public Dictionary<string, object> Metadata => throw new NotImplementedException();
    }
