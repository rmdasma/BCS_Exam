/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using BCS_Exam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCS_Exam.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetCustomers(string parkCode, string arrival);
        Task<bool> SubmitResponse(string resId, string email);
    }
}