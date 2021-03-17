﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JongSnamFootball.Entities.Models;

namespace JongSnamFootball.Interfaces.Managers
{
    public interface IAddressManager
    {
        Task<List<ProvinceModel>> GetProvinces();

        Task<List<DistrictModel>> GetDistrictByProvinceId(int ProvinceId);

        Task<List<SubDistrictModel>> GetSubDistrictByDistrictId(int DistrictId);
    }
}