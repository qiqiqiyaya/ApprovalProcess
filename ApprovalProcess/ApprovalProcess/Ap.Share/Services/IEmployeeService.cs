﻿using System.Threading.Tasks;
using Ap.Share.Models;

namespace Ap.Share.Services
{
    public interface IEmployeeService
    {
        ValueTask<User> GetAsync(string id);
    }
}
