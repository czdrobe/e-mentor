using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.CoreNew.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Services
{
    public class AdService : IAdService
    {
        private IAdData adData;
        public AdService(IAdData adData)
        {
            this.adData = adData;
        }

        public Ad GetAd(int id)
        {
            return adData.GetAd(id);
        }

        public void AdView(int adId)
        {
            adData.AdView(adId);
        }

        public int GetNrOfViewsForAd(int adId)
        {
            return adData.GetNrOfViewsForAd(adId);
        }

        public List<Ad> GetAdsForUser(int userId)
        {
            return adData.GetAdsForUser(userId);
        }

        public SearchResult<Ad> GetAds(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take)
        {
            return adData.GetAds(categoryId, cycleId, cityId, order, skip, take);
        }

        public SearchResult<Ad> GetAll(int skip, int take, int? order)
        {
            return adData.GetAll(skip, take, order);
        }

        public void SaveAdForUser(Ad ad)
        {
            adData.SaveAdForUser(ad);
        }

        public void DeleteAdForUser(Ad ad)
        {
            adData.DeleteAdForUser(ad);
        }
    }
}
