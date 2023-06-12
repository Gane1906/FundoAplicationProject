using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace ModelLayer.Model
{
    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public T Data { get; set; }
    }
}
