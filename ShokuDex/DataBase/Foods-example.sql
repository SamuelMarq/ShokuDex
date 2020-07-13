USE ShokuDex

INSERT INTO Foods
           ([Id],[CreatedAt],[UpdatedAt],[IsDeleted],[Name],[Description],[Fats],[Carbohydrates],[Protein],[Alcohol],[Calories],[Portion],[Photo],[IsRecipe],[ProfileId],[CategoryId]) VALUES
           (NEWID(), GETDATE(), GETDATE(), 0, 'NomeDaComida', '', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, '' , 0, '00000000-0000-0000-0000-000000000001', 'id da categoria'),
           (NEWID(), GETDATE(), GETDATE(), 0, 'NomeDaComida', '', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, '' , 0, '00000000-0000-0000-0000-000000000001', 'id da categoria'),
           (NEWID(), GETDATE(), GETDATE(), 0, 'NomeDaComida', '', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, '' , 0, '00000000-0000-0000-0000-000000000001', 'id da categoria'),
           (NEWID(), GETDATE(), GETDATE(), 0, 'NomeDaComida', '', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, '' , 0, '00000000-0000-0000-0000-000000000001', 'id da categoria'),
           (NEWID(), GETDATE(), GETDATE(), 0, 'NomeDaComida', '', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, '' , 0, '00000000-0000-0000-0000-000000000001', 'id da categoria');