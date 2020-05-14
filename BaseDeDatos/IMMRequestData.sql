USE [IMMRequest]
GO
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Name]) VALUES (1, N'Transporte')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (2, N'Espacios publicos y calles')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (3, N'Limpieza')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (4, N'Saneamiento')
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (1, 1, N'Acoso Sexual')
SET IDENTITY_INSERT [dbo].[Topics] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Email], [PhoneNumber], [Discriminator], [Password], [Token]) VALUES (1, N'Admin Foo', N'admin@foo.com', NULL, N'Admin', N'pass', N'51b95fa3-5529-4362-8111-48f898f91454')
INSERT [dbo].[User] ([Id], [Name], [Email], [PhoneNumber], [Discriminator], [Password], [Token]) VALUES (2, N'Gian Carlo', N'citizen@email.com', N'5555-555-555', N'Citizen', NULL, NULL)
INSERT [dbo].[User] ([Id], [Name], [Email], [PhoneNumber], [Discriminator], [Password], [Token]) VALUES (3, N'citizen 8', N'email8@citizen.com', N'9999-888', N'Citizen', NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Types] ON 

INSERT [dbo].[Types] ([Id], [TopicId], [Name], [IsActive]) VALUES (1, 1, N'Taxi - Acoso', 1)
INSERT [dbo].[Types] ([Id], [TopicId], [Name], [IsActive]) VALUES (2, 1, N'newtype', 1)
INSERT [dbo].[Types] ([Id], [TopicId], [Name], [IsActive]) VALUES (3, 1, N'newtype', 1)
INSERT [dbo].[Types] ([Id], [TopicId], [Name], [IsActive]) VALUES (4, 1, N'newtype', 1)
INSERT [dbo].[Types] ([Id], [TopicId], [Name], [IsActive]) VALUES (5, 1, N'newtype', 1)
SET IDENTITY_INSERT [dbo].[Types] OFF
SET IDENTITY_INSERT [dbo].[Requests] ON 

INSERT [dbo].[Requests] ([Id], [CitizenId], [TypeId], [Details]) VALUES (1, 2, 1, N'pedido')
INSERT [dbo].[Requests] ([Id], [CitizenId], [TypeId], [Details]) VALUES (2, 3, 1, N'creation details')
INSERT [dbo].[Requests] ([Id], [CitizenId], [TypeId], [Details]) VALUES (3, 3, 1, N'creation details')
SET IDENTITY_INSERT [dbo].[Requests] OFF
SET IDENTITY_INSERT [dbo].[AdditionalField] ON 

INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (1, N'Matricula', 1, 0, 1, N'TextField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (2, N'Fecha y hora', 2, 0, 1, N'DateField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (3, N'Nro de Movil', 0, 1, 1, N'IntegerField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (4, NULL, 2, 1, 2, N'IntegerField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (5, NULL, 2, 1, 4, N'IntegerField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (6, N'campo', 2, 1, 5, N'IntegerField')
INSERT [dbo].[AdditionalField] ([Id], [Name], [FieldType], [IsRequired], [TypeId], [Discriminator]) VALUES (7, N'campo', 2, 1, 5, N'IntegerField')
SET IDENTITY_INSERT [dbo].[AdditionalField] OFF
SET IDENTITY_INSERT [dbo].[DateRangeItems] ON 

INSERT [dbo].[DateRangeItems] ([Id], [Value], [DateFieldId]) VALUES (1, CAST(N'2010-05-11 00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[DateRangeItems] ([Id], [Value], [DateFieldId]) VALUES (2, CAST(N'2030-05-11 00:00:00.0000000' AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[DateRangeItems] OFF
SET IDENTITY_INSERT [dbo].[IntegerRangeItems] ON 

INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (1, 0, 3)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (2, 99999999, 3)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (3, 6, 4)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (4, 7, 4)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (5, 6, 5)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (6, 7, 5)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (7, 6, 6)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (8, 7, 6)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (9, 6, 7)
INSERT [dbo].[IntegerRangeItems] ([Id], [Value], [IntegerFieldId]) VALUES (10, 7, 7)
SET IDENTITY_INSERT [dbo].[IntegerRangeItems] OFF
SET IDENTITY_INSERT [dbo].[RequestField] ON 

INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (1, 1, N'Fecha y hora', CAST(N'2020-05-09 00:00:00.0000000' AS DateTime2), NULL, N'DateRequestField', NULL)
INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (2, 1, N'Nro de Movil', NULL, 111111, N'IntRequestField', NULL)
INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (3, 2, N'Fecha y hora', CAST(N'2020-05-09 00:00:00.0000000' AS DateTime2), NULL, N'DateRequestField', NULL)
INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (4, 2, N'Nro de Movil', NULL, 888998, N'IntRequestField', NULL)
INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (5, 3, N'Fecha y hora', CAST(N'2020-05-09 00:00:00.0000000' AS DateTime2), NULL, N'DateRequestField', NULL)
INSERT [dbo].[RequestField] ([Id], [requestId], [Name], [Value], [IntRequestField_Value], [Discriminator], [TextRequestField_Value]) VALUES (6, 3, N'Nro de Movil', NULL, 888998, N'IntRequestField', NULL)
SET IDENTITY_INSERT [dbo].[RequestField] OFF
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([Id], [Description], [Discriminator], [RequestId]) VALUES (1, N'This request has been created', N'CreatedState', 1)
INSERT [dbo].[State] ([Id], [Description], [Discriminator], [RequestId]) VALUES (2, N'This request has been created', N'CreatedState', 2)
INSERT [dbo].[State] ([Id], [Description], [Discriminator], [RequestId]) VALUES (3, N'This request has been created', N'CreatedState', 3)
SET IDENTITY_INSERT [dbo].[State] OFF
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200425152543_hola', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200425162209_masmigraciones', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200425165033_stateperrequest', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200428230420_state', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200507230646_Token', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200509153537_IsActiveColumn', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200509160841_CollectionsChange', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200509224146_mail', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200510220442_update', N'3.1.3')
