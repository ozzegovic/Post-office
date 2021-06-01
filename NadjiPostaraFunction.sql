CREATE FUNCTION [dbo].[NadjiPostara]()
RETURNS @returntable TABLE
(
	 jmbg_postara   int,
	 broj_paketa    int
)
AS
BEGIN
	INSERT INTO @returntable
	SELECT radnik.JMBG_Radnika, count(paket.ID_Posiljke)
	FROM Radnici radnik
	INNER JOIN PostanskeUsluge pu ON (radnik.JMBG_Radnika = pu.PostarJMBG_Radnika)
	INNER JOIN PostanskeUsluge_Paket paket ON (paket.ID_Posiljke = pu.ID_Posiljke)	
	group by radnik.JMBG_Radnika
	RETURN
END
--koliko paketa ima svaki postar, malo nesrecno nazvala ali ok