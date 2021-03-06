﻿--{0} = table
--{1} = column
--{2} = scramble type
--{3} = Identifier
--{4} = Seed
--{5} = Schema


DECLARE @numberOfValues int;
SET @numberOfValues = (select count(1) from #ScrambleData where type = '{2}');

--Order by / top is needed for performance reasons (query optimizer is not smart enough)
--We assume the table is an entity table and has an id column
with TableWithRandomNumber as (
	select 
		top( select count(1) from [{5}].[{0}] ) 
			[{5}].[{0}].*, 
			([{3}] + {4}) % @numberOfValues RandomNumber
	from [{5}].[{0}]
	order by  RandomNumber
)

update [{5}].[{0}]
set [{1}] = #ScrambleData.value
from TableWithRandomNumber
inner join [{5}].[{0}] on [{5}].[{0}].[{3}] = TableWithRandomNumber.[{3}]
inner join #ScrambleData on #ScrambleData.ValueIndex = TableWithRandomNumber.RandomNumber
where [{5}].[{0}].[{1}] is not null and #ScrambleData.type = '{2}'