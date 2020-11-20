Create procedure spRegisterUser
@EmailID varchar(255),
@Password varchar(255),
@UserName varchar(255),
@RegistrationDate datetime
as
begin
if not exists (select EmailID from RegisteredUsers where EmailID = @EmailID)
begin
	insert into RegisteredUsers(EmailID,Password,UserName,RegistrationDate)
	values (@EmailID , @Password , @UserName, @RegistrationDate);

	select * from RegisteredUsers where EmailID = @EmailID
end
end