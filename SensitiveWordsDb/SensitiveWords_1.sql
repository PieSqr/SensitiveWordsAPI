﻿CREATE TABLE dbo.SensitiveWords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Word NVARCHAR(100) NOT NULL UNIQUE
);
GO