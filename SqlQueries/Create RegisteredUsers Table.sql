CREATE TABLE RegisteredUsers
(
  ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  EmailID varchar(50),
  Password varchar(50),
  UserName varchar(50),
  RegistrationDate datetime,
);
select * from RegisteredUsers