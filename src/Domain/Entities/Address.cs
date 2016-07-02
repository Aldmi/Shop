using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [ComplexType]
    public class Address
	{
	    public string AdressLine { get; set; }

        public string City { get; set; }

	    public int ZipCode { get; set; }
    }
}
