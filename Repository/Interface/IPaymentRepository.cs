using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.payOS.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Repository.Interface
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {

    }
}
