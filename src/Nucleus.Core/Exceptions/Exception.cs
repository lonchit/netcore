using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nucleus.Core.Exceptions
{
    public class Exception : SimpleEntity
    {
        public string Text { get; set; }
    }
}
