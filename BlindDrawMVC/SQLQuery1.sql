select * from GamesById
select * from Players

select top 4 g.pid2 from players p
left outer join gamesbyid g on p.id = g.pid1
where p.id = 2
order by g.gamecount