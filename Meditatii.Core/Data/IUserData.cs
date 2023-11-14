using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IUserData: ILazyLoadable
    {
        SearchResult<User> GetAll(int skip, int take, int? order);

        SearchResult<User> GetSuggestedUsers(int userId, int? categoryId, int? cityId);

        SearchResult<User> GetTeachersForNewsletter(int lastTeacherId);
        SearchResult<User> GetStudentsForNewsletter(int? studentId);

        SearchResult<Request> GetRequests(string city, string subject, int skip, int take);

        Request GetRequest(int requestId);

        int SaveNewRequest(Request newRequest);

        SearchResult<User> GetUsers(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take);

        UserSats GetUserStats();

        User GetUser(int userId);

        User GetUser(string useremail);

        void SaveUser(User user);

        void ActivateUser(int userId);

        void IncrementVisitorsNumber(int userId);

        void IncrementPhoneViews(int userId);

        void LogLogin(int userId, int success);

        User UpdateSubscriptionPeriod(int userId, int period);

        List<City> GetCities();

        List<Experience> GetExperiences();

        List<Occupation> GetOccupations();

        void SaveCityForUser(string username, int city1, bool isOnline, bool atTeacher, bool atStudent);
    }
}
