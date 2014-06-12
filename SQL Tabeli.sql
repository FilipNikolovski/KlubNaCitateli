
CREATE TABLE Books (
	IDBook bigint NOT NULL AUTO_INCREMENT,
	ISBN varchar(100) NOT NULL,
	Name varchar(200) NOT NULL,
	Description text,
	CoverLink varchar(1000),
	Thumbnail varchar(1000),
	YearPublished int,
	Language varchar(150),
	SumRating int,
	NumVotes int,
	DateAdded varchar(20),
	PRIMARY KEY (IDBook)
);

CREATE TABLE Authors (
	IDAuthor int NOT NULL AUTO_INCREMENT,
	Name varchar(150) NOT NULL,
	PRIMARY KEY (IDAuthor)
);

CREATE TABLE Tags (
	IDTag int NOT NULL AUTO_INCREMENT,
	Name varchar(200) NOT NULL,
	PRIMARY KEY (IDTag)
);

CREATE TABLE Categories (
	IDCategory int NOT NULL AUTO_INCREMENT,
	Name varchar(200) NOT NULL,
	PRIMARY KEY (IDCategory)
);

CREATE TABLE Users (
	IDUser int NOT NULL AUTO_INCREMENT,
	Banned boolean NOT NULL,
	Name varchar(150) NOT NULL,
	Surname varchar(150) NOT NULL,
	Email varchar(200) NOT NULL,
	Username varchar(100) NOT NULL,
	Password varchar(150) NOT NULL,
	Type varchar(100) NOT NULL,
	NumComments int,
	PRIMARY KEY (IDUser)
);

CREATE TABLE ForumTopics (
	IDTopic int NOT NULL AUTO_INCREMENT,
	TopicName varchar(300) NOT NULL,
	PRIMARY KEY (IDTopic)
);

CREATE TABLE DiscussionThreads(
	IDThread int NOT NULL AUTO_INCREMENT,
	IDUser int NOT NULL,
	IDTopic int NOT NULL,
	ThreadName varchar(350) NOT NULL,
	DateCreated varchar(11),
	PRIMARY KEY (IDThread),
	FOREIGN KEY (IDUser) REFERENCES Users (IDUser),
	FOREIGN KEY (IDTopic) REFERENCES ForumTopics (IDTopic)
);

CREATE TABLE Posts (
	IDPost int NOT NULL AUTO_INCREMENT,
	IDUser int NOT NULL,
	IDThread int NOT NULL,
	PostComment text NOT NULL,
	DatePosted varchar(11),
	PRIMARY KEY (IDPost),
	FOREIGN KEY (IDUser) REFERENCES Users (IDUser),
	FOREIGN KEY (IDThread) REFERENCES DiscussionThreads (IDThread)
);

CREATE TABLE Wrote (
	IDAuthor int NOT NULL,
	IDBook bigint NOT NULL,
	FOREIGN KEY (IDAuthor) REFERENCES Authors (IDAuthor),
	FOREIGN KEY (IDBook) REFERENCES Books (IDBook),
	PRIMARY KEY (IDAuthor, IDBook)
);

CREATE TABLE Tagged (
	IDTag int NOT NULL,
	IDBook bigint NOT NULL,
	FOREIGN KEY (IDBook) REFERENCES Books (IDBook),
	FOREIGN KEY (IDTag) REFERENCES Tags (IDTag),
	PRIMARY KEY (IDTag,IDBook)
);

CREATE TABLE BookComments (
	IDBook bigint NOT NULL,
	IDUser int NOT NULL,
	Comment text NOT NULL,
	Date varchar(11),
	FOREIGN KEY (IDBook) REFERENCES Books (IDBook),
	FOREIGN KEY (IDUser) REFERENCES Users (IDUser),
	PRIMARY KEY (IDBook, IDUser)
);

CREATE TABLE Preferences (
	IDUser int NOT NULL,
	IDCategory int NOT NULL,
	FOREIGN KEY (IDUser) REFERENCES Users (IDUser),
	FOREIGN KEY (IDCategory) REFERENCES Categories (IDCategory),
	PRIMARY KEY (IDUser, IDCategory)
);

CREATE TABLE Rates (
	IDUser int NOT NULL,
	IDBook bigint NOT NULL,
	FOREIGN KEY (IDBook) REFERENCES Books (IDBook),
	FOREIGN KEY (IDUser) REFERENCES Users (IDUser),
	PRIMARY KEY (IDBook, IDUser)
);

CREATE TABLE BelongsTo (
	IDBook bigint NOT NULL,
	IDCategory int NOT NULL,
	FOREIGN KEY (IDBook) REFERENCES Books (IDBook),
	FOREIGN KEY (IDCategory) REFERENCES Categories (IDCategory),
	PRIMARY KEY (IDBook, IDCategory)
);

CREATE TABLE UserCategories(
	IDUser INT NOT NULL ,
	IDCategory INT NOT NULL ,
	FOREIGN KEY ( IDUser ) REFERENCES Users( IDUser ) ,
	FOREIGN KEY ( IDCategory ) REFERENCES Categories( IDCategory ) ,
	PRIMARY KEY ( IDUser, IDCategory )
);


CREATE TABLE UserBooks(
	IDUser INT NOT NULL ,
	IDBook BIGINT NOT NULL ,
	FOREIGN KEY ( IDUser ) REFERENCES Users( IDUser ) ,
	FOREIGN KEY ( IDBook ) REFERENCES Books( IDBook ) ,
	PRIMARY KEY ( IDUser, IDBook )
);
