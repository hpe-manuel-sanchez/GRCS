/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ArtistRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Jyoti Tyagi
 * Created on     : 09-10-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.UI.Interfaces;
using System.Collections.ObjectModel;

namespace UMGI.GRCS.UI.Models
{
    public class ArtistRepository : IArtistRepository
    {

        #region "Private Variables"

        private readonly IServiceFactory _serviceFactory;
        private readonly IArtist _artistService;
        private readonly IUser _userService;

        #endregion

        #region "Public Variables"

        public ISessionWrapper SessionWrapper { get; set; }
        public IConfigFactory ConfigurationFactory { get; set; }
        public IAddressBookModel AddressBookModel { get; set; }

        #endregion

        #region "Constants"
        /// <summary>
        /// Constants related to this class
        /// </summary>
        public class Constants
        {
            public const string ArtistService = "ArtistService";
            public const string UserService = "UserService";
        }
        #endregion


        #region "Artist Repository Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository"/> class.
        /// </summary>
        public ArtistRepository(IServiceFactory serviceFactory, IConfigFactory configFactory, ISessionWrapper sessionWrapper)
        {
            try
            {
                ObjArtistModel = new ArtistModel();
                _serviceFactory = serviceFactory;
                AddressBookModel = new AddressBookModel();

                ObjUserInfo = new UserInfo();

                _artistService = _serviceFactory.GetService<IArtist>(Constants.ArtistService);
                _userService = _serviceFactory.GetService<IUser>(Constants.UserService);

                SessionWrapper = sessionWrapper;
                ObjUserInfo = SessionWrapper.CurrentUserInfo;

                ConfigurationFactory = configFactory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Search for Artist
        /// </summary>
        /// <param name="searchOption">Search Parameters</param>
        /// <returns></returns>
        public ArtistSearchResult SearchArtist(ArtistSearchCriteria searchOption,bool artistQualifyingCriteria)
        {
            try
            {
                //var calcStartIndex = searchOption.PageNumber;
                //if (searchOption.PageNumber >= searchOption.PageSize)
                //    calcStartIndex = searchOption.PageNumber / searchOption.PageSize;
                //searchOption.PageNumber = calcStartIndex;
                //ObservableCollection<ArtistDetail> artists = new ObservableCollection<ArtistDetail>();
                //ArtistSearchResult artistsres = new ArtistSearchResult();


                // for (int i = 0; i <= 10; i++)
                // {
                //     ArtistDetail artistdetail = new ArtistDetail();
                //     ArtistSearch artistsearch = new ArtistSearch();
                //     ArtistInfo artistInfo = new ArtistInfo();
                //     artistdetail.FirstName = "Artist" + i;
                //     artistdetail.LastName = "LastName" + i;
                //     artistdetail.AdditonalInfo = "AddInfo" + i;
                //     artistdetail.AliasIndicator = "aliasIndicator" + i;
                //     artistdetail.Prefix = "Prefix" + i;
                //     artistdetail.Title = "Title" + i;
                //     artistdetail.RolesPerformed = "RolesPerformed" + i;
                //     artistdetail.PageSize = 5;
                //     artistInfo.Id = i;
                //     artistInfo.Name = "Artist" + i;
                //     artistsearch.Info = artistInfo;
                //     artistdetail.Info = artistsearch.Info;                     
                //     artists.Add(artistdetail);                    

                // }
                // artistsres.Details=artists;
                ArtistSearchResult artists = _artistService.SearchArtist(searchOption, artistQualifyingCriteria);
                return artists;
            }
            catch (Exception)
            {
                throw;
            }
        }    


        #region "Public Property"

        /// <summary>
        /// Gets or sets the obj contract model.
        /// </summary>
        /// <value>The obj contract model.</value>
        public IArtistModel ObjArtistModel { get; set; }

        /// <summary>
        /// User Information
        /// </summary>
        /// <value>The obj user info.</value>
        public UserInfo ObjUserInfo { get; set; }



        #endregion




    }
}
