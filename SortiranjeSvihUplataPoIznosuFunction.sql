ALTER FUNCTION [dbo].[SortiranjeUplata]()
RETURNS @returntable TABLE
(
	 postanski_broj   int,
	 iznos		      int,
	 id_uplate   	  int
)
AS
BEGIN
	INSERT INTO @returntable
	SELECT p.PostanskiBroj, fu.Iznos, uplatnica.ID_Uplate
	FROM FinansijskaUslugas uplatnica
	INNER JOIN FinansijskaUslugas fu ON (uplatnica.ID_Uplate = fu.ID_Uplate)
	INNER JOIN Poste p ON (p.PostanskiBroj = fu.SluzbenikPostanskiBroj)
	order by fu.Iznos desc 
	
	RETURN
END
