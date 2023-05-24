﻿public static class Constants
    {
        public const string DatabaseFilename = "ZLMClaims.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

       // public static string DatabasePath =>
       //     Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static string DatabasePath
    {
        get
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, DatabaseFilename);
        }
    }
}