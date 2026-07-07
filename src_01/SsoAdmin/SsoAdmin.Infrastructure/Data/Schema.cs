namespace SsoAdmin.Infrastructure.Data;

internal static class Schema
{
    internal const string Sql = """
        CREATE TABLE IF NOT EXISTS Users (
            Id       INTEGER PRIMARY KEY AUTOINCREMENT,
            Name     TEXT    NOT NULL,
            IsActive INTEGER NOT NULL DEFAULT 1
        );

        CREATE TABLE IF NOT EXISTS Credentials (
            Id       INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId   INTEGER NOT NULL REFERENCES Users(Id),
            Username TEXT    NOT NULL,
            Emisor   TEXT    NOT NULL,
            UNIQUE(Username, Emisor)
        );

        CREATE TABLE IF NOT EXISTS Applications (
            Id   INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Url  TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Permissions (
            Id            INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId        INTEGER NOT NULL REFERENCES Users(Id),
            ApplicationId INTEGER NOT NULL REFERENCES Applications(Id),
            FechaDesde    TEXT    NOT NULL,
            FechaHasta    TEXT
        );

        CREATE INDEX IF NOT EXISTS IX_Permissions_UserId_ApplicationId
            ON Permissions(UserId, ApplicationId);

        CREATE TABLE IF NOT EXISTS Login (
            Id           INTEGER PRIMARY KEY AUTOINCREMENT,
            Username     TEXT    NOT NULL UNIQUE,
            PasswordHash TEXT    NOT NULL
        );

        INSERT OR IGNORE INTO Login (Username, PasswordHash)
        VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918');
        """;
}
