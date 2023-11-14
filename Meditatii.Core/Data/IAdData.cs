using Meditatii.Core.Entities;
using Meditatii.CoreNew.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IAdData: ILazyLoadable
    {
        List<Ad> GetAdsForUser(int userId);

        Ad GetAd(int id);

        void AdView(int adId);

        int GetNrOfViewsForAd(int adId);

        SearchResult<Ad> GetAds(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take);

        SearchResult<Ad> GetAll(int skip, int take, int? order);

        void SaveAdForUser(Ad ad);

        void DeleteAdForUser(Ad ad);
    }
}
