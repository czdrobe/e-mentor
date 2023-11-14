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

        public  SearchResult<User> GetTeachersForNewsletter(int lastTeacherId)
        {
            return userData.GetTeachersForNewsletter(lastTeacherId);
        }
        public SearchResult<User> GetStudentsForNewsletter(int? studentId)
        {
            return userData.GetStudentsForNewsletter(studentId);
        }

        public UserSats GetUserStats()
        {
            return userData.GetUserStats();
        }

        public User GetUser(int userId)
        {
            return userData.GetUser(userId);
        }

        public SearchResult<User> GetSuggestedUsers(int userId, int? categoryId, int? cityId)
        {
            return userData.GetSuggestedUsers(userId, categoryId, cityId);
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

        public void LogLogin(int userId, int success)
        {
            userData.LogLogin(userId, success);
        }

        public void IncrementPhoneViews(int userId)
        {
            userData.IncrementPhoneViews(userId);
        }

        public void SaveCityForUser(string username, int city1, bool isOnline, bool atTeacher, bool atStudent)
        {
            userData.SaveCityForUser(username, city1, isOnline, atTeacher, atStudent);
        }

        public User UpdateSubscriptionPeriod(int userId, int period)
        {
            return userData.UpdateSubscriptionPeriod(userId, period);
        }

        public List<City> GetCities()
        {
            return userData.GetCities();
        }
        public List<Experience> GetExperience()
        {
            return userData.GetExperiences();
        }
        public List<Occupation> GetOccupation()
        {
            return userData.GetOccupations();
        }
    }
}
