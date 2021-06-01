 

Select * from [dbo].[SortiranjePosiljkiPoTezini]()

Select * from [dbo].[SortiranjeUplata]()

Select * from [dbo].[NadjiPostara]()


Exec PostariOdredista @postanski_broj = 21001;

Exec IspisiPreporucena @preporuceno = 0;


