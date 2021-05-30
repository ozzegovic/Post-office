
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2021 19:02:11
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

IF OBJECT_ID(N'[dbo].[FK_PostarRadnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Postari] DROP CONSTRAINT [FK_PostarRadnik];
GO
IF OBJECT_ID(N'[dbo].[FK_PostaRadnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radnici] DROP CONSTRAINT [FK_PostaRadnik];
GO
IF OBJECT_ID(N'[dbo].[FK_RadnikSluzbenik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sluzbenici] DROP CONSTRAINT [FK_RadnikSluzbenik];
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
IF OBJECT_ID(N'[dbo].[FK_PostanskaUslugaPaket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pakets] DROP CONSTRAINT [FK_PostanskaUslugaPaket];
GO
IF OBJECT_ID(N'[dbo].[FK_PismoPostanskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pismoes] DROP CONSTRAINT [FK_PismoPostanskaUsluga];
GO
IF OBJECT_ID(N'[dbo].[FK_FinansijskaUslugaSluzbenik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinansijskaUslugas] DROP CONSTRAINT [FK_FinansijskaUslugaSluzbenik];
GO
IF OBJECT_ID(N'[dbo].[FK_FinansijskaUslugaUplatnica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Uplatnicas] DROP CONSTRAINT [FK_FinansijskaUslugaUplatnica];
GO
IF OBJECT_ID(N'[dbo].[FK_PostNetFinansijskaUsluga]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostNets] DROP CONSTRAINT [FK_PostNetFinansijskaUsluga];
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
IF OBJECT_ID(N'[dbo].[Postari]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Postari];
GO
IF OBJECT_ID(N'[dbo].[Poste]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Poste];
GO
IF OBJECT_ID(N'[dbo].[Sluzbenici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sluzbenici];
GO
IF OBJECT_ID(N'[dbo].[PostanskeUsluge]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostanskeUsluge];
GO
IF OBJECT_ID(N'[dbo].[Pakets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pakets];
GO
IF OBJECT_ID(N'[dbo].[Pismoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pismoes];
GO
IF OBJECT_ID(N'[dbo].[FinansijskaUslugas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinansijskaUslugas];
GO
IF OBJECT_ID(N'[dbo].[Uplatnicas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uplatnicas];
GO
IF OBJECT_ID(N'[dbo].[PostNets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostNets];
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

-- Creating table 'Postari'
CREATE TABLE [dbo].[Postari] (
    [JMBG_Radnika] int  NOT NULL,
    [PostanskiBroj] decimal(18,0)  NOT NULL,
    [DeoGrada] nvarchar(max)  NOT NULL
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

-- Creating table 'Sluzbenici'
CREATE TABLE [dbo].[Sluzbenici] (
    [JMBG_Radnika] int  NOT NULL,
    [PostanskiBroj] decimal(18,0)  NOT NULL,
    [Odeljenje] nvarchar(max)  NOT NULL
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

-- Creating table 'Pakets'
CREATE TABLE [dbo].[Pakets] (
    [ID_Posiljke] int IDENTITY(1,1) NOT NULL,
    [Tezina] float  NOT NULL
);
GO

-- Creating table 'Pismoes'
CREATE TABLE [dbo].[Pismoes] (
    [ID_Posiljke] int IDENTITY(1,1) NOT NULL,
    [Preporuceno] bit  NOT NULL
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

-- Creating table 'Uplatnicas'
CREATE TABLE [dbo].[Uplatnicas] (
    [ID_Uplate] int IDENTITY(1,1) NOT NULL,
    [BrojRacuna] bigint  NOT NULL
);
GO

-- Creating table 'PostNets'
CREATE TABLE [dbo].[PostNets] (
    [ID_Uplate] int IDENTITY(1,1) NOT NULL,
    [BrojTelefona] int  NOT NULL,
    [JMBG_Primaoca] int  NOT NULL
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

-- Creating primary key on [JMBG_Radnika], [PostanskiBroj] in table 'Postari'
ALTER TABLE [dbo].[Postari]
ADD CONSTRAINT [PK_Postari]
    PRIMARY KEY CLUSTERED ([JMBG_Radnika], [PostanskiBroj] ASC);
GO

-- Creating primary key on [PostanskiBroj] in table 'Poste'
ALTER TABLE [dbo].[Poste]
ADD CONSTRAINT [PK_Poste]
    PRIMARY KEY CLUSTERED ([PostanskiBroj] ASC);
GO

-- Creating primary key on [JMBG_Radnika], [PostanskiBroj] in table 'Sluzbenici'
ALTER TABLE [dbo].[Sluzbenici]
ADD CONSTRAINT [PK_Sluzbenici]
    PRIMARY KEY CLUSTERED ([JMBG_Radnika], [PostanskiBroj] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'PostanskeUsluge'
ALTER TABLE [dbo].[PostanskeUsluge]
ADD CONSTRAINT [PK_PostanskeUsluge]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'Pakets'
ALTER TABLE [dbo].[Pakets]
ADD CONSTRAINT [PK_Pakets]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Posiljke] in table 'Pismoes'
ALTER TABLE [dbo].[Pismoes]
ADD CONSTRAINT [PK_Pismoes]
    PRIMARY KEY CLUSTERED ([ID_Posiljke] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'FinansijskaUslugas'
ALTER TABLE [dbo].[FinansijskaUslugas]
ADD CONSTRAINT [PK_FinansijskaUslugas]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'Uplatnicas'
ALTER TABLE [dbo].[Uplatnicas]
ADD CONSTRAINT [PK_Uplatnicas]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- Creating primary key on [ID_Uplate] in table 'PostNets'
ALTER TABLE [dbo].[PostNets]
ADD CONSTRAINT [PK_PostNets]
    PRIMARY KEY CLUSTERED ([ID_Uplate] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JMBG_Radnika], [PostanskiBroj] in table 'Postari'
ALTER TABLE [dbo].[Postari]
ADD CONSTRAINT [FK_PostarRadnik]
    FOREIGN KEY ([JMBG_Radnika], [PostanskiBroj])
    REFERENCES [dbo].[Radnici]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

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

-- Creating foreign key on [JMBG_Radnika], [PostanskiBroj] in table 'Sluzbenici'
ALTER TABLE [dbo].[Sluzbenici]
ADD CONSTRAINT [FK_RadnikSluzbenik]
    FOREIGN KEY ([JMBG_Radnika], [PostanskiBroj])
    REFERENCES [dbo].[Radnici]
        ([JMBG_Radnika], [PostaPostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
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
    REFERENCES [dbo].[Sluzbenici]
        ([JMBG_Radnika], [PostanskiBroj])
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
    REFERENCES [dbo].[Postari]
        ([JMBG_Radnika], [PostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostanskaUslugaPostar'
CREATE INDEX [IX_FK_PostanskaUslugaPostar]
ON [dbo].[PostanskeUsluge]
    ([PostarJMBG_Radnika], [PostarPostanskiBroj]);
GO

-- Creating foreign key on [ID_Posiljke] in table 'Pakets'
ALTER TABLE [dbo].[Pakets]
ADD CONSTRAINT [FK_PostanskaUslugaPaket]
    FOREIGN KEY ([ID_Posiljke])
    REFERENCES [dbo].[PostanskeUsluge]
        ([ID_Posiljke])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Posiljke] in table 'Pismoes'
ALTER TABLE [dbo].[Pismoes]
ADD CONSTRAINT [FK_PismoPostanskaUsluga]
    FOREIGN KEY ([ID_Posiljke])
    REFERENCES [dbo].[PostanskeUsluge]
        ([ID_Posiljke])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj] in table 'FinansijskaUslugas'
ALTER TABLE [dbo].[FinansijskaUslugas]
ADD CONSTRAINT [FK_FinansijskaUslugaSluzbenik]
    FOREIGN KEY ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj])
    REFERENCES [dbo].[Sluzbenici]
        ([JMBG_Radnika], [PostanskiBroj])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinansijskaUslugaSluzbenik'
CREATE INDEX [IX_FK_FinansijskaUslugaSluzbenik]
ON [dbo].[FinansijskaUslugas]
    ([SluzbenikJMBG_Radnika], [SluzbenikPostanskiBroj]);
GO

-- Creating foreign key on [ID_Uplate] in table 'Uplatnicas'
ALTER TABLE [dbo].[Uplatnicas]
ADD CONSTRAINT [FK_FinansijskaUslugaUplatnica]
    FOREIGN KEY ([ID_Uplate])
    REFERENCES [dbo].[FinansijskaUslugas]
        ([ID_Uplate])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Uplate] in table 'PostNets'
ALTER TABLE [dbo].[PostNets]
ADD CONSTRAINT [FK_PostNetFinansijskaUsluga]
    FOREIGN KEY ([ID_Uplate])
    REFERENCES [dbo].[FinansijskaUslugas]
        ([ID_Uplate])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------