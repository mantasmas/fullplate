using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Enums;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;
using Microsoft.Data.Edm.Library;
using Microsoft.EntityFrameworkCore;

namespace fullPlate.Services
{
    public class LunchService : ILunchService
    {
        public readonly FullPlateContext _dbContext;

        public LunchService(FullPlateContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LunchResponse> GetAllLunches()
        {
            List<LunchResponse> lunches = _dbContext.LunchDays
                .Include(x => x.LunchDayDishes)
                .Where(x => x.Deleted == false && x.LunchDate > DateTime.Now)
                .Select(x => new LunchResponse
                {
                    id = x.Id,
                    date = x.LunchDate,
                    lastRegistrationTime = x.LastRegisterDate,
                    enabled = x.Enabled,
                    paidByCompany = x.PaidByCompany,
                    dishes = _dbContext.LunchDayDishes
                    .Include(d => d.Dish)
                    .Where(d => d.LunchDayId.Equals(x.Id))
                    .Select(d => new DishResponse
                        {
                            id = d.Dish.Id,
                            name = d.Dish.Name,
                            price = d.Dish.Price,
                            dishType = d.Dish.DishType,
                            isVegetarian = d.Dish.IsVegetarian
                    }).ToList()
                })
                .ToList();

            return lunches;
        }

        public List<LunchResponse> GetAvailableLunches(string userId)
        {
            List<LunchResponse> lunches = _dbContext.LunchDays
                .Include(x => x.LunchDayDishes)
                .Include(x => x.Orders)
                .Where(x => x.Deleted == false && x.Enabled && x.LastRegisterDate > DateTime.Now)
                .Select(x => new LunchResponse
                {
                    id = x.Id,
                    date = x.LunchDate,
                    lastRegistrationTime = x.LastRegisterDate,
                    enabled = x.Enabled,
                    paidByCompany = x.PaidByCompany,
//                    userOrderId = x.Orders.Single(q => q.UserId.Equals(userId)).Id
                })
                .ToList();

            return lunches;
        }

        public List<LunchResponse> GetLunchSummary(int lunchId)
        {
            List<LunchResponse> lunches = _dbContext.LunchDays
                .Include(x => x.LunchDayDishes)
                .Where(x => x.Deleted == false && x.Enabled && x.LunchDate > new DateTime())
                .Select(x => new LunchResponse
                {
                    id = x.Id,
                    date = x.LunchDate,
                    lastRegistrationTime = x.LastRegisterDate,
                    enabled = x.Enabled,
                    paidByCompany = x.PaidByCompany
                })
                .ToList();

            return lunches;
        }

        public LunchResponse GetOneLunch(int lunchId)
        {
            LunchResponse lunch = _dbContext.LunchDays
                .Include(x => x.LunchDayDishes)
                .Where(x => x.Deleted == false && x.Id == lunchId)
                .Select(x => new LunchResponse
                {
                    id = x.Id,
                    date = x.LunchDate,
                    lastRegistrationTime = x.LastRegisterDate,
                    paidByCompany = x.PaidByCompany,
                    dishes = _dbContext.LunchDayDishes
                        .Include(d => d.Dish)
                        .Where(d => d.LunchDayId.Equals(x.Id))
                        .Select(d => new DishResponse
                        {
                            id = d.Dish.Id,
                            name = d.Dish.Name,
                            price = d.Dish.Price,
                            dishType = d.Dish.DishType,
                            isVegetarian = d.Dish.IsVegetarian
                        }).ToList(),
                })
                .Single();

            return lunch;
        }

        public LunchResponse CreateNewLuch(LunchDataRequest newLunchRequest)
        {
            LunchDay lunch = new LunchDay
            {
                LunchDate = newLunchRequest.lunchDate,
                LastRegisterDate = newLunchRequest.lastRegisterDate,
                Enabled = newLunchRequest.enabled,
                PaidByCompany = newLunchRequest.paidByCompany
            };

            _dbContext.LunchDays.Add(lunch);

            foreach (var dishId in newLunchRequest.dishIds)
            {
                LunchDish lunchDish = new LunchDish
                {
                    LunchDayId = lunch.Id,
                    DishId = dishId
                };

                _dbContext.LunchDayDishes.Add(lunchDish);
            }

            _dbContext.SaveChanges();

            return GetOneLunch(lunch.Id);
        }

        public bool RemoveLunch(int lunchId)
        {
            LunchDay lunch = new LunchDay
            {
                Id = lunchId,
                Deleted = true
            };

            _dbContext.LunchDays.Update(lunch);
            _dbContext.SaveChanges();

            return true;
        }

        public bool EnableLunch(int lunchId)
        {
            LunchDay lunch = new LunchDay
            {
                Id = lunchId,
                Enabled = true
            };

            _dbContext.LunchDays.Attach(lunch);
            var entry = _dbContext.Entry(lunch);
            entry.Property(x => x.Enabled).IsModified = true;

            _dbContext.SaveChanges();

            return true;
        }


        public List<AvailableDishResponse> GetAllDishes()
        {
            List<AvailableDishResponse> dishes = _dbContext.Dishes
                .Include(x => x.Restaurant)
                .Where(x => x.Deleted == false)
                .Select(x => new AvailableDishResponse
                {
                    id = x.Id,
                    name = x.Name,
                    price = x.Price,
                    dishType = x.DishType,
                    isVegetarian = x.IsVegetarian,
                    restaurantName = x.Restaurant.Name
                })
                .ToList();

            return dishes;
        }
    }
}
