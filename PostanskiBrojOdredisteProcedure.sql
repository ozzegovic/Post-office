ALTER PROCEDURE [dbo].[PostariOdredista]
	@postanski_broj   int,

	@numrows AS INT = 0 OUTPUT
AS
declare
@result TABLE
(
      id_posiljke int not null,
	  imePostara nvarchar(max),
	  prezimePostara nvarchar(max),
	  idPostara int not null
	  
);
declare
@i int,
@j int;

DECLARE PISMO_CURSOR CURSOR
FOR SELECT id_posiljke, idPostara FROM @result
begin
	INSERT INTO @result(id_posiljke, imePostara, prezimePostara, idPostara) SELECT pu.id_posiljke, radnik.Ime, radnik.Prezime, radnik.JMBG_Radnika
	FROM Radnici radnik
	INNER JOIN PostanskeUsluge pu ON ((pu.PostanskiBrojOdredista = @postanski_broj))
	--INNER JOIN PostanskeUsluge ppu ON(pu.PostarJMBG_Radnika = ppu.PostarJMBG_Radnika)
	INNER JOIN Radnici_Postar postar ON ((radnik.JMBG_Radnika = postar.JMBG_Radnika) and (radnik.PostaPostanskiBroj = @postanski_broj))

	OPEN PISMO_CURSOR;
	FETCH NEXT FROM PISMO_CURSOR
	INTO @i, @j;
	WHILE @@FETCH_STATUS = 0
		BEGIN
			print 'Posiljku sa id:' + CAST(@i AS VARCHAR(10)) + ' moze da isporucuje postar sa brojem ' + + CAST(@j AS VARCHAR(10));
			FETCH NEXT FROM PISMO_CURSOR
			INTO @i, @j;
		END;
CLOSE PISMO_CURSOR; 

end
--ispisujem id posiljke u kojoj radnik koji je postar koji ima postanski broj (zaposlen u posti sa postanskim brojem)
--koji je isti kao i postanski broj odredista koji prosledjujem 
--(u principu koji postar bi trebao da isporucuje odredjene posiljke sa postanskim brojem - id te posiljke)
--tehnicki se moze dodeliti bilo koji postar da radi dostavu