
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/31/2021 13:00:08
-- Generated from EDMX file: C:\Users\maja\Desktop\UNI\4\VIII\baze2\Projekat\PostaGUI\DatabaseModel\PostaDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Posta];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PostaRadnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radnici] DROP CONSTRAINT [FK_PostaRadnik];
GO
IF OBJECT_ID(N'[dbo].[FK_SkladistePosta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Skladista] DROP CONSTRAINT [FK_SkladistePosta];
GO
IF OBJECT_ID(N'[dbo].[FK_SkladistePostanskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostanskeUsluge] DROP CONSTRAINT [FK_SkladistePostanskaUsluga];
GO
IF OBJECT_ID(N'[dbo].[FK_PostanskaUslugaSluzbenik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostanskeUsluge] DROP CONSTRAINT [FK_PostanskaUslugaSluzbenik];
GO
IF OBJECT_ID(N'[dbo].[FK_PostanskaUslugaPostar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostanskeUsluge] DROP CONSTRAINT [FK_PostanskaUslugaPostar];
GO
IF OBJECT_ID(N'[dbo].[FK_FinansijskaUslugaSluzbenik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinansijskaUslugas] DROP CONSTRAINT [FK_FinansijskaUslugaSluzbenik];
GO
IF OBJECT_ID(N'[dbo].[FK_Sluzbenik_inherits_Radnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radnici_Sluzbenik] DROP CONSTRAINT [FK_Sluzbenik_inherits_Radnik];
GO
IF OBJECT_ID(N'[dbo].[FK_Postar_inherits_Radnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radnici_Postar] DROP CONSTRAINT [FK_Postar_inherits_Radnik];
GO
IF OBJECT_ID(N'[dbo].[FK_Paket_inherits_PostanskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostanskeUsluge_Paket] DROP CONSTRAINT [FK_Paket_inherits_PostanskaUsluga];
GO
IF OBJECT_ID(N'[dbo].[FK_Pismo_inherits_PostanskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostanskeUsluge_Pismo] DROP CONSTRAINT [FK_Pismo_inherits_PostanskaUsluga];
GO
IF OBJECT_ID(N'[dbo].[FK_Uplatnica_inherits_FinansijskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinansijskaUslugas_Uplatnica] DROP CONSTRAINT [FK_Uplatnica_inherits_FinansijskaUsluga];
GO
IF OBJECT_ID(N'[dbo].[FK_PostNet_inherits_FinansijskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinansijskaUslugas_PostNet] DROP CONSTRAINT [FK_PostNet_inherits_FinansijskaUsluga];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Skladista]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skladista];
GO
IF OBJECT_ID(N'[dbo].[Radnici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radnici];
GO
IF OBJECT_ID(N'[dbo].[Poste]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Poste];
GO
IF OBJECT_ID(N'[dbo].[PostanskeUsluge]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostanskeUsluge];
GO
IF OBJECT_ID(N'[dbo].[FinansijskaUslugas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinansijskaUslugas];
GO
IF OBJECT_ID(N'[dbo].[Radnici_Sluzbenik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radnici_Sluzbenik];
GO
IF OBJECT_ID(N'[dbo].[Radnici_Postar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radnici_Postar];
GO
IF OBJECT_ID(N'[dbo].[PostanskeUsluge_Paket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostanskeUsluge_Paket];
GO
IF OBJECT_ID(N'[dbo].[PostanskeUsluge_Pismo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostanskeUsluge_Pismo];
GO
IF OBJECT_ID(N'[dbo].[FinansijskaUslugas_Uplatnica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinansijskaUslugas_Uplatnica];
GO
IF OBJECT_ID(N'[dbo].[FinansijskaUslugas_PostNet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinansijskaUslugas_PostNet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Skladista'
CREATE TABLE [dbo].[Skladista] (
    [Id_Skladiste] int IDENTITY(1,1) NOT NULL,
    [PostaPostanskiBroj] decimal(18,0)  NULL,
    [Grad] nvarchar(max)  NOT NULL,
    [Ulica] nvarchar(max)  NOT NULL,
    [Broj] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Radnici'
CREATE TABLE [dbo].[Radnici] (
    [JMBG_Radnika] int  NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [PostaPostanskiBroj] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Poste'
CREATE TABLE [dbo].[Poste] (
    [PostanskiBroj] decimal(18,0)  NOT NULL,
    [Grad] nvarchar(max)  NOT NULL,
    [Ulica] nvarchar(max)  NOT NULL,
    [Broj] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PostanskeUsluge'
CREATE TABLE [dbo].[PostanskeUsluge] (
    [ID_Posiljke] int IDENTITY(1,1) NOT NULL,
    [SkladisteId_Skladiste] int  NOT NULL,
    [SluzbenikJMBG_Radnika] int  NOT NULL,
    [SluzbenikPostanskiBroj] decimal(18,0)  NOT NULL,
    [PostarJMBG_Radnika] int  NOT NULL,
    [PostarPostanskiBroj] decimal(18,0)  NOT NULL,
    [PosiljalacIme] nvarchar(max)  NOT NULL,
    [PosiljalacPrezime] nvarchar(max)  NOT NULL,
    [PrimalacIme] nvarchar(max)  NOT NULL,
    [PrimalacPrezime] nvarchar(max)  NOT NULL,
    [Grad] nvarchar(max)  NOT NULL,
    [Ulica] nvarchar(max)  NOT NULL,
    [Broj] nvarchar(max)  NOT NULL,
    [PostanskiBrojOdredista] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'FinansijskaUslugas'
CREATE TABLE [dbo].[FinansijskaUslugas] (
    [ID_Uplate] int IDENTITY(1,1) NOT NULL,
    [SluzbenikJMBG_Radnika] int  NOT NULL,
    [SluzbenikPostanskiBroj] decimal(18,0)  NOT NULL,
    [PrimalacIme] nvarchar(max)  NOT NULL,
    [PrimalacPrezime] nvarchar(max)  NOT NULL,
    [PosiljalacIme] nvarchar(max)  NOT NULL,
    [PosiljalacPrezime] nvarchar(max)  NOT NULL,
    [Grad] nvarchar(max)  NOT NULL,
    [Ulica] nvarchar(max)  NOT NULL,
    [Broj] nvarchar(max)  NOT NULL,
    [Iznos] float  NOT NULL
);
GO

-- Creating table 'Radnici_Sluzbenik'
CREATE TABLE [dbo].[Radnici_Sluzbenik] (
    [Odeljenje] nvarchar(max)  NOT NULL,
    [JMBG_Radnika] int  NOT NULL,
    [PostaPostanskiBroj] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Radnici_Postar'
CREATE TABLE [dbo].[Radnici_Postar] (
    [DeoGrada] nvarchar(max)  NOT NULL,
    [JMBG_Radnika] int  NOT NULL,
    [PostaPostanskiBroj] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'PostanskeUsluge_Paket'
CREATE TABLE [dbo].[PostanskeUsluge_Paket] (
    [Tezina] float  NOT NULL,
    [ID_Posiljke] int  NOT NULL
);
GO

-- Creating table 'PostanskeUsluge_Pismo'
CREATE TABLE [dbo].[PostanskeUsluge_Pismo] (
    [Preporuceno] bit  NOT NULL,
    [ID_Posiljke] int  NOT NULL
);
GO

-- Creating table 'FinansijskaUslugas_Uplatnica'
CREATE TABLE [dbo].[FinansijskaUslugas_Uplatnica] (
    [BrojRacuna] bigint  NOT NULL,
    [ID_Uplate] int  NOT NULL
);
GO

-- Creating table 'FinansijskaUslugas_PostNet'
CREATE TABLE [dbo].[FinansijskaUslugas_PostNet] (
    [BrojTelefona] int  NOT NULL,
    [JMBG_Primaoca] int  NOT NULL,
    [ID_Uplate] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Skladiste] in table 'Skladista'
ALTER TABLE [dbo].[Skladista]
ADD CONSTRAINT [PK_Skladista]
    PRIMARY KEY CLUSTERED ([Id_Skladiste] ASC);
GO

-- Creating primary key on [JMBG_Radnika], [PostaPostanskiBroj] in table 'Radnici'
ALTER TABLE [dbo].[Radnici]
ADD CONSTRAINT [PK_Radnici]
    PRIMARY KEY CLUSTERED ([JMBG_Radnika], [PostaPostanskiBroj] ASC);
GO

-- Creating primary key on [PostanskiBroj] in table 'Poste'
ALTER TABLE [dbo].[Poste]
ADD CONSTRAINT [PK_Poste]
    PRIMARY KEY CLUSTERED ([PostanskiBroj] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'PostanskeUsluge'
ALTER TABLE [dbo].[PostanskeUsluge]
ADD CONSTRAINT [PK_PostanskeUsluge]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'FinansijskaUslugas'
ALTER TABLE [dbo].[FinansijskaUslugas]
ADD CONSTRAINT [PK_FinansijskaUslugas]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- Creating primary key on [JMBG_Radnika], [PostaPostanskiBroj] in table 'Radnici_Sluzbenik'
ALTER TABLE [dbo].[Radnici_Sluzbenik]
ADD CONSTRAINT [PK_Radnici_Sluzbenik]
    PRIMARY KEY CLUSTERED ([JMBG_Radnika], [PostaPostanskiBroj] ASC);
GO

-- Creating primary key on [JMBG_Radnika], [PostaPostanskiBroj] in table 'Radnici_Postar'
ALTER TABLE [dbo].[Radnici_Postar]
ADD CONSTRAINT [PK_Radnici_Postar]
    PRIMARY KEY CLUSTERED ([JMBG_Radnika], [PostaPostanskiBroj] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'PostanskeUsluge_Paket'
ALTER TABLE [dbo].[PostanskeUsluge_Paket]
ADD CONSTRAINT [PK_PostanskeUsluge_Paket]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'PostanskeUsluge_Pismo'
ALTER TABLE [dbo].[PostanskeUsluge_Pismo]
ADD CONSTRAINT [PK_PostanskeUsluge_Pismo]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'FinansijskaUslugas_Uplatnica'
ALTER TABLE [dbo].[FinansijskaUslugas_Uplatnica]
ADD CONSTRAINT [PK_FinansijskaUslugas_Uplatnica]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'FinansijskaUslugas_PostNet'
ALTER TABLE [dbo].[FinansijskaUslugas_PostNet]
ADD CONSTRAINT [PK_FinansijskaUslugas_PostNet]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PostaPostanskiBroj] in table 'Radnici'
ALTER TABLE [dbo].[Radnici]
ADD CONSTRAINT [FK_PostaRadnik]
    FOREIGN KEY ([PostaPostanskiBroj])
    REFERENCES [dbo].[Poste]
        ([PostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostaRadnik'
CREATE INDEX [IX_FK_PostaRadnik]
ON [dbo].[Radnici]
    ([PostaPostanskiBroj]);
GO

-- Creating foreign key on [PostaPostanskiBroj] in table 'Skladista'
ALTER TABLE [dbo].[Skladista]
ADD CONSTRAINT [FK_SkladistePosta]
    FOREIGN KEY ([PostaPostanskiBroj])
    REFERENCES [dbo].[Poste]
        ([PostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkladistePosta'
CREATE INDEX [IX_FK_SkladistePosta]
ON [dbo].[Skladista]
    ([PostaPostanskiBroj]);
GO

-- Creating foreign key on [SkladisteId_Skladiste] in table 'PostanskeUsluge'
ALTER TABLE [dbo].[PostanskeUsluge]
ADD CONSTRAINT [FK_SkladistePostanskaUsluga]
    FOREIGN KEY ([SkladisteId_Skladiste])
    REFERENCES [dbo].[Skladista]
        ([Id_Skladiste])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkladistePostanskaUsluga'
CREATE INDEX [IX_FK_SkladistePostanskaUsluga]
ON [dbo].[PostanskeUsluge]
    ([SkladisteId_Skladiste]);
GO

-- Creating foreign key on [SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj] in table 'PostanskeUsluge'
ALTER TABLE [dbo].[PostanskeUsluge]
ADD CONSTRAINT [FK_PostanskaUslugaSluzbenik]
    FOREIGN KEY ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj])
    REFERENCES [dbo].[Radnici_Sluzbenik]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostanskaUslugaSluzbenik'
CREATE INDEX [IX_FK_PostanskaUslugaSluzbenik]
ON [dbo].[PostanskeUsluge]
    ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj]);
GO

-- Creating foreign key on [PostarJMBG_Radnika], [PostarPostanskiBroj] in table 'PostanskeUsluge'
ALTER TABLE [dbo].[PostanskeUsluge]
ADD CONSTRAINT [FK_PostanskaUslugaPostar]
    FOREIGN KEY ([PostarJMBG_Radnika], [PostarPostanskiBroj])
    REFERENCES [dbo].[Radnici_Postar]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostanskaUslugaPostar'
CREATE INDEX [IX_FK_PostanskaUslugaPostar]
ON [dbo].[PostanskeUsluge]
    ([PostarJMBG_Radnika], [PostarPostanskiBroj]);
GO

-- Creating foreign key on [SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj] in table 'FinansijskaUslugas'
ALTER TABLE [dbo].[FinansijskaUslugas]
ADD CONSTRAINT [FK_FinansijskaUslugaSluzbenik]
    FOREIGN KEY ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj])
    REFERENCES [dbo].[Radnici_Sluzbenik]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinansijskaUslugaSluzbenik'
CREATE INDEX [IX_FK_FinansijskaUslugaSluzbenik]
ON [dbo].[FinansijskaUslugas]
    ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj]);
GO

-- Creating foreign key on [JMBG_Radnika], [PostaPostanskiBroj] in table 'Radnici_Sluzbenik'
ALTER TABLE [dbo].[Radnici_Sluzbenik]
ADD CONSTRAINT [FK_Sluzbenik_inherits_Radnik]
    FOREIGN KEY ([JMBG_Radnika], [PostaPostanskiBroj])
    REFERENCES [dbo].[Radnici]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JMBG_Radnika], [PostaPostanskiBroj] in table 'Radnici_Postar'
ALTER TABLE [dbo].[Radnici_Postar]
ADD CONSTRAINT [FK_Postar_inherits_Radnik]
    FOREIGN KEY ([JMBG_Radnika], [PostaPostanskiBroj])
    REFERENCES [dbo].[Radnici]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Posiljke] in table 'PostanskeUsluge_Paket'
ALTER TABLE [dbo].[PostanskeUsluge_Paket]
ADD CONSTRAINT [FK_Paket_inherits_PostanskaUsluga]
    FOREIGN KEY ([ID_Posiljke])
    REFERENCES [dbo].[PostanskeUsluge]
        ([ID_Posiljke])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Posiljke] in table 'PostanskeUsluge_Pismo'
ALTER TABLE [dbo].[PostanskeUsluge_Pismo]
ADD CONSTRAINT [FK_Pismo_inherits_PostanskaUsluga]
    FOREIGN KEY ([ID_Posiljke])
    REFERENCES [dbo].[PostanskeUsluge]
        ([ID_Posiljke])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Uplate] in table 'FinansijskaUslugas_Uplatnica'
ALTER TABLE [dbo].[FinansijskaUslugas_Uplatnica]
ADD CONSTRAINT [FK_Uplatnica_inherits_FinansijskaUsluga]
    FOREIGN KEY ([ID_Uplate])
    REFERENCES [dbo].[FinansijskaUslugas]
        ([ID_Uplate])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Uplate] in table 'FinansijskaUslugas_PostNet'
ALTER TABLE [dbo].[FinansijskaUslugas_PostNet]
ADD CONSTRAINT [FK_PostNet_inherits_FinansijskaUsluga]
    FOREIGN KEY ([ID_Uplate])
    REFERENCES [dbo].[FinansijskaUslugas]
        ([ID_Uplate])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------