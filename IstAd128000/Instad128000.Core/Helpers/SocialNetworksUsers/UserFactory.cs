using Instad128000.Core.Common.Enums;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Helpers.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Helpers.SocialNetworksUsers
{
    public static class UserFactory
    {
        private static InstagramUser _instaUser { get; set; }
        public static InstagramUser Insta
        {
            get { return _instaUser; }
        }

        public static ISocialNetworkUser Init(SocialUserType type, string username, string password, IRequestService reqRes, IDataStringService dataStrSrv)
        {
            switch (type)
            {
                case SocialUserType.Instagram:
                    return _instaUser ?? (_instaUser = new InstagramUser(Properties.Settings.Default.InstaClientKey,
                Properties.Settings.Default.InstaClientId, Driver.PhantomInstance,username,password, reqRes, dataStrSrv));
                default:
                    throw new Exception("Unknown user type");
            }
        }
    }
}
