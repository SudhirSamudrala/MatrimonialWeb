-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 12, 2022 at 10:55 AM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `matrimonial`
--
CREATE DATABASE IF NOT EXISTS `matrimonial` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `matrimonial`;

-- --------------------------------------------------------

--
-- Table structure for table `applyformeting`
--

DROP TABLE IF EXISTS `applyformeting`;
CREATE TABLE IF NOT EXISTS `applyformeting` (
  `Uid` int(11) NOT NULL AUTO_INCREMENT,
  `id` int(11) NOT NULL,
  `AppliedUserId` varchar(100) NOT NULL,
  `MeetingDate` date DEFAULT NULL,
  `MeetingTime` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Uid`),
  KEY `fk1` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `applyformeting`
--

INSERT INTO `applyformeting` (`Uid`, `id`, `AppliedUserId`, `MeetingDate`, `MeetingTime`) VALUES
(9, 3, '1', NULL, NULL),
(10, 3, '2', '2022-11-25', '12:00AM');

-- --------------------------------------------------------

--
-- Table structure for table `userstable`
--

DROP TABLE IF EXISTS `userstable`;
CREATE TABLE IF NOT EXISTS `userstable` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `FullName` varchar(100) NOT NULL,
  `EmailId` varchar(100) NOT NULL,
  `Age` int(11) NOT NULL,
  `Mobile` varchar(100) NOT NULL,
  `Path` varchar(100) DEFAULT NULL,
  `Password` varchar(100) NOT NULL,
  `RegDate` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `userstable`
--

INSERT INTO `userstable` (`id`, `FullName`, `EmailId`, `Age`, `Mobile`, `Path`, `Password`, `RegDate`) VALUES
(1, 'Gail Kim', 'gail.k@gmail.com', 25, '8404455804', NULL, '123456', '2022-11-10 20:15:17'),
(2, 'Ashley Fliehr', 'ashley.f@gmail.com', 27, '8588993697', NULL, '654321', '2022-11-10 18:30:00'),
(3, 'John Noble', 'john.n@gmail.com', 29, '8546917355', NULL, '852369', '2022-11-12 09:17:35');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
