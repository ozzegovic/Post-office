ALTER FUNCTION [dbo].[SortiranjePosiljkiPoTezini]()
RETURNS @returntable TABLE
(
	 postanski_broj   int,
	 tezina_paketa    int,
	 id_posiljke	  int
)
AS
BEGIN
	INSERT INTO @returntable
	SELECT p.PostanskiBroj, paket.Tezina, pu.ID_posiljke
	FROM PostanskeUsluge_Paket paket
	INNER JOIN PostanskeUsluge pu ON (paket.ID_Posiljke = pu.ID_Posiljke)
	INNER JOIN Skladista skl ON (pu.SkladisteId_Skladiste = skl.Id_Skladiste)
	INNER JOIN Poste p ON (skl.PostaPostanskiBroj = p.PostanskiBroj)
	order by paket.Tezina desc 
	
	RETURN
END
