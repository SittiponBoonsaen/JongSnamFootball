﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamFootball.Entities.Models;

namespace JongSnamFootball.Interfaces.Repositories
{
    public interface ISubDistrictRepository
    {
        Task<List<SubDistrictModel>> GetSubDistrictByDistrictId(int DistrictId);

        Task<SubDistrictModel> GetSubDistrictById(int id);
    }
}
