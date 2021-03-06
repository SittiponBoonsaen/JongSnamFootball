﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ReservationManager : IReservationManager
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ReservationManager(IMapper mapper, IReservationRepository reservationRepository, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<BasePagingDto<ReservationDto>> GetYourReservation(int userId, int currentPage, int pageSize)
        {
            var reservations = await _reservationRepository.GetYourReservation(userId);

              var list = _mapper.Map<List<ReservationModel>, List<ReservationDto>>(reservations, opt =>
              {
                  opt.AfterMap((src, dest) =>
                  {
                      foreach (var d in dest)
                      {
                          d.Image = src[0].StoreModel.Image;
                      }
                  });
              });
            var result = MakePaging.ReservationDtoToPaging(list, currentPage, pageSize);

            return result;
        }
        public async Task<BasePagingDto<GrahpDto>> GraphMonthReservation(int userId, int month, int currentPage, int pageSize)
        {
            var listStore = await _reservationRepository.GetYourReservation(userId);

            var result = new ObservableCollection<GrahpDto>();

            foreach (var data in listStore)
            {
                if (data.CreatedDate.Value.Month == month)
                {
                    result.Add(new GrahpDto
                    {
                        Days = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Day,
                        Months = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Month,
                        Years = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Year
                    }); ;
                }
            }
            var listFieldDto = _mapper.Map<List<GrahpDto>>(result);

            var resultPaging = MakePaging.GraphDtoToPaging(listFieldDto, currentPage, pageSize);

            return resultPaging;
        }
        public async Task<BasePagingDto<GrahpDto>> GraphYearReservation(int userId, int year, int currentPage, int pageSize)
        {
            var listStore = await _reservationRepository.GetYourReservation(userId);

            var result = new ObservableCollection<GrahpDto>();

            foreach (var data in listStore)
            {
                if (data.CreatedDate.Value.Year == year)
                {
                    result.Add(new GrahpDto
                    {
                        Days = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Day,
                        Months = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Month,
                        Years = data.CreatedDate == null ? 0 : data.CreatedDate.Value.Year
                    }); ;
                }
            }
            var listFieldDto = _mapper.Map<List<GrahpDto>>(result);

            var resultPaging = MakePaging.GraphDtoToPaging(listFieldDto, currentPage, pageSize);

            return resultPaging;
        }

        public async Task<BasePagingDto<ReservationDto>> GetReservationBySearch(int userId, SearchReservationRequest request, int currentPage, int pageSize)
        {
            var listReservation = await _reservationRepository.GetReservationBySearch(userId, request);
            var lisReservationDto = _mapper.Map<List<ReservationDto>>(listReservation);

            var result = MakePaging.ReservationDtoToPaging(lisReservationDto, currentPage, pageSize);

            return result;
        }

        public async Task<ReservationDetailDto> GetShowDetailYourReservation(int Id)
        {
            var reservation = await _reservationRepository.GetShowDetailYourReservation(Id);

            var result = _mapper.Map<ReservationDetailDto>(reservation);
            result.ImageProfile = reservation.UserModel.ImageProfile;
            return result;
        }

        public async Task<bool> UpdateApprovalStatus(int id, ReservationApprovalRequest request)
        {
            try
            {
                var reservationModel = await _reservationRepository.GetReservatioById(id);

                reservationModel.ApprovalStatus = request.ApprovalStatus;
                reservationModel.UpdatedDate = DateTime.Now;

                _repositoryWrapper.Reservation.Updete(reservationModel);

                await _repositoryWrapper.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateReservation(ReservationRequest request)
        {
            try
            {
                var reservationModel = _mapper.Map<ReservationModel>(request);

                reservationModel.CreatedDate = DateTime.Now;
                reservationModel.UpdatedDate = DateTime.Now;
                reservationModel.ApprovalStatus = false;

                await _repositoryWrapper.Reservation.CreateAsync(reservationModel);

                await _repositoryWrapper.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReservation(int id)
        {
            try
            {
                var reservationModel = await _reservationRepository.GetReservatioById(id);
                if (reservationModel == null)
                {
                    return false;
                }

                await _repositoryWrapper.BeginTransactionAsync();

                reservationModel.Active = false;

                _repositoryWrapper.Reservation.Updete(reservationModel);

                await _repositoryWrapper.SaveAsync();

                await _repositoryWrapper.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }
        public async Task<bool> UpdateReservation(int id, UpdateReservationRequest request)
        {
            try
            {
                var reservation = await _reservationRepository.GetShowDetailYourReservation(id);

                var foo = reservation.PaymentModel.OfType<IList>().FirstOrDefault();

                reservation.StartTime = request.StartTime;
                reservation.StopTime = request.StopTime;
                reservation.UpdatedDate = DateTime.Now;
                _repositoryWrapper.Reservation.Updete(reservation);

                await _repositoryWrapper.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
