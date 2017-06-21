
create Table Book
(
	 id int Primary Key Identity(1,1),
	 Name nvarchar(max),
	 Author nvarchar(max),
	 Year int,
	   
); 

create Table LibraryUser
(
	Id int Primary Key Identity (1,1),
	Name nvarchar(max),
	Age int 
);

create Table BookLibraryUser
(
	BookId int,
	LibraryUserId int, 
	Primary Key(BookId, LibraryUserId),
	Foreign Key (BookId) references Book(Id),
	Foreign Key (LibraryUserID) references LibraryUser(Id) 
);

insert into Book(Name, Author, Year) values ('Richetr', 'CSharp', 2015);

insert into Book(Name, Author, Year) values ('Author1', 'Book1', 2013);

insert into Book(Name, Author, Year) values ('Author2', 'Book2', 1995);