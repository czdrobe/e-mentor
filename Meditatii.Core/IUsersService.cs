using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface IUsersService:ILazyLoadable
    {
        SearchResult<User> GetUsers(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take);
        SearchResult<User> GetSuggestedUsers(string currentUser, int? categoryId, int? cityId);
        SearchResult<Request> GetRequests(string city, string subject, int skip, int take);

        Request GetRequest(int requestId);

        int SaveNewRequest(Request newRequest);

        UserSats GetUserStats();

        User GetUser(int userId);
        User GetUser(string username);
        void SaveUser(User user);

        void ActivateUser(int userId);

        void IncrementVisitorsNumber(int userId);
        void IncrementPhoneViews(int userId);

        User UpdateSubscriptionPeriod(string useremail, int period);

        void SaveCityForUser(string username, int city1, int city2, int city3, bool isOnline);

        List<City> GetCities();
    }
}
