ALTER PROCEDURE [dbo].[IspisiPreporucena]
	@preporuceno   bit,

	@numrows AS INT = 0 OUTPUT
AS
declare
@result TABLE
(
      id_posiljke int not null,
	  imePrimalac nvarchar(max),
	  prezimePrimalac nvarchar(max),
	  gradPosta nvarchar(max) not null
	  
);
declare
@i int,
@j nvarchar(max);

DECLARE PISMO_CURSOR CURSOR
FOR SELECT id_posiljke, gradPosta FROM @result
begin
	INSERT INTO @result(id_posiljke, imePrimalac, prezimePrimalac, gradPosta) SELECT pu.id_posiljke, pu.PrimalacIme, pu.primalacPrezime, posta.Grad
	FROM PostanskeUsluge pu
	INNER JOIN PostanskeUsluge_Pismo pismo ON ((pu.ID_Posiljke = pismo.ID_Posiljke) and (pismo.Preporuceno = @preporuceno))
	INNER JOIN Poste posta ON( pu.PostarPostanskiBroj = posta.PostanskiBroj)
	OPEN PISMO_CURSOR;
	FETCH NEXT FROM PISMO_CURSOR
	INTO @i, @j;
	WHILE @@FETCH_STATUS = 0
		BEGIN
		if (@preporuceno = 1)
			print 'Preporuceno pismo sa id: ' + CAST(@i AS VARCHAR(10)) + ' dostavlja postar koji radi u posti iz grada:' + @j;
		else
			print 'Pismo sa id: ' + CAST(@i AS VARCHAR(10)) + ' dostavlja postar koji radi u posti iz grada:' + @j;

			FETCH NEXT FROM PISMO_CURSOR
			INTO @i, @j;
		END;
CLOSE PISMO_CURSOR; 

end
--ispisujem id posiljke pisma (preporuceno/ne) nakon cega se ispisuju detalji poste u kojoj postar koji dostavlja to pismo radi