select a.id, a.fullname, a.Handicap, b.fullName, b.Handicap, a.Handicap - b.Handicap
From person a, person b
where a.id <> b.id and a.Handicap - b.Handicap >= 0
order by a.fullname, b.fullName