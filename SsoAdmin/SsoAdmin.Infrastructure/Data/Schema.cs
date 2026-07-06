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
        """;
}
