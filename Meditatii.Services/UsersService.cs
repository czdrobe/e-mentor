using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Services
{
    public class UsersService : IUsersService
    {
        private IUserData userData;
        public UsersService(IUserData userData)
        {
            this.userData = userData;
        }

        public SearchResult<User> GetUsers(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take)
        {
            return userData.GetUsers(categoryId, cycleId, cityId, order, skip, take);
        }

        public UserSats GetUserStats()
        {
            return userData.GetUserStats();
        }

        public User GetUser(int userId)
        {
            return userData.GetUser(userId);
        }

        public SearchResult<User> GetSuggestedUsers(string currentUser, int? categoryId, int? cityId)
        {
            return userData.GetSuggestedUsers(currentUser, categoryId, cityId);
        }

        public SearchResult<Request> GetRequests(string city, string subject, int skip, int take)
        {
            return userData.GetRequests(city, subject, skip, take);
        }

        public Request GetRequest(int requestId)
        {
            return userData.GetRequest(requestId);
        }

        public int SaveNewRequest(Request newRequest)
        {
            return userData.SaveNewRequest(newRequest);
        }

        public User GetUser(string useremail)
        {
            return userData.GetUser(useremail);
        }

        public void SaveUser(User user)
        {
            userData.SaveUser(user);
        }

        public void ActivateUser(int userId)
        {
            userData.ActivateUser(userId);
        }

        public void IncrementVisitorsNumber(int userId)
        {
            userData.IncrementVisitorsNumber(userId);
        }

        public void IncrementPhoneViews(int userId)
        {
            userData.IncrementPhoneViews(userId);
        }

        public void SaveCityForUser(string username, int city1, int city2, int city3, bool isOnline)
        {
            userData.SaveCityForUser(username, city1, city2, city3, isOnline);
        }

        public User UpdateSubscriptionPeriod(string useremail, int period)
        {
            return userData.UpdateSubscriptionPeriod(useremail, period);
        }

        public List<City> GetCities()
        {
            return userData.GetCities();
        }

    }
}
