USE [Posta]
GO
/****** Object:  Trigger [dbo].[PrevelikaUplata]    Script Date: 6/1/2021 1:52:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[PrevelikaUplata]
	ON [dbo].[FinansijskaUslugas]
	FOR INSERT
	AS

	declare @id_uplate  as int 
    declare @iznos as float 
	
	declare @sluzbenikjmbgradnika as int 
    declare @sluzbenikPB as decimal(18,0) 
	declare @primalacIme  as nvarchar(max) 
	declare @primalacPrezime  as nvarchar(max)
	declare @posiljalacIme  as nvarchar(max) 
	declare @posiljalacPrezime  as nvarchar(max) 
	declare @grad  as nvarchar(max) 
	declare @ulica  as nvarchar(max) 
	declare @broj  as nvarchar(max) 

	BEGIN
		IF TRIGGER_NESTLEVEL() > 1 RETURN
		set @id_uplate = (select ID_Uplate from inserted)
		set @iznos = (select Iznos from inserted)
		set @sluzbenikjmbgradnika = (select SluzbenikJMBG_Radnika from inserted)
		set @sluzbenikPB = (select SluzbenikPostanskiBroj from inserted)
		set @primalacIme = (select PrimalacIme from inserted)
		set @primalacPrezime = (select PrimalacPrezime from inserted)
		set @posiljalacIme = (select PosiljalacIme from inserted)
		set @posiljalacPrezime = (select PosiljalacPrezime from inserted)
		set @grad = (select Grad from inserted)
		set @ulica = (select Ulica from inserted)
		set @broj = (select Broj from inserted)
		if exists(
			select usluga.ID_Uplate
			from FinansijskaUslugas usluga
			where @iznos > 10000 		
		)
	BEGIN
			RAISERROR ('Nije dozvoljeno slanje vise od 10000 dinara!', 16,  1)
		END

		ELSE
		BEGIN
			INSERT INTO FinansijskaUslugas( SluzbenikJMBG_Radnika,  SluzbenikPostanskiBroj, PrimalacIme, PrimalacPrezime, PosiljalacIme,
			PosiljalacPrezime, Grad, Ulica, Broj, Iznos)
			SELECT SluzbenikJMBG_Radnika,SluzbenikPostanskiBroj, PrimalacIme, PrimalacPrezime, PosiljalacIme,PosiljalacPrezime, Grad, Ulica, Broj, Iznos FROM inserted	
		END

	END

