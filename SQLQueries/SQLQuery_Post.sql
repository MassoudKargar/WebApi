SELECT P.[Id],[Title],[Description],[CategoryId],C.[Name],[AuthorId],U.[UserName]
FROM Posts as P
INNER JOIN Categories as C on p.CategoryId = C.Id
INNER JOIN AspNetUsers as U on P.AuthorId = U.Id