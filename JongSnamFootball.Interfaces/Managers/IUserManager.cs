﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamFootball.Entities.Dtos;
using JongSnamFootball.Entities.Request;

namespace JongSnamFootball.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<List<UserDto>> GetAll();

        Task<UserDto> GetUserById(int id);

        Task<bool> CreateUser(UserRequest requestDto);

        Task<bool> ChangePassword(int id, ChangePasswordRequest request);

        Task<bool> UpdateUser(int id, UpdateUserRequest request);
    }
}
