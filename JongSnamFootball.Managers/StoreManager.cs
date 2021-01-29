﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JongSnamFootball.Entities.Dtos;
using JongSnamFootball.Entities.Models;
using JongSnamFootball.Entities.Request;
using JongSnamFootball.Interfaces.Managers;
using JongSnamFootball.Interfaces.Repositories;
using JongSnamFootball.Managers.Extensions;

namespace JongSnamFootball.Managers
{
    public class StoreManager : IStoreManager
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public StoreManager(IStoreRepository storeRepository, IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<BasePagingDto<StoreDto>> GetListStore(int currentPage, int pageSize)
        {
            var listStore = await _storeRepository.GetAll();

            var listStoreDto = _mapper.Map<List<StoreDto>>(listStore);

            var result = MakePaging.StoreDtoToPaging(listStoreDto, currentPage, pageSize);

            return result;
        }

        public async Task<BasePagingDto<YourStore>> GetYourStores(int ownerId, int currentPage, int pageSize)
        {

            var listStore = await _storeRepository.GetStoreByOwnerId(ownerId);

            var listStoreDto = _mapper.Map<List<YourStore>>(listStore);

            var result = MakePaging.YourStoreDtoToPaging(listStoreDto, currentPage, pageSize);

            return result;
        }
        public async Task<bool> AddStore(StoreRequest requestDto)
        {
            try
            {
                var storeModel = _mapper.Map<StoreModel>(requestDto);

                await _repositoryWrapper.BeginTransactionAsync();

                await _repositoryWrapper.Store.CreateAsync(storeModel);

                await _repositoryWrapper.SaveAsync();

                await _repositoryWrapper.CommitAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        
    }
}
