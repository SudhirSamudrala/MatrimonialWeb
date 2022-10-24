CREATE DATABASE IF NOT EXISTS matrimonial;
USE matrimonial;

DELIMITER $$
--
-- Procedures
--
DROP PROCEDURE IF EXISTS checkavailbilty$$
CREATE DEFINER=root@localhost PROCEDURE checkavailbilty (IN email VARCHAR(100))  NO SQL SELECT EmailId FROM userstable  WHERE EmailId=email$$

DROP PROCEDURE IF EXISTS login$$
CREATE DEFINER=root@localhost PROCEDURE login (IN useremail VARCHAR(100), IN password VARCHAR(100))  NO SQL SELECT EmailId,Password from  userstable where EmailId=useremail and Password=password$$

DROP PROCEDURE IF EXISTS registration$$
CREATE DEFINER=root@localhost PROCEDURE registration (IN fname VARCHAR(100), IN emailid VARCHAR(100), IN password VARCHAR(100))  NO SQL insert into userstable(FullName,EmailId,Password) VALUES(fname,emailid,password)$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table userstable
--

DROP TABLE IF EXISTS userstable;
CREATE TABLE IF NOT EXISTS userstable (
  id int(11) NOT NULL AUTO_INCREMENT,
  FullName varchar(100) NOT NULL,
  EmailId varchar(100) NOT NULL,
  Password varchar(100) NOT NULL,
  RegDate timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (id)
);
COMMIT;
