using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessengerClient.Constant
{
    class Constants
    {
        #region connection constants

        public const int PORT = 12345;
        public const string IP = "192.168.0.149";

        public const string DATABASE_FILENAME = "local_message_db.db";

        public const string DEFAULT_RECEIVER_TEXT = "Receiver not selected";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DATABASE_FILENAME);
            }
        }
        #endregion

        #region view constants

        public const string SENDED_MESSAGE_COLOR = "000";
        public const string RECEIVED_MESSAGE_COLOR = "222";

        public const string MESSAGE_DATE_FORMAT = "d MMM, HH:mm";

        public const string RM_CONVER = "Remove conversation";
        public const string DEL_MESS = "Delete messages";
        public const string DEL_CONVER = "Delete conversation and messages";

        #endregion
    }
}
