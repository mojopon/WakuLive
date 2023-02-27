using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain.Twitch.Utility
{
    internal static class TwitchLibUtilities
    {
        public static UserType ConvertUserType(TwitchLib.Client.Enums.UserType userType)
        {
            switch (userType)
            {
                case TwitchLib.Client.Enums.UserType.Viewer:
                    {
                        return UserType.Viewer;
                    }
                case TwitchLib.Client.Enums.UserType.Admin:
                    {
                        return UserType.Admin;
                    }
                case TwitchLib.Client.Enums.UserType.Broadcaster:
                    {
                        return UserType.Broadcaster;
                    }
                case TwitchLib.Client.Enums.UserType.Staff:
                    {
                        return UserType.Staff;
                    }
                case TwitchLib.Client.Enums.UserType.Moderator:
                    {
                        return UserType.Moderator;
                    }
                case TwitchLib.Client.Enums.UserType.GlobalModerator:
                    {
                        return UserType.GlobalModerator;
                    }
                default:
                    {
                        return UserType.Viewer;
                    }
            }
        }
    }
}
