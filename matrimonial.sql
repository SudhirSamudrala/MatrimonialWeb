CREATE DATABASE IF NOT EXISTS matrimonial;
USE matrimonial;

DROP TABLE IF EXISTS applyformeting;
CREATE TABLE IF NOT EXISTS applyformeting (
  Uid int(11) NOT NULL AUTO_INCREMENT,
  id int(11) NOT NULL,
  AppliedUserId varchar(100) NOT NULL,
  MeetingDate date DEFAULT NULL,
  MeetingTime varchar(100) DEFAULT NULL,
  PRIMARY KEY (Uid),
  KEY fk1 (id)
);


DROP TABLE IF EXISTS userstable;
CREATE TABLE IF NOT EXISTS userstable (
  id int(11) NOT NULL AUTO_INCREMENT,
  FullName varchar(100) NOT NULL,
  EmailId varchar(100) NOT NULL,
  Age int(11) NOT NULL,
  Mobile varchar(100) NOT NULL,
  Path varchar(100) DEFAULT NULL,
  Password varchar(100) NOT NULL,
  RegDate timestamp NOT NULL DEFAULT current_timestamp(),
  Profession varchar(70) DEFAULT NULL,
  Hobbies varchar(70) DEFAULT NULL,
  Religion varchar(70) DEFAULT NULL,
  Native varchar(70) DEFAULT NULL,
  Height varchar(70) DEFAULT NULL,
  Weight varchar(70) DEFAULT NULL,
  Role varchar(70) DEFAULT NULL,
  FileName varchar(150) DEFAULT NULL,
  Gender varchar(50) DEFAULT NULL,
  PRIMARY KEY (id)
);
