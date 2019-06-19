using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Meditatii.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System;

namespace meditatii.Controllers.Api
{
    public class AppoitmentApiController : ApiController
    {
        private IAppoitmentService appoitmentService;

        public AppoitmentApiController(IAppoitmentService appoitmentService)
        {
            this.appoitmentService = appoitmentService;
        }

        [HttpGet]
        [Route("api/appoitments/getappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetAppoitments(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/appoitments/getactiveappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetActiveAppoitments(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetActiveAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/appoitments/getoldappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetOldAppoitments(int page)
        {
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetOldAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpPost]
        [Route("api/appoitments/saveappoitment")]
        public void SaveAppoitment(AppoitmentModel newAppoitment)
        {
            this.appoitmentService.SaveNewAppoitment(System.Web.HttpContext.Current.User.Identity.Name, newAppoitment.TeacherId, newAppoitment.StartDate, newAppoitment.EndDate);
        }

        [HttpGet]
        [Route("api/appoitments/deleteappoitment/{appoitmentId}")]
        public void DeleteAppoitment(string appoitmentId)
        {
            //todo check if the user is logged in and if it a teacher or pupil of the appoitment - or admin
            this.appoitmentService.DeleteAppoitment(appoitmentId);
        }

        [HttpPost]
        [Route("api/appoitments/saveappoitmentchat")]
        public void SaveAppoitmentChat(AppoitmentChatModel newChat)
        {
            this.appoitmentService.SaveChat(newChat.AppoitmentId, System.Web.HttpContext.Current.User.Identity.Name, newChat.Message);
        }

        [HttpGet]
        [Route("api/appoitments/GetChatForAppoitment/{appoitmentid}")]
        public SearchResult<AppoitmentChatModel> GetChatForAppoitment(int appoitmentid)
        {
            int page = 1;
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            return MappingHelper.Map<SearchResult<AppoitmentChatModel>>(this.appoitmentService.GetChatForAppoitment(appoitmentid, skip, take));
        }

        [HttpGet]
        [Route("api/appoitments/GetRemaningTimeForAppoitment/{appoitmentId}")]
        public int GetRemaningTimeForAppoitment(int appoitmentId)
        {
            //return 50;
            var appoitment = this.appoitmentService.GetAppoitment(appoitmentId);
            return (int)Math.Ceiling(appoitment.EndDate.Subtract(DateTime.Now).TotalMinutes);
        }

        [HttpPost]
        [Route("api/appoitments/savepayments")]
        public void SavePayments([FromBody]int[] lstappoitmentid)
        {
            this.appoitmentService.Payment(lstappoitmentid, HttpContext.Current.User.Identity.Name);
        }
    }


    public class AppoitmentIdMocker
    {
        int[] Lstappoitmentid { get; set;}
    }
}