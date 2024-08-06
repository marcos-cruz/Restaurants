SELECT TOP (1000)
  [Id]
      , [Name]
      , [NormalizedName]
      , [ConcurrencyStamp]
FROM [RestaurantsDb].[dbo].[AspNetRoles]

UPDATE [RestaurantsDb].[dbo].[AspNetRoles]
  SET NormalizedName = UPPER([Name])