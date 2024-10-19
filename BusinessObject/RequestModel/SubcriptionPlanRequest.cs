using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class SubcriptionPlanRequest
    {
        public string PlanName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
