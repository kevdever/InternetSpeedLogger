IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_ResultReport]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vw_ResultReport] as 
select top 20 download/1000000 as dl_mbit, upload/1000000 as ul_mbit, timestamp 
from [dbo].[results] 
order by timestamp desc;
